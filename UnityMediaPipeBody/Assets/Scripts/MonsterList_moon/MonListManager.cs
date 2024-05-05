using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MonListManager : MonoBehaviour
{
    public List<MonsterListData> monsterListData;

    MonsterData Monsterdata = new MonsterData(); // GameData.cs

    GameData gamedata = new GameData();
    // public GameObject gamedata;

    public GameObject MonsterInfo;
    public GameObject MonsterImage;
    public Text MonsterName;


    // 몬스터 수집 여부 시각화 -> 게임오브젝트의 이미지 변경 시키기 
    void Start()
    {
        // 테스트 해보기 
        Monsterdata.MonsterUnLocked[0] = true;
        Monsterdata.MonsterUnLocked[1] = true;

        // 총 몬스터 수 임시 3
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
            else
            {
                print(id + "는 아직 보유하지 못한 몬스터 입니다.");
            }
          
        }
        
    }

    // 수집된 몬스터 클릭 시 설명창 활성화
    // 해당 버튼의 몬스터 ID 넘겨주고 정보 연결시켜주기 
    public void ActMonsterInfo(int ID)
    {
        MonsterInfo.SetActive(true);

        // --- 데이터 가져와서 넣어주기 ---
        // 1. 몬스터 이미지 : MonsterListData의 True 이미지 가져오기 
        // 2. 몬스터 이름 : MonsterData의 이름 가져와서 Text에 넣어주기
        // 3. 몬스터 발생 부위 -> ID에 따라 구분한 데이터 구현해놓고 거기서 가져오기 
        // 4. 몬스터 발생 부위 이미지 

        // 1. 몬스터 이미지 
        Sprite MonsterImg = monsterListData[ID].MonTrueImage; // 가져오기
        MonsterImage.GetComponent<Image>().sprite = MonsterImg; // 넣어주기 (UI조정 필요: 노션확인)

        // 몬스터 데이터 로드
        GameData.instance.LoadMonsterData(); 

        // 2. 몬스터 이름 : 몬스터 데이터 사용 
        string monstername = GameData.instance.monsterdata.MonsterName[ID];
        MonsterName.text = monstername;

        // 3. 몬스터 발생 부위


    }

    // 뒤로가기 버튼 클릭
    public void InActMonsterInfo()
    {
        MonsterInfo.SetActive(false);
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

    [field: SerializeField]
    public string MonsterPart { get; private set; } // 몬스터 발생 부위

    [field: SerializeField]
    public Sprite MonsterPartImage { get; private set; } // 몬스터 발생 부위 이미지

}
