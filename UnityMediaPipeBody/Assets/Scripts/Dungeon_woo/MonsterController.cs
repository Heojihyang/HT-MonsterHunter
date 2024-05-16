using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 몬스터 프리팹에 적용
public class MonsterController : MonoBehaviour
{
    public Camera mainCamera;

    public GameObject[] Monsters = new GameObject[10];  // 몬스터 10종 프리팹
    public GameObject monster;                          // 생성된 몬스터
    public Animator animator;                           // 생성된 몬스터의 애니메이터
    public float maxHealth = 100f;                      // 몬스터 전체 체력
    private float currentHealth;                        // 몬스터 현재 체력

    public GameObject dungeonScene; //씬 관리 오브젝트

    private void Start()
    {
        transform.SetParent(mainCamera.transform, false);
        Debug.Log("카메라(부모) - 몬스터(자식) 관계 설정 성공");

        int monNum = dungeonScene.GetComponent<TDungeonSceneManager>().receivedMonsterNumber;
        CreateMonster(monNum);                          // 몬스터 생성
        currentHealth = maxHealth;                      // 체력 초기화
    }

    // 몬스터 생성 함수
    public void CreateMonster(int monsterNumber) 
    {
        // 부모 오브젝트
        Vector3 parentPosition = this.transform.position;
        Quaternion parentRotation = this.transform.rotation;

        // 프리팹을 가져와서 공식 몬스터 오브젝트에 넣기
        GameObject monsterPrefab = Monsters[monsterNumber];
        monster = Instantiate(monsterPrefab, parentPosition, parentRotation);

        // 부모-자식 설정
        monster.transform.SetParent(this.transform);

        // 생성된 몬스터의 애니메이션 상태값 초기화
        animator = monster.GetComponent<Animator>();
        animator.SetBool("ani_Damage", false);
        animator.SetBool("ani_Die", false);
    }
    
    // 데미지 함수
    public void TakeDamage(float damage)
    {
        animator.SetBool("ani_Damage", true);
        currentHealth -= damage;
        Debug.Log("아파라!");

        // 현재 체력이 0 이하라면 사망 처리
        if (currentHealth <= 0)
        {
            Die();
        }
        //yield return new WaitForSeconds(2.0f);
        //animator.SetBool("ani_Damage", false);
    }
    
    // 사망처리 함수
    private void Die()
    {
        animator.SetBool("ani_DIe", true);
        Debug.Log("꿱");
        // Destroy(monster);
    }
}