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
    const int LINES_COUNT = 11;             // 정답모델 랜드마크 라인 수

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

    // 던전 로직
    


    // 정답 모델 클래스
    public class CorrectModel
    {
        // 부모 포지션
        public Transform coParent;

        // 생성될 랜드마크와 라인 
        public Vector3[] co_LandmarkPositions = new Vector3[LANDMARK_COUNT];           // 랜드마크 위치 포지션 배열
        public GameObject[] co_Instances = new GameObject[LANDMARK_COUNT];             // 랜드마크 프리팹 오브젝트 배열
        public LineRenderer[] co_lines = new LineRenderer[LINES_COUNT];                // 랜드마크 라인 배열

        // 생성자
        public CorrectModel(Transform coParent, GameObject landmarkPrefab, GameObject linePrefab, int LANDMARK_COUNT, int LINES_COUNT)
        {
            this.coParent = coParent;

            // 랜드마크 포지션 초기화
            for (int i = 0; i < LANDMARK_COUNT; i++)
            {
                co_LandmarkPositions[i] = new Vector3(0, 0, 0);
            }

            // 랜드마크 생성
            for (int i = 0; i < LANDMARK_COUNT; i++)
            {
                co_Instances[i] = Instantiate(landmarkPrefab);
                co_Instances[i].transform.parent = coParent;
                co_Instances[i].name = "correctRandmarkNameTest";
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
