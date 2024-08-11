using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


// ���� �����տ� ����
public class MonsterController : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject dungeonScene; //�� ���� ������Ʈ
    public int monNum;              // �Ѱܹ޾� ������ ���� ��ȣ

    [SerializeField] public GameObject[] Monsters = new GameObject[10];  // ���� 10�� ������
    [SerializeField] public GameObject monster;                          // ������ ����
    [SerializeField] public Animator animator;                           // ������ ������ �ִϸ�����

    private void Start()
    {
        transform.SetParent(mainCamera.transform, false);
        Debug.Log("ī�޶�(�θ�) - ����(�ڽ�) ���� ���� ����");

        monNum = PlayerPrefs.GetInt("MonsterNumberToSend");
        //Debug.Log("���� ��ȣ " +  monNum);
        CreateMonster(monNum);                          // ���� ����
    }

    /// ���� ����
    public void CreateMonster(int monsterNumber) 
    {
        // �θ� ������Ʈ
        Vector3 parentPosition = this.transform.position;
        Quaternion parentRotation = this.transform.rotation;

        // �������� �����ͼ� ���� ���� ������Ʈ�� �ֱ�
        GameObject monsterPrefab = Monsters[monsterNumber];
        monster = Instantiate(monsterPrefab, parentPosition, parentRotation);

        // �θ�-�ڽ� ����
        monster.transform.SetParent(this.transform);

        // ������ ������ �ִϸ��̼� ���°� �ʱ�ȭ
        animator = monster.GetComponent<Animator>();
        animator.SetBool("ani_Damage", false);
        animator.SetBool("ani_Die", false);
    }
}