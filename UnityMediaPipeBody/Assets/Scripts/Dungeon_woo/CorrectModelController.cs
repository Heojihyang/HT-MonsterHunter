using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorrectModelController : MonoBehaviour
{
    // �ƿ﷿ ����
    public Camera mainCamera;               // ����ī�޶�
    public Transform coParent;              // ���� ���帶ũ���� �θ� - CorrectModel ������ �����ǵ���
    public GameObject landmarkPrefab;       // ����� ���帶ũ ������
    public GameObject linePrefab;           // ����� ���帶ũ ���� ������

    // ���帶ũ�� ���� ��
    const int LANDMARK_COUNT = 15;          // ����� ���帶ũ ��
    const int LINES_COUNT = 11;             // ����� ���帶ũ ���� ��

    // ������� ��ü��
    public CorrectModel correctModel;

    /*
    [ ����� INDEX ]
    0 : Co_HEAD
    1 : Co_LEFT_SHOULDER,   2 : Co_RIGHT_SHOULDER,   3 : Co_LEFT_ELBOW,   4 : Co_RIGHT_ELBOW
    5 : Co_LEFT_WRIST,   6 : Co_RIGHT_WRIST,   7 : Co_LEFT_HIP,   8 : Co_RIGHT_HOP
    9 : Co_LEFT_KNEE,   10 : Co_RIGHT_KNEE,   11 : Co_LEFT_ANKLE,   12 : Co_RIGHT_ANKLE
    13 : Co_LEFT_INDEX,   14 : Co_RIGHT_INDEX
    */

    // ���� ����
    


    // ���� �� Ŭ����
    public class CorrectModel
    {
        // �θ� ������
        public Transform coParent;

        // ������ ���帶ũ�� ���� 
        public Vector3[] co_LandmarkPositions = new Vector3[LANDMARK_COUNT];           // ���帶ũ ��ġ ������ �迭
        public GameObject[] co_Instances = new GameObject[LANDMARK_COUNT];             // ���帶ũ ������ ������Ʈ �迭
        public LineRenderer[] co_lines = new LineRenderer[LINES_COUNT];                // ���帶ũ ���� �迭

        // ������
        public CorrectModel(Transform coParent, GameObject landmarkPrefab, GameObject linePrefab, int LANDMARK_COUNT, int LINES_COUNT)
        {
            this.coParent = coParent;

            // ���帶ũ ������ �ʱ�ȭ
            for (int i = 0; i < LANDMARK_COUNT; i++)
            {
                co_LandmarkPositions[i] = new Vector3(0, 0, 0);
            }

            // ���帶ũ ����
            for (int i = 0; i < LANDMARK_COUNT; i++)
            {
                co_Instances[i] = Instantiate(landmarkPrefab);
                co_Instances[i].transform.parent = coParent;
                co_Instances[i].name = "correctRandmarkNameTest";
                co_Instances[i].GetComponent<Transform>().position = co_LandmarkPositions[i];
            }

            // ���η����� ����
            for (int i = 0; i < LINES_COUNT; i++)
            {
                co_lines[i] = Instantiate(linePrefab).GetComponent<LineRenderer>();
            }

            Debug.Log("���� �� ������ �۵� �Ϸ�");
        }

        // ���� ���帶ũ ������Ʈ
        public void UpdateCoLandmarks()
        {

        }

        // ���� ���帶ũ ���� ������Ʈ
        public void UpdateCoLines()
        {

        }
    }


    

    void Start()
    {
        // ������� ����ī�޶� �ڽ����� ����
        transform.SetParent(mainCamera.transform, false);
        Debug.Log("ī�޶�(�θ�) - �����(�ڽ�) ���� ���� ����");

        // �� ����
        correctModel = new CorrectModel(coParent, landmarkPrefab, linePrefab, LANDMARK_COUNT, LINES_COUNT);
    }

    void Update()
    {
        //correctModel.UpdateCoLandmarks();
    }
}
