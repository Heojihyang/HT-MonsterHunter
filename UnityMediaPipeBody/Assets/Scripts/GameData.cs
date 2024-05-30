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

public class GamePlayData
{
    // 게임을 플레이한 날짜와 그 날짜에
    // (게임을 플레이한 횟수, 플레이 총 시간, 클리어한 게임, 수집한 몬스터) 의 정보들을 저장

    // 게임 플레이 횟수
    public int playCount; 

    // 총 플레이 시간
    public float totalPlayTime;

    // 클리어한 게임
    public bool[] ClearedGames = new bool[10];

    // 수집한 몬스터
    public bool[] CollectedMonsters = new bool[10];
    

    /*
    public int playCount;
    public float totalPlayTime;
    public List<string> clearedGames = new List<string>();
    public List<string> collectedMonsters = new List<string>();

    
    public GamePlayData()
    {
        clearedGames = new List<string>();
        collectedMonsters = new List<string>();
    }
    */
}


public class GameData : MonoBehaviour
{
    public static GameData instance; // 싱글톤

    string Path;
    string MonsterFileName = "MonsterDataSave";
    string PlayerFileName = "PlayerDataSave";
    string gamePlayFileName = "GamePlayDataSave";

    public MonsterData monsterdata = new MonsterData();
    public PlayerData playerdata = new PlayerData();
    public GamePlayData gmaeplaydata = new GamePlayData();
    // public Dictionary<string, GamePlayData> gamePlayData = new Dictionary<string, GamePlayData>();

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
        string data = JsonUtility.ToJson(gmaeplaydata); // Json은 string 형태로 저장됨

        // print(Path); // 로컬 저장경로 확인하기

        // 3. Json을 외부에 저장
        File.WriteAllText(Path + PlayerFileName, data);

        print("플레이어 데이터 저장 완료");

        // var serializedData = new SerializableDictionary(gamePlayData);
        // string data = JsonUtility.ToJson(serializedData);

        /*
        print(gamePlayData[DateTime.Today].playCount);
        print(gamePlayData[DateTime.Today].totalPlayTime);
        print(gamePlayData[DateTime.Today].clearedGames[0]);
        print(gamePlayData[DateTime.Today].collectedMonsters[0]);
        */
        // File.WriteAllText(Path + gamePlayFileName, data); // Json을 외부에 저장
        // print("게임 플레이 데이터 저장 완료");
    }

    public void LoadGamePlayData()
    {
        /*
        if (File.Exists(Path + gamePlayFileName))
        {
            string data = File.ReadAllText(Path + gamePlayFileName);
            var serializedData = JsonUtility.FromJson<SerializableDictionary>(data);
            gamePlayData = serializedData.ToDictionary();
            print("게임 플레이 데이터 불러오기 완료");
        }
        */
        
    }


    // 게임 플레이 데이터 추가 
    /*
    public void AddGamePlayData(DateTime date, int playCount, float playTime, string clearedGame, string collectedMonster)
    {
        string dateKey = date.ToString("yyyy-MM-dd");

        // 주어진 날짜의 데이터가 없는 경우에만 새로운 데이터 추가
        if (!gamePlayData.ContainsKey(dateKey))
        {
            gamePlayData[dateKey] = new GamePlayData();
        }

        gamePlayData[dateKey].playCount += playCount;
        gamePlayData[dateKey].totalPlayTime += playTime;
        gamePlayData[dateKey].clearedGames.Add(clearedGame);
        gamePlayData[dateKey].collectedMonsters.Add(collectedMonster);

        SaveGamePlayData(); // 데이터 추가 후 즉시 저장
    }
    */

    // 게임 플레이 데이터 조회 
    /*
    public void PrintGamePlayData()
    {

        foreach (var record in gamePlayData)
        {
            Debug.Log("Date: " + record.Key);
            Debug.Log("Play Count: " + record.Value.playCount);
            Debug.Log("Total Play Time: " + record.Value.totalPlayTime + " hours");

            Debug.Log("Cleared Games:");
            foreach (var game in record.Value.clearedGames)
            {
                Debug.Log("- " + game);
            }

            Debug.Log("Collected Monsters:");
            foreach (var monster in record.Value.collectedMonsters)
            {
                Debug.Log("- " + monster);
            }
        }

    }
    */
}

/*
[Serializable]
public class SerializableDictionary
{
    [SerializeField]
    private List<string> keys = new List<string>();
    [SerializeField]
    private List<GamePlayData> values = new List<GamePlayData>();

    private Dictionary<string, GamePlayData> target;

    public SerializableDictionary(Dictionary<string, GamePlayData> target)
    {
        this.target = target;
    }

    public void OnBeforeSerialize()
    {
        keys.Clear();
        values.Clear();
        foreach (var kvp in target)
        {
            keys.Add(kvp.Key);
            values.Add(kvp.Value);
        }
    }

    public void OnAfterDeserialize()
    {
        target = new Dictionary<string, GamePlayData>();
        for (int i = 0; i < keys.Count; i++)
        {
            target.Add(keys[i], values[i]);
        }
    }

    public Dictionary<string, GamePlayData> ToDictionary()
    {
        return target;
    }
}
*/