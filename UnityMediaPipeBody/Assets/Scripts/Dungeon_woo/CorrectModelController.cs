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
    const int LINES_COUNT = 5;             // ����� ���帶ũ ���� ��

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

    // ����� ���� ����(6�� 15��) - ���ĵ� ���̵� ���׷�����, ����Ʈ, ���� ## ���� �̾ �ؾ��ϴ� �κ� ##
    public Vector3[,] dungeon_Thigh = new Vector3[6, LANDMARK_COUNT];


    // ���� �� Ŭ����
    public class CorrectModel
    {
        // �θ� ������
        public Transform coParent;

        // ������ ���帶ũ�� ���� 
        public Vector3[] co_LandmarkPositions = new Vector3[LANDMARK_COUNT];           // ���帶ũ ��ġ ������ �迭
        public GameObject[] co_Instances = new GameObject[LANDMARK_COUNT];             // ���帶ũ ������ ������Ʈ �迭
        public LineRenderer[] co_lines = new LineRenderer[LINES_COUNT];                // ���帶ũ ���� �迭

        // ���帶ũ ���� ������(���ڷ� ������ �ִ� ���) ## ������ �����ϸ� ������� ���� ##
        public Vector3[] co_LandmarkStartPositions = new Vector3[LANDMARK_COUNT];

        // ���帶ũ �̸�
        public string[] co_LandmarkNames = new string[] {"Co_HEAD", "Co_LEFT_SHOULDER", "Co_RIGHT_SHOULDER", "Co_LEFT_ELBOW",
                                                         "Co_RIGHT_ELBOW", "Co_LEFT_WRIST", "Co_RIGHT_WRIST", "Co_LEFT_HIP",
                                                         "Co_RIGHT_HOP", "Co_LEFT_KNEE", "Co_RIGHT_KNEE", "Co_LEFT_ANKLE",
                                                         "Co_RIGHT_ANKLE", "Co_LEFT_INDEX","Co_RIGHT_INDEX"};

        // ������
        public CorrectModel(Transform coParent, GameObject landmarkPrefab, GameObject linePrefab, int LANDMARK_COUNT, int LINES_COUNT)
        {
            this.coParent = coParent;

            // ���帶ũ ������ �ʱ�ȭ
            for (int i = 0; i < LANDMARK_COUNT; i++)
            {
                co_LandmarkPositions[i] = coParent.position;
            }

            // ���帶ũ ����
            for (int i = 0; i < LANDMARK_COUNT; i++)
            {
                co_Instances[i] = Instantiate(landmarkPrefab);
                co_Instances[i].transform.localScale = Vector3.one * 0.5f;
                co_Instances[i].transform.parent = coParent;
                co_Instances[i].name = co_LandmarkNames[i];
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
            // ����
            co_lines[0].positionCount = 5;
            co_lines[0].SetPosition(0, co_Instances[1].transform.position);
            co_lines[0].SetPosition(1, co_Instances[2].transform.position);
            co_lines[0].SetPosition(2, co_Instances[8].transform.position);
            co_lines[0].SetPosition(3, co_Instances[7].transform.position);
            co_lines[0].SetPosition(4, co_Instances[1].transform.position);

            // ����
            co_lines[1].positionCount = 3;
            co_lines[1].SetPosition(0, co_Instances[1].transform.position);
            co_lines[1].SetPosition(1, co_Instances[3].transform.position);
            co_lines[1].SetPosition(2, co_Instances[5].transform.position);

            // ������
            co_lines[2].positionCount = 3;
            co_lines[2].SetPosition(0, co_Instances[2].transform.position);
            co_lines[2].SetPosition(1, co_Instances[4].transform.position);
            co_lines[2].SetPosition(2, co_Instances[6].transform.position);

            // �޹�
            co_lines[3].positionCount = 4;
            co_lines[3].SetPosition(0, co_Instances[7].transform.position);
            co_lines[3].SetPosition(1, co_Instances[9].transform.position);
            co_lines[3].SetPosition(2, co_Instances[11].transform.position);
            co_lines[3].SetPosition(3, co_Instances[13].transform.position);

            // ������
            co_lines[4].positionCount = 4;
            co_lines[4].SetPosition(0, co_Instances[8].transform.position);
            co_lines[4].SetPosition(1, co_Instances[10].transform.position);
            co_lines[4].SetPosition(2, co_Instances[12].transform.position);
            co_lines[4].SetPosition(3, co_Instances[14].transform.position);
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
