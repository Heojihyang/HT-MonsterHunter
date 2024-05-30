using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MonListManager : MonoBehaviour
{
    public GameObject MonsterInfo;
    public GameObject MonsterImage;
    public GameObject MonsterPartImg;
    public Text MonsterName;
    public Text MonsterPart;

    public GameObject popupImage; // "아직잡지않은몬스터임"

    public List<MonsterListData> monsterListData;

    MonsterData Monsterdata = new MonsterData(); // GameData.cs

    // SpriteRenderer 컴포넌트를 가져오기 위한 변수
    private SpriteRenderer spriteRenderer;
    public Color ActiveColor = Color.white;

    // RGB 값을 이용해 색상을 생성. (16진수 676767)
    Color OriginColor = new Color32(0x67, 0x67, 0x67, 0xFF);

    // 몬스터 수집 여부 시각화 -> 게임오브젝트의 이미지 변경 시키기 
    void Start()
    {
        // --- 데이터 저장 예시 (노션 확인) ---
        // GameData.instance.LoadMonsterData();
        // GameData.instance.monsterdata.MonsterName[0] = "0번 몬스터";
        // GameData.instance.monsterdata.MonsterUnLocked[0] = true;
        // GameData.instance.SaveMonsterData();
        // ---

        // 총 몬스터 수 임시 
        for (int id = 0; id < 10; id++)
        {
            // 몬스터 수집 여부 확인 
            bool haveMonster = GameData.instance.monsterdata.MonsterUnLocked[id];

            if (haveMonster) 
            {
                // monsterListData 리스트에서 동일한 ID찾기
                int monsterId = monsterListData.FindIndex(data => data.ID == id);

                // 해당 ID의 GameObject와 Sprite 가져와서
                GameObject FalseImage = monsterListData[monsterId].MonFalseImage;
                Sprite TrueImage = monsterListData[monsterId].MonTrueImage;

                // GameObject의 FalseImage를 TrueImage로 변경해주기
                FalseImage.GetComponent<Image>().sprite = TrueImage;

            }
          
        }
        
    }

    // 수집된 몬스터 클릭 시 설명창 활성화
    // 해당 버튼의 몬스터 ID 넘겨주고 정보 연결시켜주기 
    public void ActMonsterInfo(int id)
    {
        bool monsterUnLooked = GameData.instance.monsterdata.MonsterUnLocked[id];

        // 수집완료 몬스터인가?
        if (monsterUnLooked)
        {
            MonsterInfo.SetActive(true);

            // --- 데이터 가져와서 넣어주기 1~4 ---

            // 1. 몬스터 이미지 
            Sprite MonsterImg = monsterListData[id].MonTrueImage; // 가져오기
            MonsterImage.GetComponent<Image>().sprite = MonsterImg; // 넣어주기 (UI조정 필요: 노션확인)

            // 몬스터 데이터 로드
            // GameData.instance.LoadMonsterData();

            // 2. 몬스터 이름 : 몬스터 데이터 사용 
            string monstername = GameData.instance.monsterdata.MonsterName[id];
            MonsterName.text = monstername;

            // 3. 몬스터 발생 부위
            string monsterpart = monsterListData[id].MonsterPart;
            MonsterPart.text = monsterpart;

            // 4. 몬스터 발생 부위 이미지 
            Sprite monsterpartImg = monsterListData[id].MonsterPartImage;
            MonsterPartImg.GetComponent<Image>().sprite = monsterpartImg;

            // 4. 몬스터 발생 부위 활성화
            GameObject monsterobj = monsterListData[id].MonsterPartObj;

            // 현재 게임 오브젝트에서 SpriteRenderer 컴포넌트를 가져옵니다.
            spriteRenderer = monsterobj.GetComponent<SpriteRenderer>();

            // SpriteRenderer가 존재하는지 확인합니다.
            if (spriteRenderer != null)
            {
                // SpriteRenderer의 색상을 변경합니다.
                spriteRenderer.color = ActiveColor;
            }


        }
        else
        {
            // "아직 잡지않은 몬스터입니다." 띄우기

            // 팝업 이미지를 활성화
            popupImage.SetActive(true);

            // 2초 후에 팝업 이미지를 비활성화하는 코루틴 시작
            StartCoroutine(HidePopupAfterDelay(1f));
        }
        
    }

    // 일정 시간 후 팝업 이미지를 비활성화하는 코루틴
    private IEnumerator HidePopupAfterDelay(float delay)
    {
        // 지정한 시간(초) 동안 대기
        yield return new WaitForSeconds(delay);

        // 팝업 이미지를 비활성화
        popupImage.SetActive(false);
    }

    // 도감에서 뒤로가기 버튼 클릭
    public void ChangeMainScene()
    {
        SceneManager.LoadScene("MainScene");
    }

    // 팝업에서 뒤로가기 버튼 클릭
    public void InActMonsterInfo()
    {
        MonsterInfo.SetActive(false);

        // 비어있지 않으면 
        if (spriteRenderer != null)
        {
            // SpriteRenderer의 색상을 변경합니다.
            spriteRenderer.color = OriginColor;
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

    [field: SerializeField]
    public string MonsterPart { get; private set; } // 몬스터 발생 부위

    [field: SerializeField]
    public Sprite MonsterPartImage { get; private set; } // 몬스터 발생 부위 이미지

    [field: SerializeField]
    public GameObject MonsterPartObj { get; private set; } // 몬스터 발생 부위 이미지 활성화 


}