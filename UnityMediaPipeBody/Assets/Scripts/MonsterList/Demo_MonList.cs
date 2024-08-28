using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// [ ���� ���� ���θ� �����ϴ� ��ũ��Ʈ ]
// 
//  ���� 1�� ������ => ��� ���� �����Ϸ� 
// 
public class Demo_MonList : MonoBehaviour
{
    public MonListManager monListManager;
    // ������Ʈ �޼��忡�� �Է��� �����Ͽ� ȣ��
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ShowMonsters(new int[] { 6, 8 }); // ��� ���͸� ���� �Ϸ� ���·� ǥ��
        }
    }

    // ������ ID �迭�� �޾� �ش� ���͵��� ���� �Ϸ� ���·� ǥ���ϴ� �޼���
    private void ShowMonsters(int[] monsterIds)
    {
        foreach (int id in monsterIds)
        {
            UnlockMonster(id);
        }
    }

    // Ư�� ���� ID�� ���� �Ϸ� ���·� ǥ���ϴ� �޼���
    private void UnlockMonster(int id)
    {
        // ���� ������ ������Ʈ
        GameData.instance.monsterdata.MonsterUnLocked[id] = true;

        // ���� ����Ʈ���� �ش� ������ �ε��� ã��
        int monsterId = monListManager.monsterListData.FindIndex(data => data.ID == id);
        if (monsterId == -1) return; // ���� ID�� ����Ʈ�� ���� ���

        // �̹��� ������Ʈ
        GameObject falseImage = monListManager.monsterListData[monsterId].MonFalseImage;
        Sprite trueImage = monListManager.monsterListData[monsterId].MonTrueImage;
        falseImage.GetComponent<Image>().sprite = trueImage;
    }
}
