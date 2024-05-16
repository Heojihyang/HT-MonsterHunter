using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        int monNum = dungeonScene.GetComponent<TDungeonSceneManager>().receivedMonsterNumber;
        CreateMonster(monNum);                          // ���� ����
        currentHealth = maxHealth;                      // ü�� �ʱ�ȭ
    }

    // ���� ���� �Լ�
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
    
    // ������ �Լ�
    public void TakeDamage(float damage)
    {
        animator.SetBool("ani_Damage", true);
        currentHealth -= damage;
        Debug.Log("���Ķ�!");

        // ���� ü���� 0 ���϶�� ��� ó��
        if (currentHealth <= 0)
        {
            Die();
        }
        //yield return new WaitForSeconds(2.0f);
        //animator.SetBool("ani_Damage", false);
    }
    
    // ���ó�� �Լ�
    private void Die()
    {
        animator.SetBool("ani_DIe", true);
        Debug.Log("��");
        // Destroy(monster);
    }
}