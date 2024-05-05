using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject monster;
    public float damage = 7.5f;

    private void Update()
    {
        // 스페이스바를 눌렀을 때 공격하는 동작을 감지
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
    }

    // 몬스터 공격 함수
    private void Attack()
    {
        monster.GetComponent<MonsterController>().TakeDamage(damage);
    }

    // 현재 랜드마크 위치 가져오기
    private void getLandmarkPosition()
    {
        /*
        GetComponent<PipeServer>().instances;
        for (int i = 0; i < instances.Length; i++)
        {
            Debug.Log("Array Element " + i + ": " + array[i]);
        }
        */
    }
}