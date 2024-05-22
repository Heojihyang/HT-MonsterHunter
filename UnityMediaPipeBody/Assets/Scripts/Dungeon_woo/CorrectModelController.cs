using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorrectModelController : MonoBehaviour
{
    // �ƿ﷿ ����
    public Camera mainCamera;                // ����ī�޶�
    public Transform coParent;               // ���� ���帶ũ���� �θ� - CorrectModel ������ �����ǵ���
    public GameObject landmarkPrefab;        // ����� ���帶ũ ������
    public GameObject linePrefab;            // ����� ���帶ũ ���� ������
    public GameObject dungeonScene;          //�� ���� ������Ʈ

    // ���帶ũ�� ���� ��
    const int LANDMARK_COUNT = 15;           // ����� ���帶ũ ��
    const int LINES_COUNT = 5;               // ����� ���帶ũ ���� ��

    // ������� ��ü��
    public CorrectModel correctModel;

    // Update ��ŸŸ�� ����
    private int i = 0;                       // ������ ����
    private float elapsedTime = 0.0f;        // �ð� ����� ������ ����
    private const float interval = 3.0f;     // ���� ���� (�� ����)

    // ������Ǵ� ������ ���帶ũ �����ǡ� - 6���� ������ ���� �� ����(�⺻�ڼ�+��ڼ�)
    public Vector3[,] dungeonProgressPosition = new Vector3[6, LANDMARK_COUNT];


    /*
    [ ����� INDEX ]
    0 : Co_HEAD
    1 : Co_LEFT_SHOULDER,   2 : Co_RIGHT_SHOULDER,   3 : Co_LEFT_ELBOW,   4 : Co_RIGHT_ELBOW
    5 : Co_LEFT_WRIST,   6 : Co_RIGHT_WRIST,   7 : Co_LEFT_HIP,   8 : Co_RIGHT_HOP
    9 : Co_LEFT_KNEE,   10 : Co_RIGHT_KNEE,   11 : Co_LEFT_ANKLE,   12 : Co_RIGHT_ANKLE
    13 : Co_LEFT_INDEX,   14 : Co_RIGHT_INDEX
    */


    // ����� ���� ����() - ��������
    public void setDungeon_Thigh(ref Vector3[,] positions)
    {
        // 1-1. ���̵� ���׷����� ���ڼ�
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

        // 1-2. ���̵� ���� ������ 
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

        // 2-1. ����Ʈ ���ڼ�

        // 2-2. ����Ʈ

        // 3-1. ���� ���ڼ�

        // 3-2. ����
        

        Debug.Log("����� ����� ���� ���� �Ϸ�");
    }

    // �ٸ� ������ �����ϼ���()
    public void setDungeon_pal(ref Vector3[,] position)
    {

    }


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
                                                         "Co_RIGHT_HIP", "Co_LEFT_KNEE", "Co_RIGHT_KNEE", "Co_LEFT_ANKLE",
                                                         "Co_RIGHT_ANKLE", "Co_LEFT_INDEX","Co_RIGHT_INDEX"};

        // ������()
        public CorrectModel(Transform coParent, GameObject landmarkPrefab, GameObject linePrefab, int LANDMARK_COUNT, int LINES_COUNT)
        {
            this.coParent = coParent;

            // ���帶ũ ������ �ʱ�ȭ - �� ������ �ִ� ���
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

            // ���帶ũ ����
            for (int i = 0; i < LANDMARK_COUNT; i++)
            {
                co_Instances[i] = Instantiate(landmarkPrefab);
                co_Instances[i].transform.localScale = Vector3.one * 0.5f;
                
                co_Instances[i].name = co_LandmarkNames[i];
                co_Instances[i].GetComponent<Transform>().position = co_LandmarkPositions[i];
                co_Instances[i].transform.parent = coParent;
            }

            // ���η����� ����
            for (int i = 0; i < LINES_COUNT; i++)
            {
                co_lines[i] = Instantiate(linePrefab).GetComponent<LineRenderer>();
            }
        }

        // ���� ���帶ũ ������Ʈ() - dpp : ��������������, rowNum : ���� ������ ����� ���ȣ
        public void UpdateCoLandmarks(Vector3[,] dpp, int rowNum)
        {
            // ���帶ũ ������Ʈ
            for (int i = 0; i < LANDMARK_COUNT; i++)
            {
                co_Instances[i].GetComponent<Transform>().position = dpp[rowNum,i];
            }

            UpdateCoLines();
            // Debug.Log(rowNum + "�� ���� ������Ʈ �Ϸ�");
        }

        // ���� ���帶ũ ���� ������Ʈ()
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

        // ���� ����(�ϴ� ����� 0)
        int dunNum = dungeonScene.GetComponent<TDungeonSceneManager>().receivedMonsterNumber;
        switch (dunNum)
        {
            case 0:
                setDungeon_Thigh(ref dungeonProgressPosition);
                break;
            default:
                break;
        }

        // �� ����
        correctModel = new CorrectModel(coParent, landmarkPrefab, linePrefab, LANDMARK_COUNT, LINES_COUNT);

        // �� ������Ʈ
        
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= interval)
        {
            i++;
            elapsedTime = 0.0f;
        }

        // 3�� �������� ���帶ũ ������Ʈ [6][15]

        // ���帶ũ ������Ʈ
        if (i < 2)  //���� 0,1�� ������ �־����
        {
            correctModel.UpdateCoLandmarks(dungeonProgressPosition, i);
        }
        if (i > 2) i = 0;
    }
}
