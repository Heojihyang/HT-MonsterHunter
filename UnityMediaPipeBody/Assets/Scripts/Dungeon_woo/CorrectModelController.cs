using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorrectModelController : MonoBehaviour
{
    // 아울렛 접속
    public Camera mainCamera;                // 메인카메라
    public Transform coParent;               // 정답 랜드마크들의 부모 - CorrectModel 하위에 생성되도록
    public GameObject landmarkPrefab;        // 정답모델 랜드마크 프리팹
    public GameObject linePrefab;            // 정답모델 랜드마크 라인 프리팹
    public GameObject dungeonScene;          //씬 관리 오브젝트

    // 랜드마크와 라인 수
    const int LANDMARK_COUNT = 15;           // 정답모델 랜드마크 수
    const int LINES_COUNT = 5;               // 정답모델 랜드마크 라인 수

    // ★정답모델 객체★
    public CorrectModel correctModel;

    // Update 델타타임 관련
    private int i = 0;                       // 증가할 변수
    private float elapsedTime = 0.0f;        // 시간 경과를 추적할 변수
    private const float interval = 3.0f;     // 증가 간격 (초 단위)

    // ★진행되는 던전의 랜드마크 포지션★ - 6가지 동작을 담을 수 있음(기본자세+운동자세)
    public Vector3[,] dungeonProgressPosition = new Vector3[6, LANDMARK_COUNT];


    /*
    [ 정답모델 INDEX ]
    0 : Co_HEAD
    1 : Co_LEFT_SHOULDER,   2 : Co_RIGHT_SHOULDER,   3 : Co_LEFT_ELBOW,   4 : Co_RIGHT_ELBOW
    5 : Co_LEFT_WRIST,   6 : Co_RIGHT_WRIST,   7 : Co_LEFT_HIP,   8 : Co_RIGHT_HOP
    9 : Co_LEFT_KNEE,   10 : Co_RIGHT_KNEE,   11 : Co_LEFT_ANKLE,   12 : Co_RIGHT_ANKLE
    13 : Co_LEFT_INDEX,   14 : Co_RIGHT_INDEX
    */


    // 허벅지 던전 셋팅() - 참조변수
    public void setDungeon_Thigh(ref Vector3[,] positions)
    {
        // 1-1. 사이드 레그레이즈 정자세
        positions[0, 0] = coParent.position + new Vector3(0.0f, 0.0f, 0.0f);
        positions[0, 1] = coParent.position + new Vector3(-1.0f, -1.0f, 0.0f);
        positions[0, 2] = coParent.position + new Vector3(1.0f, -1.0f, 0.0f);
        positions[0, 3] = coParent.position + new Vector3(-2.0f, -1.0f, 0.0f);
        positions[0, 4] = coParent.position + new Vector3(2.0f, -1.0f, 0.0f);
        positions[0, 5] = coParent.position + new Vector3(-3.0f, -1.0f, 0.0f);
        positions[0, 6] = coParent.position + new Vector3(3.0f, -1.0f, 0.0f);
        positions[0, 7] = coParent.position + new Vector3(-1.0f, -3.5f, 0.0f);
        positions[0, 8] = coParent.position + new Vector3(1.0f, -3.5f, 0.0f);
        positions[0, 9] = coParent.position + new Vector3(-1.0f, -5.5f, 0.0f);
        positions[0, 10] = coParent.position + new Vector3(1.0f, -5.5f, 0.0f);
        positions[0, 11] = coParent.position + new Vector3(-1.0f, -7.5f, 0.0f);
        positions[0, 12] = coParent.position + new Vector3(1.0f, -7.5f, 0.0f);
        positions[0, 13] = coParent.position + new Vector3(-1.0f, -8.5f, 0.0f);
        positions[0, 14] = coParent.position + new Vector3(1.0f, -8.5f, 0.0f);

        // 1-2. 사이드 레그 레이즈 
        positions[1, 0] = coParent.position + new Vector3(0.0f, 0.0f, 0.0f);
        positions[1, 1] = coParent.position + new Vector3(-1.0f, -1.0f, 0.0f);
        positions[1, 2] = coParent.position + new Vector3(1.0f, -1.0f, 0.0f);
        positions[1, 3] = coParent.position + new Vector3(-2.0f, -1.0f, 0.0f);
        positions[1, 4] = coParent.position + new Vector3(2.0f, -1.0f, 0.0f);
        positions[1, 5] = coParent.position + new Vector3(-3.0f, -1.0f, 0.0f);
        positions[1, 6] = coParent.position + new Vector3(3.0f, -1.0f, 0.0f);
        positions[1, 7] = coParent.position + new Vector3(-1.0f, -3.5f, 0.0f);
        positions[1, 8] = coParent.position + new Vector3(1.0f, -3.5f, 0.0f);
        positions[1, 9] = coParent.position + new Vector3(-1.0f, -5.5f, 0.0f);
        positions[1, 10] = coParent.position + new Vector3(2.43f, -7.62f, 0.0f);
        positions[1, 11] = coParent.position + new Vector3(-1.0f, -7.5f, 0.0f);
        positions[1, 12] = coParent.position + new Vector3(3.77f, -10.4f, 0.0f);
        positions[1, 13] = coParent.position + new Vector3(-1.0f, -8.5f, 0.0f);
        positions[1, 14] = coParent.position + new Vector3(4.4f, -11.84f, 0.0f);

        // 2-1. 스쿼트 정자세

        // 2-2. 스쿼트

        // 3-1. 런지 정자세

        // 3-2. 런지
        

        Debug.Log("정답모델 허벅지 던전 셋팅 완료");
    }

    // 다른 던전도 셋팅하세요()
    public void setDungeon_pal(ref Vector3[,] position)
    {

    }


    // 정답 모델 클래스
    public class CorrectModel
    {
        // 부모 포지션
        public Transform coParent;

        // 생성될 랜드마크와 라인 
        public Vector3[] co_LandmarkPositions = new Vector3[LANDMARK_COUNT];           // 랜드마크 위치 포지션 배열
        public GameObject[] co_Instances = new GameObject[LANDMARK_COUNT];             // 랜드마크 프리팹 오브젝트 배열
        public LineRenderer[] co_lines = new LineRenderer[LINES_COUNT];                // 랜드마크 라인 배열

        // 랜드마크 생성 포지션(대자로 벌리고 있는 모습) ## 다음에 개발하면 여기부터 개발 ##
        public Vector3[] co_LandmarkStartPositions = new Vector3[LANDMARK_COUNT];

        // 랜드마크 이름
        public string[] co_LandmarkNames = new string[] {"Co_HEAD", "Co_LEFT_SHOULDER", "Co_RIGHT_SHOULDER", "Co_LEFT_ELBOW",
                                                         "Co_RIGHT_ELBOW", "Co_LEFT_WRIST", "Co_RIGHT_WRIST", "Co_LEFT_HIP",
                                                         "Co_RIGHT_HIP", "Co_LEFT_KNEE", "Co_RIGHT_KNEE", "Co_LEFT_ANKLE",
                                                         "Co_RIGHT_ANKLE", "Co_LEFT_INDEX","Co_RIGHT_INDEX"};

        // 생성자()
        public CorrectModel(Transform coParent, GameObject landmarkPrefab, GameObject linePrefab, int LANDMARK_COUNT, int LINES_COUNT)
        {
            this.coParent = coParent;

            // 랜드마크 포지션 초기화 - 팔 벌리고 있는 모습
            co_LandmarkPositions[0] = coParent.position + new Vector3(0.0f, 0.0f, 0.0f);
            co_LandmarkPositions[1] = coParent.position + new Vector3(-1.0f, -1.0f, 0.0f);
            co_LandmarkPositions[2] = coParent.position + new Vector3(1.0f, -1.0f, 0.0f);
            co_LandmarkPositions[3] = coParent.position + new Vector3(-2.0f, -1.0f, 0.0f);
            co_LandmarkPositions[4] = coParent.position + new Vector3(2.0f, -1.0f, 0.0f);
            co_LandmarkPositions[5] = coParent.position + new Vector3(-3.0f, -1.0f, 0.0f);
            co_LandmarkPositions[6] = coParent.position + new Vector3(3.0f, -1.0f, 0.0f);
            co_LandmarkPositions[7] = coParent.position + new Vector3(-1.0f, -3.5f, 0.0f);
            co_LandmarkPositions[8] = coParent.position + new Vector3(1.0f, -3.5f, 0.0f);
            co_LandmarkPositions[9] = coParent.position + new Vector3(-1.0f, -5.5f, 0.0f);
            co_LandmarkPositions[10] = coParent.position + new Vector3(1.0f, -5.5f, 0.0f);
            co_LandmarkPositions[11] = coParent.position + new Vector3(-1.0f, -7.5f, 0.0f);
            co_LandmarkPositions[12] = coParent.position + new Vector3(1.0f, -7.5f, 0.0f);
            co_LandmarkPositions[13] = coParent.position + new Vector3(-1.0f, -8.5f, 0.0f);
            co_LandmarkPositions[14] = coParent.position + new Vector3(1.0f, -8.5f, 0.0f);

            // 랜드마크 생성
            for (int i = 0; i < LANDMARK_COUNT; i++)
            {
                co_Instances[i] = Instantiate(landmarkPrefab);
                co_Instances[i].transform.localScale = Vector3.one * 0.5f;
                
                co_Instances[i].name = co_LandmarkNames[i];
                co_Instances[i].GetComponent<Transform>().position = co_LandmarkPositions[i];
                co_Instances[i].transform.parent = coParent;
            }

            // 라인렌더러 생성
            for (int i = 0; i < LINES_COUNT; i++)
            {
                co_lines[i] = Instantiate(linePrefab).GetComponent<LineRenderer>();
            }
        }

        // 정답 랜드마크 업데이트() - dpp : 던전로직포지션, rowNum : 현재 동작이 저장된 행번호
        public void UpdateCoLandmarks(Vector3[,] dpp, int rowNum)
        {
            // 랜드마크 업데이트
            for (int i = 0; i < LANDMARK_COUNT; i++)
            {
                co_Instances[i].GetComponent<Transform>().position = dpp[rowNum,i];
            }

            UpdateCoLines();
            // Debug.Log(rowNum + "번 동작 업데이트 완료");
        }

        // 정답 랜드마크 라인 업데이트()
        public void UpdateCoLines()
        {
            // 몸통
            co_lines[0].positionCount = 5;
            co_lines[0].SetPosition(0, co_Instances[1].transform.position);
            co_lines[0].SetPosition(1, co_Instances[2].transform.position);
            co_lines[0].SetPosition(2, co_Instances[8].transform.position);
            co_lines[0].SetPosition(3, co_Instances[7].transform.position);
            co_lines[0].SetPosition(4, co_Instances[1].transform.position);

            // 왼팔
            co_lines[1].positionCount = 3;
            co_lines[1].SetPosition(0, co_Instances[1].transform.position);
            co_lines[1].SetPosition(1, co_Instances[3].transform.position);
            co_lines[1].SetPosition(2, co_Instances[5].transform.position);

            // 오른팔
            co_lines[2].positionCount = 3;
            co_lines[2].SetPosition(0, co_Instances[2].transform.position);
            co_lines[2].SetPosition(1, co_Instances[4].transform.position);
            co_lines[2].SetPosition(2, co_Instances[6].transform.position);

            // 왼발
            co_lines[3].positionCount = 4;
            co_lines[3].SetPosition(0, co_Instances[7].transform.position);
            co_lines[3].SetPosition(1, co_Instances[9].transform.position);
            co_lines[3].SetPosition(2, co_Instances[11].transform.position);
            co_lines[3].SetPosition(3, co_Instances[13].transform.position);

            // 오른발
            co_lines[4].positionCount = 4;
            co_lines[4].SetPosition(0, co_Instances[8].transform.position);
            co_lines[4].SetPosition(1, co_Instances[10].transform.position);
            co_lines[4].SetPosition(2, co_Instances[12].transform.position);
            co_lines[4].SetPosition(3, co_Instances[14].transform.position);
        }
    }

    void Start()
    {
        // 정답모델을 메인카메라 자식으로 설정
        transform.SetParent(mainCamera.transform, false);

        // 던전 셋팅(일단 허벅지 0)
        int dunNum = dungeonScene.GetComponent<TDungeonSceneManager>().receivedMonsterNumber;
        switch (dunNum)
        {
            case 0:
                setDungeon_Thigh(ref dungeonProgressPosition);
                break;
            default:
                break;
        }

        // 모델 생성
        correctModel = new CorrectModel(coParent, landmarkPrefab, linePrefab, LANDMARK_COUNT, LINES_COUNT);

        // 모델 업데이트
        
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= interval)
        {
            i++;
            elapsedTime = 0.0f;
        }

        // 3초 간격으로 랜드마크 업데이트 [6][15]

        // 랜드마크 업데이트
        if (i < 2)  //지금 0,1만 포지션 넣어놔서
        {
            correctModel.UpdateCoLandmarks(dungeonProgressPosition, i);
        }
        if (i > 2) i = 0;
    }
}
