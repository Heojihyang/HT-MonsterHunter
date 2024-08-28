using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// [ �ÿ��� �����͸� �����ϴ� ��ũ��Ʈ ]
// 
public class Demo_StartSetting : MonoBehaviour
{
    
    void Start()
    {
        // [ �÷��̾� ����ġ ] 
        SetPlayerExp();

        // [ ���� ���� ]
        SetMonsterList();

        // [ � ��� ]
        SetExerciseRecords();
        DeleteTodayRecords();


        // ������ ����
        GameData.instance.SavePlayerData();
        GameData.instance.SaveGamePlayData();
        GameData.instance.SaveMonsterData();

        // �����͸� �ҷ����� : ���� ������Ʈ �� �����͸� �������� ��.
        GameData.instance.LoadPlayerData();
        GameData.instance.LoadGamePlayData();
        GameData.instance.LoadMonsterData();

    }

    private void SetPlayerExp()
    {
        GameData.instance.playerdata.PlayerExp = 6;
    }

    private void SetMonsterList()
    {
        for (int MonsterNum = 0; MonsterNum < 10; MonsterNum++)
        {
            if (MonsterNum == 6 || MonsterNum == 8) GameData.instance.monsterdata.MonsterUnLocked[MonsterNum] = false;
            else                                    GameData.instance.monsterdata.MonsterUnLocked[MonsterNum] = true;
        }

        print("���� �������� ���� �Ϸ�");
    }

    private void SetExerciseRecords()
    {
        RecordData recordData = GameData.instance.recordData;

        // < 24�� 6�� >
        AddWorkoutRecord(recordData, "2024-06-14", new List<int> { 2 });
        AddWorkoutRecord(recordData, "2024-06-17", new List<int> { 3, 7 });
        AddWorkoutRecord(recordData, "2024-06-18", new List<int> { 9 });

        // < 24�� 7�� >
        AddWorkoutRecord(recordData, "2024-07-24", new List<int> { 1, 4 });
        AddWorkoutRecord(recordData, "2024-07-19", new List<int> { 5 });

        // < 24�� 8�� >
        AddWorkoutRecord(recordData, "2024-08-05", new List<int> { 5, 7 });
        AddWorkoutRecord(recordData, "2024-08-07", new List<int> { 0 });
        AddWorkoutRecord(recordData, "2024-08-15", new List<int> { 2 });
        AddWorkoutRecord(recordData, "2024-08-16", new List<int> { 7 });
        AddWorkoutRecord(recordData, "2024-08-21", new List<int> { 0 });
        AddWorkoutRecord(recordData, "2024-08-22", new List<int> { 1, 4, 7 });
        AddWorkoutRecord(recordData, "2024-08-24", new List<int> { 3, 9 });
        AddWorkoutRecord(recordData, "2024-08-26", new List<int> { 2 });

        print("� ��� ���� �Ϸ�");
    }

    private void AddWorkoutRecord(RecordData recordData, string date, List<int> clearedMaps)
    {
        if (!recordData.dailyRecords.ContainsKey(date))
        {
            recordData.dailyRecords[date] = new GamePlayData
            {
                PlayCount = clearedMaps.Count,
                ClearedMaps = clearedMaps
            };
        }
        else
        {
            GamePlayData existingRecord = recordData.dailyRecords[date];
            existingRecord.PlayCount += clearedMaps.Count;
            existingRecord.ClearedMaps.AddRange(clearedMaps);
        }
    }

    private void DeleteTodayRecords()
    {
        RecordData recordData = GameData.instance.recordData;

        string today = DateTime.Now.ToString("yyyy-MM-dd");

        // ���� ��¥�� ���õ� �����͸� ����
        if (recordData.dailyRecords.ContainsKey(today))
        {
            recordData.dailyRecords.Remove(today);
        }
    }
}
