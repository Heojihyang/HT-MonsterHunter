using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MonListManager : MonoBehaviour
{
    public GameObject MonsterInfo;
    public Image MonsterImage;           // �̹��� ������Ʈ
    public GameObject MonsterPartImg;
    public Text MonsterName;
    public Text MonsterPart;
    public Text Button_L;                // ��ü��� �ؾ��ϴ� ����
    public Text Button_R;                // ��ü� ��õ 

    public GameObject PopUp_L; 
    public GameObject PopUp_R;
    public Text Title_L;
    public Text Title_R;
    public GameObject L_upper;
    public GameObject L_lower;
    public GameObject R_upper;
    public GameObject R_lower;
    public GameObject popupImage;        // "������������������"

    public List<MonsterListData> monsterListData;
    MonsterData Monsterdata = new MonsterData();               // GameData.cs

    private SpriteRenderer spriteRenderer;                     // SpriteRenderer ������Ʈ�� �������� ���� ����
    public Color ActiveColor = Color.white;

    Color OriginColor = new Color32(0x67, 0x67, 0x67, 0xFF);   // RGB ���� �̿��� ������ ����. (16���� 676767)

    private bool isPartLower ;                                 // ��ü �����ΰ�? 



    // ���� ���� ���� �ð�ȭ -> ���ӿ�����Ʈ�� �̹��� ���� ��Ű�� 
    void Start()
    {

        #region --- ������ ���� ���� (��� Ȯ��) ---
        // GameData.instance.LoadMonsterData();
        // GameData.instance.monsterdata.MonsterName[0] = "0�� ����";
        // GameData.instance.monsterdata.MonsterUnLocked[0] = true;
        // GameData.instance.SaveMonsterData();
        #endregion



        for (int id = 0; id < 10; id++)   // �� ���� �� 10
        {
            bool haveMonster = GameData.instance.monsterdata.MonsterUnLocked[id];   // ���� ���� ���� Ȯ�� 

            if (haveMonster) 
            {
                int monsterId = monsterListData.FindIndex(data => data.ID == id);    // monsterListData ����Ʈ���� ������ IDã��

                GameObject FalseImage = monsterListData[monsterId].MonFalseImage;    // �ش� ID�� GameObject�� Sprite �����ͼ�
                Sprite TrueImage = monsterListData[monsterId].MonTrueImage;

                FalseImage.GetComponent<Image>().sprite = TrueImage;                 // GameObject�� FalseImage�� TrueImage�� �������ֱ�
            }
        }
    }



    // ������ ���� Ŭ�� �� ����â Ȱ��ȭ : �ش� ��ư�� ���� ID �Ѱ��ְ� ���� ��������ֱ� 
    public void ActMonsterInfo(int id)
    {
        // ��/��ü ���� ���� 
        if (id >= 0 && id <= 6)
        {
            isPartLower = false;
        }
        else if (id >= 7 && id <= 9)
        {
            isPartLower = true;
        }

        bool monsterUnLooked = GameData.instance.monsterdata.MonsterUnLocked[id];


        // �����Ϸ� �����ΰ�?
        if (monsterUnLooked)
        {
            MonsterInfo.SetActive(true);


            // --- ������ �����ͼ� �־��ֱ� 1~4 ---


            // 1. ���� �̹��� 
            Sprite MonsterImg = monsterListData[id].MonTrueImage; // ��������

            float aspectRatio = (float)MonsterImg.texture.width / MonsterImg.texture.height;    // �̹����� ���� ���� ��������

            RectTransform rectTransform = MonsterImage.GetComponent<RectTransform>();           // UI ����� RectTransform ������Ʈ ��������

            rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, rectTransform.sizeDelta.x / aspectRatio);  // �̹��� ������ ���� UI ����� ũ�� ����

            MonsterImage.sprite = MonsterImg;    // �̹��� �ֱ� 



            // 2. ���� �̸� : ���� ������ ��� 
            string monstername = GameData.instance.monsterdata.MonsterName[id];
            MonsterName.text = monstername;



            // 3. ���� �߻� ����
            string monsterpart = monsterListData[id].MonsterPart;
            MonsterPart.text = monsterpart;



            // 4. ���� �߻� ���� �̹��� 
            Sprite monsterpartImg = monsterListData[id].MonsterPartImage;
            MonsterPartImg.GetComponent<Image>().sprite = monsterpartImg;

            GameObject monsterobj = monsterListData[id].MonsterPartObj;     // ���� �߻� ���� Ȱ��ȭ

            spriteRenderer = monsterobj.GetComponent<SpriteRenderer>();     // ���� ���� ������Ʈ�� SpriteRenderer ������Ʈ�� ��������.

            

            if (spriteRenderer != null)                // SpriteRenderer�� �����ϴ��� Ȯ��
            {
                spriteRenderer.color = ActiveColor;    // SpriteRenderer�� ���� ����
            }



            // -------
            // �ϴ� ��ư �ؽ�Ʈ ������Ʈ

            if (id >= 0 && id <= 6)
            {
                Button_L.text = "��ü���\n�ؾ��ϴ� ����";
                Button_R.text = "��õ�ϴ�\n��ü �";
            }
            else if (id >= 7 && id <= 9)
            {
                Button_L.text = "��ü���\n�ؾ��ϴ� ����";
                Button_R.text = "��õ�ϴ�\n��ü �";
            }
            else
            {
                Button_L.text = "�� �� ����\n� ����";
                Button_R.text = "�� �� ����\n�";
            }



        }
        else   // "���� �������� �����Դϴ�." ����
        {
            popupImage.SetActive(true);                // �˾� �̹����� Ȱ��ȭ

            StartCoroutine(HidePopupAfterDelay(1f));   // 2�� �Ŀ� �˾� �̹����� ��Ȱ��ȭ�ϴ� �ڷ�ƾ ����
        }
        
    }



    // ���� �ð� �� �˾� �̹����� ��Ȱ��ȭ�ϴ� �ڷ�ƾ
    private IEnumerator HidePopupAfterDelay(float delay)
    {
        
        yield return new WaitForSeconds(delay);     // ������ �ð�(��) ���� ���

        popupImage.SetActive(false);                // �˾� �̹����� ��Ȱ��ȭ
    }



    // �������� �ڷΰ��� ��ư Ŭ��
    public void ChangeMainScene()
    {
        SceneManager.LoadScene("MainScene");
    }



    // ���������� �ڷΰ��� ��ư Ŭ��
    public void InActMonsterInfo()
    {
        MonsterInfo.SetActive(false);

        if (spriteRenderer != null)  // ������� ������ 
        {
            spriteRenderer.color = OriginColor;   // SpriteRenderer�� ������ �����մϴ�.
        }
    }



    // ���������� �˾� ��ư Ŭ��

    // 00��� �ؾ��ϴ� ����
    public void ActPopUp_L() 
    {
        MonsterPartImg.SetActive(false);
        PopUp_L.SetActive(true);

        if (!isPartLower)                             // ��ü����
        {
            print("��ü ���� �Դϴ�.");
            Title_L.text = "��ü��� �ؾ��ϴ�����";
            L_upper.SetActive(true);
        }
        else                                          // ��ü���� 
        {
            print("��ü ���� �Դϴ�.");
            Title_L.text = "��ü��� �ؾ��ϴ�����";
            L_lower.SetActive(true);
        }
    }



    // ��õ�ϴ� 00�
    public void ActPopUp_R() 
    {
        MonsterPartImg.SetActive(false);
        PopUp_R.SetActive(true);

        if (!isPartLower)                            // ��ü����
        {
            print("��ü ���� �Դϴ�.");
            Title_R.text = "��õ�ϴ� ��ü�";
            R_upper.SetActive(true);
        }
        else                                         // ��ü���� 
        {
            print("��ü ���� �Դϴ�.");
            Title_R.text = "��õ�ϴ� ��ü�";
            R_lower.SetActive(true);
        }
    }


    // �˾����� �ڷΰ��� ��ư Ŭ�� 
    public void InActPopUp()
    {
        MonsterPartImg.SetActive(true);

        // Ȱ��ȭ �ϸ鼭 ����� �͵� �� �ݱ� 
        L_upper.SetActive(false);
        L_lower.SetActive(false);
        R_upper.SetActive(false);
        R_lower.SetActive(false);
        PopUp_R.SetActive(false);
        PopUp_L.SetActive(false);
    }
}


// --------------------------------------------------------------------------------------------------


// ���� ���� �ð�ȭ ���� ������ 
[Serializable]
public class MonsterListData
{
    [field: SerializeField]
    public int ID { get; private set; }                       // MonsterData�� �迭 ID�� ����

    [field: SerializeField]
    public GameObject MonFalseImage { get; private set; }     // ����X ǥ��

    [field: SerializeField]
    public Sprite MonTrueImage { get; private set; }          // ����O ǥ�� 

    [field: SerializeField]
    public string MonsterPart { get; private set; }           // ���� �߻� ����

    [field: SerializeField]
    public Sprite MonsterPartImage { get; private set; }      // ���� �߻� ���� �̹���

    [field: SerializeField]
    public GameObject MonsterPartObj { get; private set; }    // ���� �߻� ���� �̹��� Ȱ��ȭ 


}