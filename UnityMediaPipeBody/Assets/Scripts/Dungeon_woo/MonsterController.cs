using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


// 몬스터 프리팹에 적용
public class MonsterController : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject dungeonScene; //씬 관리 오브젝트
    public int monNum;              // 넘겨받아 생성할 몬스터 번호

    [SerializeField] public GameObject[] Monsters = new GameObject[10];  // 몬스터 10종 프리팹
    [SerializeField] public GameObject monster;                          // 생성된 몬스터
    [SerializeField] public Animator animator;                           // 생성된 몬스터의 애니메이터

    private void Start()
    {
        transform.SetParent(mainCamera.transform, false);
        Debug.Log("카메라(부모) - 몬스터(자식) 관계 설정 성공");

        monNum = PlayerPrefs.GetInt("MonsterNumberToSend");
        //Debug.Log("몬스터 번호 " +  monNum);
        CreateMonster(monNum);                          // 몬스터 생성
    }

    /// 몬스터 생성
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
}