using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


//// Json 사용
//   Json은 string 형태로 저장
//   딕셔너리를 지원하지 않아서, 배열로 주고받아야함


//   저장하는 방법
//    1. 저장할 데이터 존재
//    2. 데이터를 Json으로 변환
//    3. Json을 외부에 저장


//   불러오는 방법
//    1. 외부에 저장된 Json을 가져옴
//    2. Json을 데이터 형태로 변환
//    3. 불러온 데이터 사용



public class MonsterData
{
    public bool[] MonsterUnLocked = new bool[10];   // 몬스터(10개) 수집 여부
    public string[] MonsterName = new string[10];   // 몬스터 이름

    // 몬스터 기본 이름 설정 메서드
    public void InitializeDefaultNames()
    {
        MonsterName[0] = "한벙두";
        MonsterName[1] = "읃닥거";
        MonsterName[2] = "최중계";
        MonsterName[3] = "허리스토텔레스";
        MonsterName[4] = "이두나";
        MonsterName[5] = "덤벙벨";
        MonsterName[6] = "김삼두";
        MonsterName[7] = "힙찔이";
        MonsterName[8] = "곽배식";
        MonsterName[9] = "쫑알이";
    }
}

public class PlayerData
{
    public int PlayerExp;   // 플레이어 경험치 
}

[Serializable]
public class GamePlayData
{
    public int PlayCount;            // 게임을 플레이한 횟수
    public List<int> ClearedMaps;    // 클리어한 게임맵 리스트 : 운동 부위 

}

[Serializable]
public class RecordData
{
    public Dictionary<string, GamePlayData> dailyRecords;   // 날짜별 게임플레이 데이터 저장
}


// -----------------------------------------------------------------------------------------------------------


public class GameData : MonoBehaviour
{
    public static GameData instance;   // 싱글톤

    string Path;
    string MonsterFileName = "MonsterDataSave";
    string PlayerFileName = "PlayerDataSave";
    string GamePlayFileName = "GamePlayDataSave";

    public MonsterData monsterdata = new MonsterData();
    public PlayerData playerdata = new PlayerData();
    public RecordData recordData = new RecordData();

    private void Awake()
    {
        // 게임 데이터는 싱글톤으로 관리한다.
        #region 싱글톤

        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(instance.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);

        #endregion


        Path = Application.persistentDataPath + "/";  // 경로
        print("경로생성 " + Path);

        recordData.dailyRecords = new Dictionary<string, GamePlayData>();   // 초기화
    }



    // MonsterData -------------------------------------------------------------------------------------------

    public void SaveMonsterData()
    {
        string data = JsonUtility.ToJson(monsterdata);     // 2. 데이터 -> Json 
        File.WriteAllText(Path + MonsterFileName, data);   // 3. Json 외부에 저장
        print("몬스터 데이터 저장 완료");
    }

    public void LoadMonsterData()
    {
        // 파일 존재 유무 확인 
        if (File.Exists(Path + MonsterFileName))
        {                                                             // 1. 외부 저장된 제이슨 가져옴
            string data = File.ReadAllText(Path + MonsterFileName);   // 2. Json -> 데이터
            monsterdata = JsonUtility.FromJson<MonsterData>(data);    // 3. 불러온 데이터 -> monsterdata에 덮어씌우기
            print("몬스터 데이터 불러오기 완료");
        }
        else
        {
            monsterdata.InitializeDefaultNames();   // 기본 이름 설정
            SaveMonsterData();                                        // 파일 없으면 기본 데이터 저장
            print("기본 몬스터 데이터 생성 완료");
        }
    }



    // PlayerData -------------------------------------------------------------------------------------------

    public void SavePlayerData()
    {
        string data = JsonUtility.ToJson(playerdata);     // 2. 데이터 -> Json 
        File.WriteAllText(Path + PlayerFileName, data);   // 3. Json 외부에 저장
        print("플레이어 데이터 저장 완료");
    }

    public void LoadPlayerData()
    {
        if (File.Exists(Path + PlayerFileName))
        {                                                             // 1. 외부 저장된 제이슨 가져옴
            string data = File.ReadAllText(Path + PlayerFileName);    // 2. Json -> 데이터
            playerdata = JsonUtility.FromJson<PlayerData>(data);      // 3. 불러온 데이터 -> playerdata에 덮어씌우기
            print("플레이어 데이터 불러오기 완료");
        }
        else
        {
            SavePlayerData();                                         // 파일 없으면 기본 데이터 저장
            print("기본 플레이어 데이터 생성 완료");
        }
    }



    // GamePlayData -------------------------------------------------------------------------------------------

    public void SaveGamePlayData()
    {
        string data = DictionaryJsonUtility.ToJson(recordData.dailyRecords, true);   // 2. 데이터 -> Json (직접 만든 변환 사용)
        File.WriteAllText(Path + GamePlayFileName, data);                            // 3. Json 외부에 저장
        print("게임 플레이 데이터 저장 완료");

    }

    public void LoadGamePlayData()
    {
        if (File.Exists(Path + GamePlayFileName))
        {                                                                                             // 1. 외부 저장된 제이슨 가져옴
            string data = File.ReadAllText(Path + GamePlayFileName);                                  // 2. Json -> 데이터
            recordData.dailyRecords = DictionaryJsonUtility.FromJson<string, GamePlayData>(data);     // 3. 불러온 데이터 -> gameplaydata에 덮어씌우기
            print("게임 플레이 데이터 불러오기 완료");
        }
        else
        {
            SaveGamePlayData();                                                                       // 파일 없으면 기본 데이터 저장
            print("기본 게임 플레이 데이터 생성 완료");
        }
    }

}
