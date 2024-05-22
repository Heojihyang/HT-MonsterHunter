using System.Collections;

using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading;
using UnityEngine;

/* Currently very messy because both the server code and hand-drawn code is all in the same file here.
 * But it is still fairly straightforward to use as a reference/base.
 */

public class PipeServer : MonoBehaviour
{
    public Transform parent;                // 부모(좌표)

    public GameObject landmarkPrefab;       // 랜드마크 
    public GameObject linePrefab;           // 랜드마크 연결라인
    public GameObject headPrefab;           // 머리통 랜드마크

    public bool anchoredBody = false;       // 몰라도될듯
    public bool enableHead = true;          // 머리 활성화 여부
    public float multiplier = 10f;          // (UpdateBody 좌표 갱신 관련)
    public float landmarkScale = 1f;        // 랜드마크 크기
    public float maxSpeed = 50f;            // (UpdateBody 좌표 갱신 관련)
    public int samplesForPose = 1;          // accumulatedValuesCount 비교값

    private Body body;                      // 몸체
    private NamedPipeServerStream server;   // 서버

    // 랜드마크와 라인 수
    const int LANDMARK_COUNT = 33;          // (머리제외) 몸 21, 표정 12
    const int LINES_COUNT = 11;             // 몸통, 왼팔, 오른팔, 왼손, 오른손, 왼다리, 오른다리, 왼발, 오른발 + 2


    // 머리 위치 설정인듯?.. 아닌가
    private Vector3 GetNormal(Vector3 p1, Vector3 p2, Vector3 p3)
    {
        Vector3 u = p2 - p1;
        Vector3 v = p3 - p1;
        Vector3 n = new Vector3((u.y * v.z - u.z * v.y), (u.z * v.x - u.x * v.z), (u.x * v.y - u.y * v.x));
        float nl = Mathf.Sqrt(n[0] * n[0] + n[1] * n[1] + n[2] * n[2]);
        return new Vector3(n[0] / nl, n[1] / nl, n[2] / nl);
    }


    // 파이썬으로부터 랜드마크 위치를 할당받기 위한 구조체
    public struct AccumulatedBuffer
    {
        public Vector3 value;                   // 파이썬 좌표 누적값
        public int accumulatedValuesCount;      // 누적 할당 카운트 값

        // AccumulatedBuffer 구조체 생성자
        public AccumulatedBuffer(Vector3 v,int ac)
        {
            value = v;
            accumulatedValuesCount = ac;
        }
    }

    // 바디 클래스(몸 형체를 만들어내는 클래스)
    public class Body 
    {
        public Transform parent;                                                            // Body 부모(위치정보)
        public AccumulatedBuffer[] positionsBuffer = new AccumulatedBuffer[LANDMARK_COUNT]; // 랜드마크 위치
        public Vector3[] localPositionTargets = new Vector3[LANDMARK_COUNT];                // ★랜드마크 로컬 위치★
        public GameObject[] instances = new GameObject[LANDMARK_COUNT];                     // 랜드마크 게임 오브젝트 저장
        public LineRenderer[] lines = new LineRenderer[LINES_COUNT];                        // 랜드마크 연결 라인 시각화 관련  
        public GameObject head;                                                             // 머리

        public bool active;                     // Body 활성화 여부 (기본값 false), 파이썬에서 좌표를 받아오면 True로 갱신됨
        public bool setCalibration = false;     // 보정 세팅여부(카메라인가?)
        public Vector3 calibrationOffset;       // 보정 오프셋
        public Vector3 virtualHeadPosition;     // 가상 머리의 위치 저장

