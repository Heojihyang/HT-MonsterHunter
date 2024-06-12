using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MonListManager : MonoBehaviour
{
    public GameObject MonsterInfo;
    public Image MonsterImage; // 이미지 컴포넌트
    public GameObject MonsterPartImg;
    public Text MonsterName;
    public Text MonsterPart;
    public Text Button_L; // 상체운동을 해야하는 이유
    public Text Button_R; // 상체운동 추천 

    public GameObject PopUp_L; 
    public GameObject PopUp_R;
    public Text Title_L;
    public Text Title_R;
    public GameObject L_upper;
    public GameObject L_lower;
    public GameObject R_upper;
    public GameObject R_lower;

    public GameObject popupImage; // "아직잡지않은몬스터임"

    public List<MonsterListData> monsterListData;

    MonsterData Monsterdata = new MonsterData(); // GameData.cs

    // SpriteRenderer 컴포넌트를 가져오기 위한 변수
    private SpriteRenderer spriteRenderer;
    public Color ActiveColor = Color.white;

    // RGB 값을 이용해 색상을 생성. (16진수 676767)
    Color OriginColor = new Color32(0x67, 0x67, 0x67, 0xFF);

    private bool isPartLower ; // 하체 몬스터인가? 

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
        // ID = id; // 현재 활성화된 몬스터의ID 

        // 상체/하체 몬스터 구분 
        if (id >= 0 && id <= 6)
        {
            isPartLower = false;
        }
        else if (id >= 7 && id <= 9)
        {
            isPartLower = true;
        }


        bool monsterUnLooked = GameData.instance.monsterdata.MonsterUnLocked[id];

        // 수집완료 몬스터인가?
        if (monsterUnLooked)
        {
            MonsterInfo.SetActive(true);

            // --- 데이터 가져와서 넣어주기 1~4 ---

            // 1. 몬스터 이미지 
            Sprite MonsterImg = monsterListData[id].MonTrueImage; // 가져오기

            // 이미지의 원본 비율 가져오기
            float aspectRatio = (float)MonsterImg.texture.width / MonsterImg.texture.height;

            // UI 요소의 RectTransform 컴포넌트 가져오기
            RectTransform rectTransform = MonsterImage.GetComponent<RectTransform>();

            // 이미지 비율에 따라 UI 요소의 크기 조정
            rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, rectTransform.sizeDelta.x / aspectRatio);

            // 이미지 넣기 
            MonsterImage.sprite = MonsterImg;


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

            // SpriteRenderer가 존재하는지 확인
            if (spriteRenderer != null)
            {
                // SpriteRenderer의 색상 변경
                spriteRenderer.color = ActiveColor;
            }

            // -------

            // 하단 버튼 텍스트 업데이트

            if (id >= 0 && id <= 6)
            {
                Button_L.text = "상체운동을\n해야하는 이유";
                Button_R.text = "추천하는\n상체 운동";
            }
            else if (id >= 7 && id <= 9)
            {
                Button_L.text = "하체운동을\n해야하는 이유";
                Button_R.text = "추천하는\n하체 운동";
            }
            else
            {
                Button_L.text = "알 수 없는\n운동 유형";
                Button_R.text = "알 수 없는\n운동";
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

    // 상세정보에서 뒤로가기 버튼 클릭
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

    // 상세정보에서 팝업 버튼 클릭

    // 00운동을 해야하는 이유
    public void ActPopUp_L() 
    {
        MonsterPartImg.SetActive(false);
        PopUp_L.SetActive(true);

        if (!isPartLower) // 상체몬스터
        {
            print("상체 몬스터 입니다.");
            Title_L.text = "상체운동을 해야하는이유";
            L_upper.SetActive(true);
        }
        else // 하체몬스터 
        {
            print("하체 몬스터 입니다.");
            Title_L.text = "하체운동을 해야하는이유";
            L_lower.SetActive(true);
        }
    }

    // 추천하는 00운동
    public void ActPopUp_R() 
    {
        MonsterPartImg.SetActive(false);
        PopUp_R.SetActive(true);

        if (!isPartLower) // 상체몬스터
        {
            print("상체 몬스터 입니다.");
            Title_R.text = "추천하는 상체운동";
            R_upper.SetActive(true);
        }
        else // 하체몬스터 
        {
            print("하체 몬스터 입니다.");
            Title_R.text = "추천하는 하체운동";
            R_lower.SetActive(true);
        }
    }

    // 팝업에서 뒤로가기 버튼 클릭 
    public void InActPopUp()
    {
        MonsterPartImg.SetActive(true);

        // 활성화 하면서 띄웠던 것들 다 닫기 
        L_upper.SetActive(false);
        L_lower.SetActive(false);
        R_upper.SetActive(false);
        R_lower.SetActive(false);
        PopUp_R.SetActive(false);
        PopUp_L.SetActive(false);


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