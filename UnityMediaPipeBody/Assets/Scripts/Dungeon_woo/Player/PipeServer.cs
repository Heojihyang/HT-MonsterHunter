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
    public Transform parent;                // �θ�(��ǥ)

    public GameObject landmarkPrefab;       // ���帶ũ 
    public GameObject linePrefab;           // ���帶ũ �������
    public GameObject headPrefab;           // �Ӹ��� ���帶ũ

    public bool anchoredBody = false;       // ���󵵵ɵ�
    public bool enableHead = true;          // �Ӹ� Ȱ��ȭ ����
    public float multiplier = 10f;          // (UpdateBody ��ǥ ���� ����)
    public float landmarkScale = 1f;        // ���帶ũ ũ��
    public float maxSpeed = 50f;            // (UpdateBody ��ǥ ���� ����)
    public int samplesForPose = 1;          // accumulatedValuesCount �񱳰�

    private Body body;                      // ��ü
    private NamedPipeServerStream server;   // ����

    // ���帶ũ�� ���� ��
    const int LANDMARK_COUNT = 33;          // (�Ӹ�����) �� 21, ǥ�� 12
    const int LINES_COUNT = 11;             // ����, ����, ������, �޼�, ������, �޴ٸ�, �����ٸ�, �޹�, ������ + 2


    // �Ӹ� ��ġ �����ε�?.. �ƴѰ�
    private Vector3 GetNormal(Vector3 p1, Vector3 p2, Vector3 p3)
    {
        Vector3 u = p2 - p1;
        Vector3 v = p3 - p1;
        Vector3 n = new Vector3((u.y * v.z - u.z * v.y), (u.z * v.x - u.x * v.z), (u.x * v.y - u.y * v.x));
        float nl = Mathf.Sqrt(n[0] * n[0] + n[1] * n[1] + n[2] * n[2]);
        return new Vector3(n[0] / nl, n[1] / nl, n[2] / nl);
    }


    // ���̽����κ��� ���帶ũ ��ġ�� �Ҵ�ޱ� ���� ����ü
    public struct AccumulatedBuffer
    {
        public Vector3 value;                   // ���̽� ��ǥ ������
        public int accumulatedValuesCount;      // ���� �Ҵ� ī��Ʈ ��

        // AccumulatedBuffer ����ü ������
        public AccumulatedBuffer(Vector3 v,int ac)
        {
            value = v;
            accumulatedValuesCount = ac;
        }
    }

    // �ٵ� Ŭ����(�� ��ü�� ������ Ŭ����)
    public class Body 
    {
        public Transform parent;                                                            // Body �θ�(��ġ����)
        public AccumulatedBuffer[] positionsBuffer = new AccumulatedBuffer[LANDMARK_COUNT]; // ���帶ũ ��ġ
        public Vector3[] localPositionTargets = new Vector3[LANDMARK_COUNT];                // �ڷ��帶ũ ���� ��ġ��
        public GameObject[] instances = new GameObject[LANDMARK_COUNT];                     // ���帶ũ ���� ������Ʈ ����
        public LineRenderer[] lines = new LineRenderer[LINES_COUNT];                        // ���帶ũ ���� ���� �ð�ȭ ����  
        public GameObject head;                                                             // �Ӹ�

        public bool active;                     // Body Ȱ��ȭ ���� (�⺻�� false), ���̽㿡�� ��ǥ�� �޾ƿ��� True�� ���ŵ�
        public bool setCalibration = false;     // ���� ���ÿ���(ī�޶��ΰ�?)
        public Vector3 calibrationOffset;       // ���� ������
        public Vector3 virtualHeadPosition;     // ���� �Ӹ��� ��ġ ����

        // Body Ŭ���� ������
        // 1���� �Ӹ�, 33���� ���帶ũ, 11���� ���帶ũ���� ����
        public Body(Transform parent, GameObject landmarkPrefab, GameObject linePrefab,float s, GameObject headPrefab)
        {
            this.parent = parent; 

            // GameObject[] instances�� 33�� ���帶ũ ������Ʈ ����
            for (int i = 0; i < instances.Length; ++i)   
            {
                instances[i] = Instantiate(landmarkPrefab);
                instances[i].transform.localScale = Vector3.one * s;    // ���帶ũ ũ�� ����
                instances[i].transform.parent = parent;                 // ���帶ũ�� �θ� ����
                instances[i].name = ((Landmark)i).ToString();           // ���帶ũ enum �����ͼ� �̸� ����

                // ǥ�� ���帶ũ�� ȭ�鿡 ������ �ʵ��� ����
                if (headPrefab && i >= 0 && i <= 10)
                {
                    instances[i].transform.localScale = Vector3.one * 0f;
                }
            }

            // ���η����� ����
            for (int i = 0; i < lines.Length; ++i)
            {
                lines[i] = Instantiate(linePrefab).GetComponent<LineRenderer>();
            }

            // �Ӹ� ����
            if (headPrefab)
            {
                head = Instantiate(headPrefab);
                head.transform.localPosition = headPrefab.transform.position;
                head.transform.localRotation = headPrefab.transform.localRotation;
                head.transform.localScale = headPrefab.transform.localScale;
            }
        }

        // ���� ����, ����
        public void UpdateLines()
        {
            // positionCount : ������ ����Ǵ� ���帶ũ ��
            // Body ����
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

            // ��� ����
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

        // ī�޶� ������ ���� Body �߽� ��� - �Լ� ���κ� �ּ�ó�� �ص�
        // Body�� �߽���(0,0,0)���� ���߱� ����
        public void Calibrate()
        {
            Vector3 centre = (localPositionTargets[(int)Landmark.LEFT_HIP] + localPositionTargets[(int)Landmark.RIGHT_HIP]) / 2f;
            calibrationOffset = -centre;
            setCalibration = true;          // false : ���� ��, true : ���� �Ϸ�
        }

        // 4���� ���帶ũ���� ���� ���
        public float GetAngle(Landmark referenceFrom, Landmark referenceTo, Landmark from, Landmark to)
        {
            Vector3 reference = (instances[(int)referenceTo].transform.position - instances[(int)referenceFrom].transform.position).normalized;
            Vector3 direction = (instances[(int)to].transform.position - instances[(int)from].transform.position).normalized;
            return Vector3.SignedAngle(reference, direction, Vector3.Cross(reference, direction));
        }

        // �� ���帶ũ���� �Ÿ� ���
        public float Distance(Landmark from,Landmark to)
        {
            return (instances[(int)from].transform.position - instances[(int)to].transform.position).magnitude;
        }

        // �ش� ���帶ũ�� ���� ��ǥ ��ȯ - �θ����
        public Vector3 LocalPosition(Landmark Mark)
        {
            return instances[(int)Mark].transform.localPosition;
        }

        // �ش� ���帶ũ�� ��ǥ ��ȯ
        public Vector3 Position(Landmark Mark) 
        {
            return instances[(int)Mark].transform.position;
        }

    }

    private void Start()
    {
        // CultureInfo.InvariantCulture ������ ����
        System.Globalization.CultureInfo.DefaultThreadCurrentCulture = System.Globalization.CultureInfo.InvariantCulture;

        // body ��ü ����
        body = new Body(parent,landmarkPrefab,linePrefab,landmarkScale,enableHead?headPrefab:null);



        // ������ ���� - ������ ��׶��� �����忡�� Run() ����
        Thread t = new Thread(new ThreadStart(Run));
        t.Start();
    }

    private void Update()
    {
        UpdateBody(body);

        // �׽�Ʈ
        // Debug.Log("Position : " + body.Position(Landmark.RIGHT_WRIST));
        // Debug.Log("localPosition : " + body.instances[16].transform.localPosition);

        // �� PlayerController.cs���� ������ ���ϱ� ���� ���� ����帶ũ�� ��� ������Ʈ �Ѱ��ֱ� ��
        // PlayerController.cs�� getLandmarkPosition(body.instances, body.head);
        GetComponent<PlayerAssessment>().getPlayerLandmark(body.instances, body.head);
    }


    // ���帶ũ ��ġ ����
    private void UpdateBody(Body b)
    {
        // body ��ü�� �������� �ʾҴٸ� ����(���̽㿡�� ��ǥ���� �������� ���ٸ� ����)
        if (b.active == false) return;      

        // �� 1. ���帶ũ ��ġ ���� ��
        // ���̽����κ��� �޾ƿ� ��ǥ�� ���ٸ� �ǳʶٱ�
        // ���帶ũ�� ��ǥ���� ���̽����κ��� �޾ƿ� ������ǥ�� ������� �Ҵ��Ű�� ���� ����ü �ʱ�ȭ
        // ���� ���帶ũ�� ��ġ : b.localPositionTargets[i]
        for (int i = 0; i < LANDMARK_COUNT; ++i)
        {
            if (b.positionsBuffer[i].accumulatedValuesCount < samplesForPose)   
                continue;
            b.localPositionTargets[i] = b.positionsBuffer[i].value / (float)b.positionsBuffer[i].accumulatedValuesCount * multiplier;
            b.positionsBuffer[i] = new AccumulatedBuffer(Vector3.zero,0);   
        }

        /*
        // PlayerController.cs���� ��ǥ���� Ȱ���ϴ� �ٸ� ���
        // localPositionTargets�迭�� ���� ���� ��ǥ���� ���� ��ǥ������ �����Ͽ� globalLandmarkPositions�� ����
        Vector3[] globalLandmarkPositions = new Vector3[localPositionTargets.Length];
        for (int i = 0; i < localPositionTargets.Length; i++)
        {
            globalLandmarkPositions[i] = parent.TransformPoint(localPositionTargets[i]);
        }
        */


        
        // ī�޶� ���� - ���X
        if (!b.setCalibration)
        {
            print("Set Calibration Data");
            b.Calibrate();

            if(FindObjectOfType<CameraController>())
                FindObjectOfType<CameraController>().Calibrate(b.instances[(int)Landmark.NOSE].transform);
        }
        


        // �� 2. ���帶ũ ���� ������Ʈ ��ġ ���� ��
        // instances, positionsBuffer, localPositionTargets
        // Vector3.MoveTowards(������ġ, ��ǥ��ġ, �Ÿ���(maxDistanceDelta))
        // ���ŵ� ���帶ũ ������Ʈ�� �������� ���� ���� ����
        // �츮�� �����;� �ϴ� �κ� �� b.instances[i].transform.localPosition ��
        for (int i = 0; i < LANDMARK_COUNT; ++i)
        {
            b.instances[i].transform.localPosition = Vector3.MoveTowards(b.instances[i].transform.localPosition, b.localPositionTargets[i]/*+b.calibrationOffset*/, Time.deltaTime * maxSpeed);
        }
        b.UpdateLines();    
        

        // ��� ������Ʈ ������ ���� virtualHeadPosition ���
        b.virtualHeadPosition = (b.Position(Landmark.RIGHT_EAR) + b.Position(Landmark.LEFT_EAR)) / 2f;

        // ��� ������Ʈ ����
        // ���� ��� ��ġ : �� b.head.transform.position ��
        if (b.head)
        {
            // Experimental method and getting the head pose.
            b.head.transform.position = b.virtualHeadPosition+Vector3.up* .5f;  //virtualHeadPosition�� ���� �� ����
            Vector3 n1 = Vector3.Scale(new Vector3(.1f, 1f, .1f), GetNormal(b.Position((Landmark)0), b.Position((Landmark)8), b.Position((Landmark)7))).normalized;
            Vector3 n2 = Vector3.Scale(new Vector3(1f, .1f, 1f), GetNormal(b.Position((Landmark)0), b.Position((Landmark)4), b.Position((Landmark)1))).normalized;
            b.head.transform.rotation = Quaternion.LookRotation(-n2, n1);
        }
    }

    // NamedPipe�� ���� ���̽� ��� - positionsBuffer
    private void Run()
    {
        // CultureInfo.InvariantCulture ������ ����
        System.Globalization.CultureInfo.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;

        // UnityMediaPipeBody ��� �̸��� �����Ǿ��ִ� �������� �����Ͽ� server�� ����
        server = new NamedPipeServerStream("UnityMediaPipeBody",PipeDirection.InOut, 99, PipeTransmissionMode.Message);

        print("Waiting for connection...");
        server.WaitForConnection();

        print("Connected.");    //���� ���� �Ϸ�
        var br = new BinaryReader(server, Encoding.UTF8);

        //���̽����κ��� ����ؼ� ���帶ũ ��ǥ�� �޾ƿ� (���� ����)
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

                    // ��ǥ�� �־��ִ� �κ�
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