        // Body 클래스 생성자
        // 1개의 머리, 33개의 랜드마크, 11개의 랜드마크라인 생성
        public Body(Transform parent, GameObject landmarkPrefab, GameObject linePrefab,float s, GameObject headPrefab)
        {
            this.parent = parent; 

            // GameObject[] instances에 33개 랜드마크 오브젝트 생성
            for (int i = 0; i < instances.Length; ++i)   
            {
                instances[i] = Instantiate(landmarkPrefab);
                instances[i].transform.localScale = Vector3.one * s;    // 랜드마크 크기 설정
                instances[i].transform.parent = parent;                 // 랜드마크의 부모 설정
                instances[i].name = ((Landmark)i).ToString();           // 랜드마크 enum 가져와서 이름 지정

                // 표정 랜드마크는 화면에 보이지 않도록 설정
                if (headPrefab && i >= 0 && i <= 10)
                {
                    instances[i].transform.localScale = Vector3.one * 0f;
                }
            }

            // 라인렌더러 생성
            for (int i = 0; i < lines.Length; ++i)
            {
                lines[i] = Instantiate(linePrefab).GetComponent<LineRenderer>();
            }

            // 머리 생성
            if (headPrefab)
            {
                head = Instantiate(headPrefab);
                head.transform.localPosition = headPrefab.transform.position;
                head.transform.localRotation = headPrefab.transform.localRotation;
                head.transform.localScale = headPrefab.transform.localScale;
            }
        }

        // 라인 연결, 갱신
        public void UpdateLines()
        {
            // positionCount : 라인이 연결되는 랜드마크 수
            // Body 라인
            lines[0].positionCount = 4;
            lines[0].SetPosition(0, Position((Landmark)32));
            lines[0].SetPosition(1, Position((Landmark)30));
            lines[0].SetPosition(2, Position((Landmark)28));
            lines[0].SetPosition(3, Position((Landmark)32));
            lines[1].positionCount = 4;
            lines[1].SetPosition(0, Position((Landmark)31));
            lines[1].SetPosition(1, Position((Landmark)29));
            lines[1].SetPosition(2, Position((Landmark)27));
            lines[1].SetPosition(3, Position((Landmark)31));

            lines[2].positionCount = 3;
            lines[2].SetPosition(0, Position((Landmark)28));
            lines[2].SetPosition(1, Position((Landmark)26));
            lines[2].SetPosition(2, Position((Landmark)24));
            lines[3].positionCount = 3;
            lines[3].SetPosition(0, Position((Landmark)27));
            lines[3].SetPosition(1, Position((Landmark)25));
            lines[3].SetPosition(2, Position((Landmark)23));

            lines[4].positionCount = 5;
            lines[4].SetPosition(0, Position((Landmark)24));
            lines[4].SetPosition(1, Position((Landmark)23));
            lines[4].SetPosition(2, Position((Landmark)11));
            lines[4].SetPosition(3, Position((Landmark)12));
            lines[4].SetPosition(4, Position((Landmark)24));

            lines[5].positionCount = 4;
            lines[5].SetPosition(0, Position((Landmark)12));
            lines[5].SetPosition(1, Position((Landmark)14));
            lines[5].SetPosition(2, Position((Landmark)16));
            lines[5].SetPosition(3, Position((Landmark)22));
            lines[6].positionCount = 4;
            lines[6].SetPosition(0, Position((Landmark)11));
            lines[6].SetPosition(1, Position((Landmark)13));
            lines[6].SetPosition(2, Position((Landmark)15));
            lines[6].SetPosition(3, Position((Landmark)21));

            lines[7].positionCount = 4;
            lines[7].SetPosition(0, Position((Landmark)16));
            lines[7].SetPosition(1, Position((Landmark)18));
            lines[7].SetPosition(2, Position((Landmark)20));
            lines[7].SetPosition(3, Position((Landmark)16));
            lines[8].positionCount = 4;
            lines[8].SetPosition(0, Position((Landmark)15));
            lines[8].SetPosition(1, Position((Landmark)17));
            lines[8].SetPosition(2, Position((Landmark)19));
            lines[8].SetPosition(3, Position((Landmark)15));

            // 헤드 라인
            if (!head)
            {
                lines[9].positionCount = 2;
                lines[9].SetPosition(0, Position((Landmark)10));
                lines[9].SetPosition(1, Position((Landmark)9));

                lines[10].positionCount = 5;
                lines[10].SetPosition(0, Position((Landmark)8));
                lines[10].SetPosition(1, Position((Landmark)5));
                lines[10].SetPosition(2, Position((Landmark)0));
                lines[10].SetPosition(3, Position((Landmark)2));
                lines[10].SetPosition(4, Position((Landmark)7));
            }
        }

