using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    //몬스터 10종
    public GameObject[] Monsters = new GameObject[10];

    void Start()
    {
        InitializeMonsters();
    }

    // 각 던전에 맞는 몬스터 생성
    private void InitializeMonsters()
    {

    }
}
