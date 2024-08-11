using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// ���� �����տ� ����
public class MonsterController : MonoBehaviour
{
    public Camera mainCamera;

    public GameObject[] Monsters = new GameObject[10];  // ���� 10�� ������
    public GameObject monster;                          // ������ ����
    public Animator animator;                           // ������ ������ �ִϸ�����
    public float maxHealth = 100f;                      // ���� ��ü ü��
    private float currentHealth;                        // ���� ���� ü��

    public GameObject dungeonScene; //�� ���� ������Ʈ

    private void Start()
    {
        transform.SetParent(mainCamera.transform, false);
        Debug.Log("ī�޶�(�θ�) - ����(�ڽ�) ���� ���� ����");

        int monNum = PlayerPrefs.GetInt("MonsterNumberToSend");
        //Debug.Log("���� ��ȣ " +  monNum);
        CreateMonster(monNum);                          // ���� ����
        currentHealth = maxHealth;                      // ü�� �ʱ�ȭ
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