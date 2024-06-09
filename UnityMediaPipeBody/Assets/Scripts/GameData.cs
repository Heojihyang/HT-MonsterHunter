using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

//// Json 사용
// 딕셔너리를 지원하지 않아서, 배열로 주고받아야함

// 저장하는 방법
// 1. 저장할 데이터 존재
// 2. 데이터를 Json으로 변환
// 3. Json을 외부에 저장

// 불러오는 방법
// 1. 외부에 저장된 Json을 가져옴
// 2. Json을 데이터 형태로 변환
// 3. 불러온 데이터 사용



//// 1. 저장할 데이터 존재
public class MonsterData
{
    // 몬스터(20개) 수집 여부
    // 수집O : true , 수집X : False
    public bool[] MonsterUnLocked = new bool[10];

    // 몬스터 이름
    public string[] MonsterName = new string[10];
}

public class PlayerData
{
    //  플레이어 경험치 
    public int PlayerExp;
}

[Serializable]
public class GamePlayData
{
    // 게임을 플레이한 날짜와 그 날짜에 정보들 저장

    public int PlayCount; // 게임을 플레이한 횟수

    // public float TotalPlayTime; // 플레이 총 시간 = 운동 부위의 시간 활용해서 계산 

    public List<string> ClearedMaps; // 클리어한 게임맵 리스트 : 운동 부위 

    // public MonsterData CollectedMonsters; // 수집한 몬스터 데이터
}

// 날짜별 운동 기록 
[Serializable]
public class RecordData
{
    public Dictionary<string, GamePlayData> dailyRecords; // 날짜별 게임 플레이 데이터를 저장하는 Dictionary
}

public class GameData : MonoBehaviour
{
    public static GameData instance; // 싱글톤

    string Path;
    string MonsterFileName = "MonsterDataSave";
    string PlayerFileName = "PlayerDataSave";
    string GamePlayFileName = "GamePlayDataSave";

    public MonsterData monsterdata = new MonsterData();
    public PlayerData playerdata = new PlayerData();
    public RecordData recordData = new RecordData();

    private void Awake()
    {
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

        // 경로를 만들어줌
        Path = Application.persistentDataPath + "/";
        print("경로생성 "+Path); // 경로 확인 

        // 초기화
        recordData.dailyRecords = new Dictionary<string, GamePlayData>();
    }

    void Start()
    {
        
    }

    public void SaveMonsterData()
    {
        // 2. 데이터를 Json으로 변환
        string data = JsonUtility.ToJson(monsterdata); // Json은 string 형태로 저장됨

        // print(Path); // 로컬 저장경로 확인하기

        // 3. Json을 외부에 저장
        File.WriteAllText(Path + MonsterFileName, data);

        print("몬스터 데이터 저장 완료");
    }

    public void LoadMonsterData()
    {
        // 1. 외부에 저장된 제이슨을 가져옴
        // 2. Json을 데이터 형태로 변환
        string data = File.ReadAllText(Path + MonsterFileName); // Json이기 때문에 string 형태로 받아짐

        // 3. 불러온 데이터 사용
        monsterdata = JsonUtility.FromJson<MonsterData>(data); // 불러온 데이터가 monsterdata에 덮어씌워짐

        print("몬스터 데이터 불러오기 완료");
    }

    public void SavePlayerData()
    {
        // 2. 데이터를 Json으로 변환
        string data = JsonUtility.ToJson(playerdata); // Json은 string 형태로 저장됨

        // print(Path); // 로컬 저장경로 확인하기

        // 3. Json을 외부에 저장
        File.WriteAllText(Path + PlayerFileName, data);

        print("플레이어 데이터 저장 완료");
    }

    public void LoadPlayerData()
    {
        // 1. 외부에 저장된 제이슨을 가져옴
        // 2. Json을 데이터 형태로 변환
        string data = File.ReadAllText(Path + PlayerFileName); // Json이기 때문에 string 형태로 받아짐

        // 3. 불러온 데이터 사용
        playerdata = JsonUtility.FromJson<PlayerData>(data); // 불러온 데이터가 monsterdata에 덮어씌워짐

        print("플레이어 데이터 불러오기 완료");
    }

    public void SaveGamePlayData()
    {
        // 2. 데이터를 Json으로 변환
        string data = DictionaryJsonUtility.ToJson(recordData.dailyRecords, true); // JSON 변환

        // 3. Json을 외부에 저장
        File.WriteAllText(Path + GamePlayFileName, data);

        print("게임 플레이 데이터 저장 완료");

    }

    public void LoadGamePlayData()
    {
        if (File.Exists(Path + GamePlayFileName))
        {
            string data = File.ReadAllText(Path + GamePlayFileName);
            recordData.dailyRecords = DictionaryJsonUtility.FromJson<string, GamePlayData>(data);
            print("게임 플레이 데이터 불러오기 완료");
        }
        else
        {
            print("게임 플레이 데이터 파일이 존재하지 않습니다.");
        }
    }

}