        // 카메라 교정을 위한 Body 중심 계산 - 함수 사용부분 주석처리 해둠
        // Body의 중심을(0,0,0)으로 맞추기 위해
        public void Calibrate()
        {
            Vector3 centre = (localPositionTargets[(int)Landmark.LEFT_HIP] + localPositionTargets[(int)Landmark.RIGHT_HIP]) / 2f;
            calibrationOffset = -centre;
            setCalibration = true;          // false : 보정 전, true : 보정 완료
        }

        // 4개의 랜드마크간의 각도 계산
        public float GetAngle(Landmark referenceFrom, Landmark referenceTo, Landmark from, Landmark to)
        {
            Vector3 reference = (instances[(int)referenceTo].transform.position - instances[(int)referenceFrom].transform.position).normalized;
            Vector3 direction = (instances[(int)to].transform.position - instances[(int)from].transform.position).normalized;
            return Vector3.SignedAngle(reference, direction, Vector3.Cross(reference, direction));
        }

        // 두 랜드마크간의 거리 계산
        public float Distance(Landmark from,Landmark to)
        {
            return (instances[(int)from].transform.position - instances[(int)to].transform.position).magnitude;
        }

        // 해당 랜드마크의 로컬 좌표 반환 - 부모기준
        public Vector3 LocalPosition(Landmark Mark)
        {
            return instances[(int)Mark].transform.localPosition;
        }

        // 해당 랜드마크의 좌표 반환
        public Vector3 Position(Landmark Mark) 
        {
            return instances[(int)Mark].transform.position;
        }

    }

    private void Start()
    {
        // CultureInfo.InvariantCulture 지역권 설정
        System.Globalization.CultureInfo.DefaultThreadCurrentCulture = System.Globalization.CultureInfo.InvariantCulture;

        // body 객체 생성
        body = new Body(parent,landmarkPrefab,linePrefab,landmarkScale,enableHead?headPrefab:null);



        // 스레드 생성 - 별도의 백그라운드 스레드에서 Run() 실행
        Thread t = new Thread(new ThreadStart(Run));
        t.Start();
    }

    private void Update()
    {
        UpdateBody(body);

        // 테스트
        // Debug.Log("Position : " + body.Position(Landmark.RIGHT_WRIST));
        // Debug.Log("localPosition : " + body.instances[16].transform.localPosition);

        // ★ PlayerController.cs에서 정답을 비교하기 위해 현재 랜드드마크와 헤드 오브젝트 넘겨주기 ★
        // PlayerController.cs의 getLandmarkPosition(body.instances, body.head);
        GetComponent<PlayerAssessment>().getPlayerLandmark(body.instances, body.head);
    }


