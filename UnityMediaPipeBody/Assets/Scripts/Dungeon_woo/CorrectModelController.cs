using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorrectModelController : MonoBehaviour
{
    // 아울렛 접속
    public Camera mainCamera;               // 메인카메라
    public Transform coParent;              // 정답 랜드마크들의 부모 - CorrectModel 하위에 생성되도록
    public GameObject landmarkPrefab;       // 정답모델 랜드마크 프리팹
    public GameObject linePrefab;           // 정답모델 랜드마크 라인 프리팹

    // 랜드마크와 라인 수
    const int LANDMARK_COUNT = 15;          // 정답모델 랜드마크 수
    const int LINES_COUNT = 5;             // 정답모델 랜드마크 라인 수

    // ★정답모델 객체★
    public CorrectModel correctModel;

    /*
    [ 정답모델 INDEX ]
    0 : Co_HEAD
    1 : Co_LEFT_SHOULDER,   2 : Co_RIGHT_SHOULDER,   3 : Co_LEFT_ELBOW,   4 : Co_RIGHT_ELBOW
    5 : Co_LEFT_WRIST,   6 : Co_RIGHT_WRIST,   7 : Co_LEFT_HIP,   8 : Co_RIGHT_HOP
    9 : Co_LEFT_KNEE,   10 : Co_RIGHT_KNEE,   11 : Co_LEFT_ANKLE,   12 : Co_RIGHT_ANKLE
    13 : Co_LEFT_INDEX,   14 : Co_RIGHT_INDEX
    */

    // 허벅지 던전 로직(6행 15열) - 스탠딩 사이드 레그레이즈, 스쿼트, 런지 ## 개발 이어서 해야하는 부분 ##
    public Vector3[,] dungeon_Thigh = new Vector3[6, LANDMARK_COUNT];


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
                                                         "Co_RIGHT_HOP", "Co_LEFT_KNEE", "Co_RIGHT_KNEE", "Co_LEFT_ANKLE",
                                                         "Co_RIGHT_ANKLE", "Co_LEFT_INDEX","Co_RIGHT_INDEX"};

        // 생성자
        public CorrectModel(Transform coParent, GameObject landmarkPrefab, GameObject linePrefab, int LANDMARK_COUNT, int LINES_COUNT)
        {
            this.coParent = coParent;

            // 랜드마크 포지션 초기화
            for (int i = 0; i < LANDMARK_COUNT; i++)
            {
                co_LandmarkPositions[i] = coParent.position;
            }

            // 랜드마크 생성
            for (int i = 0; i < LANDMARK_COUNT; i++)
            {
                co_Instances[i] = Instantiate(landmarkPrefab);
                co_Instances[i].transform.localScale = Vector3.one * 0.5f;
                co_Instances[i].transform.parent = coParent;
                co_Instances[i].name = co_LandmarkNames[i];
                co_Instances[i].GetComponent<Transform>().position = co_LandmarkPositions[i];
            }

            // 라인렌더러 생성
            for (int i = 0; i < LINES_COUNT; i++)
            {
                co_lines[i] = Instantiate(linePrefab).GetComponent<LineRenderer>();
            }

            Debug.Log("정답 모델 생성자 작동 완료");
        }

        // 정답 랜드마크 업데이트
        public void UpdateCoLandmarks()
        {

        }

        // 정답 랜드마크 라인 업데이트
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
        Debug.Log("카메라(부모) - 정답모델(자식) 관계 설정 성공");

        // 모델 생성
        correctModel = new CorrectModel(coParent, landmarkPrefab, linePrefab, LANDMARK_COUNT, LINES_COUNT);
    }

    void Update()
    {
        //correctModel.UpdateCoLandmarks();
    }
}
