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

    public GameObject popupImage; // "������������������"

    public List<MonsterListData> monsterListData;

    MonsterData Monsterdata = new MonsterData(); // GameData.cs

    // SpriteRenderer ������Ʈ�� �������� ���� ����
    private SpriteRenderer spriteRenderer;
    public Color ActiveColor = Color.white;

    // RGB ���� �̿��� ������ ����. (16���� 676767)
    Color OriginColor = new Color32(0x67, 0x67, 0x67, 0xFF);

    // ���� ���� ���� �ð�ȭ -> ���ӿ�����Ʈ�� �̹��� ���� ��Ű�� 
    void Start()
    {
        // --- ������ ���� ���� (��� Ȯ��) ---
        // GameData.instance.LoadMonsterData();
        // GameData.instance.monsterdata.MonsterName[0] = "0�� ����";
        // GameData.instance.monsterdata.MonsterUnLocked[0] = true;
        // GameData.instance.SaveMonsterData();
        // ---

        // �� ���� �� �ӽ� 
        for (int id = 0; id < 10; id++)
        {
            // ���� ���� ���� Ȯ�� 
            bool haveMonster = GameData.instance.monsterdata.MonsterUnLocked[id];

            if (haveMonster) 
            {
                // monsterListData ����Ʈ���� ������ IDã��
                int monsterId = monsterListData.FindIndex(data => data.ID == id);

                // �ش� ID�� GameObject�� Sprite �����ͼ�
                GameObject FalseImage = monsterListData[monsterId].MonFalseImage;
                Sprite TrueImage = monsterListData[monsterId].MonTrueImage;

                // GameObject�� FalseImage�� TrueImage�� �������ֱ�
                FalseImage.GetComponent<Image>().sprite = TrueImage;

            }
          
        }
        
    }

    // ������ ���� Ŭ�� �� ����â Ȱ��ȭ
    // �ش� ��ư�� ���� ID �Ѱ��ְ� ���� ��������ֱ� 
    public void ActMonsterInfo(int id)
    {
        bool monsterUnLooked = GameData.instance.monsterdata.MonsterUnLocked[id];

        // �����Ϸ� �����ΰ�?
        if (monsterUnLooked)
        {
            MonsterInfo.SetActive(true);

            // --- ������ �����ͼ� �־��ֱ� 1~4 ---

            // 1. ���� �̹��� 
            Sprite MonsterImg = monsterListData[id].MonTrueImage; // ��������
            MonsterImage.GetComponent<Image>().sprite = MonsterImg; // �־��ֱ� (UI���� �ʿ�: ���Ȯ��)

            // ���� ������ �ε�
            // GameData.instance.LoadMonsterData();

            // 2. ���� �̸� : ���� ������ ��� 
            string monstername = GameData.instance.monsterdata.MonsterName[id];
            MonsterName.text = monstername;

            // 3. ���� �߻� ����
            string monsterpart = monsterListData[id].MonsterPart;
            MonsterPart.text = monsterpart;

            // 4. ���� �߻� ���� �̹��� 
            Sprite monsterpartImg = monsterListData[id].MonsterPartImage;
            MonsterPartImg.GetComponent<Image>().sprite = monsterpartImg;

            // 4. ���� �߻� ���� Ȱ��ȭ
            GameObject monsterobj = monsterListData[id].MonsterPartObj;

            // ���� ���� ������Ʈ���� SpriteRenderer ������Ʈ�� �����ɴϴ�.
            spriteRenderer = monsterobj.GetComponent<SpriteRenderer>();

            // SpriteRenderer�� �����ϴ��� Ȯ���մϴ�.
            if (spriteRenderer != null)
            {
                // SpriteRenderer�� ������ �����մϴ�.
                spriteRenderer.color = ActiveColor;
            }


        }
        else
        {
            // "���� �������� �����Դϴ�." ����

            // �˾� �̹����� Ȱ��ȭ
            popupImage.SetActive(true);

            // 2�� �Ŀ� �˾� �̹����� ��Ȱ��ȭ�ϴ� �ڷ�ƾ ����
            StartCoroutine(HidePopupAfterDelay(1f));
        }
        
    }

    // ���� �ð� �� �˾� �̹����� ��Ȱ��ȭ�ϴ� �ڷ�ƾ
    private IEnumerator HidePopupAfterDelay(float delay)
    {
        // ������ �ð�(��) ���� ���
        yield return new WaitForSeconds(delay);

        // �˾� �̹����� ��Ȱ��ȭ
        popupImage.SetActive(false);
    }

    // �������� �ڷΰ��� ��ư Ŭ��
    public void ChangeMainScene()
    {
        SceneManager.LoadScene("MainScene");
    }

    // �˾����� �ڷΰ��� ��ư Ŭ��
    public void InActMonsterInfo()
    {
        MonsterInfo.SetActive(false);

        // ������� ������ 
        if (spriteRenderer != null)
        {
            // SpriteRenderer�� ������ �����մϴ�.
            spriteRenderer.color = OriginColor;
        }
    }

}

// ���� ���� �ð�ȭ ���� ������ 
[Serializable]
public class MonsterListData
{
    [field: SerializeField]
    public int ID { get; private set; } // MonsterData�� �迭 ID�� ����

    [field: SerializeField]
    public GameObject MonFalseImage { get; private set; } // ����X ǥ��

    [field: SerializeField]
    public Sprite MonTrueImage { get; private set; } // ����O ǥ�� 

    [field: SerializeField]
    public string MonsterPart { get; private set; } // ���� �߻� ����

    [field: SerializeField]
    public Sprite MonsterPartImage { get; private set; } // ���� �߻� ���� �̹���

    [field: SerializeField]
    public GameObject MonsterPartObj { get; private set; } // ���� �߻� ���� �̹��� Ȱ��ȭ 


}