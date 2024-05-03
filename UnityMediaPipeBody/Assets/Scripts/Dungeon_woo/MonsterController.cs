using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MonsterController : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    // 데미지를 입히는 함수
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log("아파라!");

        // 현재 체력이 0 이하인지 확인하여 사망 처리
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // 사망 처리 함수
    private void Die()
    {
        // 여기에 사망 처리 로직 추가
        Debug.Log("꿱");
        Destroy(gameObject); // 현재 게임 오브젝트 파괴
    }
}