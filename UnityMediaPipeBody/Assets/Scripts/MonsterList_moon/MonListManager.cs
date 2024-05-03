using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonListManager : MonoBehaviour
{
    public List<MonsterListData> monsterListData;

    MonsterData Monsterdata = new MonsterData(); // GameData.cs

    // 몬스터 수집 여부 시각화 -> 게임오브젝트의 이미지 변경 시키기 
    void Start()
    {
        // 테스트 해보기 
        Monsterdata.MonsterUnLocked[0] = true;
        Monsterdata.MonsterUnLocked[1] = true;

        // 총 몬스터 수 3
        for (int id = 0; id < 3; id++)
        {
            // 몬스터 수집 여부 확인 
            bool haveMonster = Monsterdata.MonsterUnLocked[id];

            if (haveMonster) 
            {
                // monsterListData 리스트에서 동일한 ID찾기
                int monsterId = monsterListData.FindIndex(data => data.ID == id);

                // 해당 ID의 GameObject와 Sprite 가져와서
                GameObject FalseImage = monsterListData[monsterId].MonFalseImage;
                Sprite TrueImage = monsterListData[monsterId].MonTrueImage;

                // GameObject의 FalseImage를 TrueImage로 변경해주기
                FalseImage.GetComponent<Image>().sprite = TrueImage;

                print(id + "는 보유한 몬스터 입니다.");
            }

            print(id + "는 아직 보유하지 못한 몬스터 입니다.");

        }
                
    }

}

// 몬스터 도감 시각화 관련 데이터 
[Serializable]
public class MonsterListData
{
    [field: SerializeField]
    public int ID { get; private set; } // MonsterData의 배열 ID와 연결

    [field: SerializeField]
    public GameObject MonFalseImage { get; private set; } // 수집X 표시

    [field: SerializeField]
    public Sprite MonTrueImage { get; private set; } // 수집O 표시 
}