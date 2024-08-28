using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// [ 몬스터 수집 여부를 조작하는 스크립트 ]
// 
//  숫자 1을 누르면 => 모든 몬스터 수집완료 
// 
public class Demo_MonList : MonoBehaviour
{
    public MonListManager monListManager;
    // 업데이트 메서드에서 입력을 감지하여 호출
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ShowMonsters(new int[] { 6, 8 }); // 모든 몬스터를 수집 완료 상태로 표시
        }
    }

    // 몬스터의 ID 배열을 받아 해당 몬스터들을 수집 완료 상태로 표시하는 메서드
    private void ShowMonsters(int[] monsterIds)
    {
        foreach (int id in monsterIds)
        {
            UnlockMonster(id);
        }
    }

    // 특정 몬스터 ID를 수집 완료 상태로 표시하는 메서드
    private void UnlockMonster(int id)
    {
        // 몬스터 데이터 업데이트
        GameData.instance.monsterdata.MonsterUnLocked[id] = true;

        // 몬스터 리스트에서 해당 몬스터의 인덱스 찾기
        int monsterId = monListManager.monsterListData.FindIndex(data => data.ID == id);
        if (monsterId == -1) return; // 몬스터 ID가 리스트에 없는 경우

        // 이미지 업데이트
        GameObject falseImage = monListManager.monsterListData[monsterId].MonFalseImage;
        Sprite trueImage = monListManager.monsterListData[monsterId].MonTrueImage;
        falseImage.GetComponent<Image>().sprite = trueImage;
    }
}