    // 랜드마크 위치 갱신
    private void UpdateBody(Body b)
    {
        // body 객체가 생성되지 않았다면 종료(파이썬에서 좌표값을 받은적이 없다면 종료)
        if (b.active == false) return;      

        // ★ 1. 랜드마크 위치 갱신 ★
        // 파이썬으로부터 받아온 좌표가 없다면 건너뛰기
        // 랜드마크의 좌표값을 파이썬으로부터 받아온 누적좌표의 평균으로 할당시키고 버퍼 구조체 초기화
        // 현재 랜드마크의 위치 : b.localPositionTargets[i]
        for (int i = 0; i < LANDMARK_COUNT; ++i)
        {
            if (b.positionsBuffer[i].accumulatedValuesCount < samplesForPose)   
                continue;
            b.localPositionTargets[i] = b.positionsBuffer[i].value / (float)b.positionsBuffer[i].accumulatedValuesCount * multiplier;
            b.positionsBuffer[i] = new AccumulatedBuffer(Vector3.zero,0);   
        }

        /*
        // PlayerController.cs에서 좌표값을 활용하는 다른 방법
        // localPositionTargets배열의 현재 로컬 좌표값을 전역 좌표값으로 변경하여 globalLandmarkPositions에 저장
        Vector3[] globalLandmarkPositions = new Vector3[localPositionTargets.Length];
        for (int i = 0; i < localPositionTargets.Length; i++)
        {
            globalLandmarkPositions[i] = parent.TransformPoint(localPositionTargets[i]);
        }
        */


        
        // 카메라 교정 - 사용X
        if (!b.setCalibration)
        {
            print("Set Calibration Data");
            b.Calibrate();

            if(FindObjectOfType<CameraController>())
                FindObjectOfType<CameraController>().Calibrate(b.instances[(int)Landmark.NOSE].transform);
        }
        


        // ★ 2. 랜드마크 게임 오브젝트 위치 갱신 ★
        // instances, positionsBuffer, localPositionTargets
        // Vector3.MoveTowards(현재위치, 목표위치, 거리차(maxDistanceDelta))
        // 갱신된 랜드마크 오브젝트를 기준으로 연결 라인 갱신
        // 우리가 가져와야 하는 부분 ★ b.instances[i].transform.localPosition ★
        for (int i = 0; i < LANDMARK_COUNT; ++i)
        {
            b.instances[i].transform.localPosition = Vector3.MoveTowards(b.instances[i].transform.localPosition, b.localPositionTargets[i]/*+b.calibrationOffset*/, Time.deltaTime * maxSpeed);
        }
        b.UpdateLines();    
        

        // 헤드 오브젝트 갱신을 위한 virtualHeadPosition 계산
        b.virtualHeadPosition = (b.Position(Landmark.RIGHT_EAR) + b.Position(Landmark.LEFT_EAR)) / 2f;

        // 헤드 오브젝트 갱신
        // 최종 헤드 위치 : ★ b.head.transform.position ★
        if (b.head)
        {
            // Experimental method and getting the head pose.
            b.head.transform.position = b.virtualHeadPosition+Vector3.up* .5f;  //virtualHeadPosition는 양쪽 귀 사이
            Vector3 n1 = Vector3.Scale(new Vector3(.1f, 1f, .1f), GetNormal(b.Position((Landmark)0), b.Position((Landmark)8), b.Position((Landmark)7))).normalized;
            Vector3 n2 = Vector3.Scale(new Vector3(1f, .1f, 1f), GetNormal(b.Position((Landmark)0), b.Position((Landmark)4), b.Position((Landmark)1))).normalized;
            b.head.transform.rotation = Quaternion.LookRotation(-n2, n1);
        }
    }

    // NamedPipe를 통한 파이썬 통신 - positionsBuffer
    private void Run()
    {
        // CultureInfo.InvariantCulture 지역권 설정
        System.Globalization.CultureInfo.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;

        // UnityMediaPipeBody 라고 이름이 지정되어있는 파이프를 오픈하여 server에 저장
        server = new NamedPipeServerStream("UnityMediaPipeBody",PipeDirection.InOut, 99, PipeTransmissionMode.Message);

        print("Waiting for connection...");
        server.WaitForConnection();

        print("Connected.");    //서버 연결 완료
        var br = new BinaryReader(server, Encoding.UTF8);

        //파이썬으로부터 계속해서 랜드마크 좌표를 받아옴 (무한 루프)
        while (true)
        {
            try
            {
                Body h = body;  
                var len = (int)br.ReadUInt32(); 
                var str = new string(br.ReadChars(len));    
                string[] lines = str.Split('\n');

                foreach (string l in lines)
                {
                    if (string.IsNullOrWhiteSpace(l))
                        continue;

                    string[] s = l.Split('|');
                    if (s.Length < 5) continue;

                    if (anchoredBody && s[0] != "ANCHORED") continue;
                    if (!anchoredBody && s[0] != "FREE") continue;

                    int i;

                    // 좌표를 넣어주는 부분
                    if (!int.TryParse(s[1], out i)) continue;  
                    h.positionsBuffer[i].value += new Vector3(float.Parse(s[2]), float.Parse(s[3]), float.Parse(s[4]));
                    h.positionsBuffer[i].accumulatedValuesCount += 1;
                    h.active = true;
                }
            }
            catch (EndOfStreamException)
            {
                break;                    // When client disconnects
            }
        }

    }

    private void OnDisable()
    {
        print("Client disconnected.");
        server.Close();
        server.Dispose();
    }
}
