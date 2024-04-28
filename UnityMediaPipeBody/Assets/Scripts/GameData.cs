using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

// Json 사용
// 딕셔너리를 지원하지 않아서, 배열로 주고받아야함

// 저장하는 방법
// 1. 저장할 데이터 존재
// 2. 데이터를 Json으로 변환
// 3. Json을 외부에 저장

// 불러오는 방법
// 1. 외부에 저장된 Json을 가져옴
// 2. Json을 데이터 형태로 변환
// 3. 불러온 데이터 사용


// 1. 저장할 데이터 존재
public class MonsterData
{
    // 몬스터(20개) 수집 여부
    // 수집O : true , 수집X : False
    public bool[] MonsterUnLocked = new bool[20];

    // 몬스터 경험치
    public int[] MonsterExp = new int[20];
}

public class GameData : MonoBehaviour
{
    string Path;
    string FileName = "MonsterDataSave";

    MonsterData monsterdata = new MonsterData();

    private void Awake()
    {
        // 경로를 만들어줌
        Path = Application.persistentDataPath + "/";
    }

    void Start()
    {
        
    }

    public void SaveData()
    {
        // 2. 데이터를 Json으로 변환
        string data = JsonUtility.ToJson(monsterdata); // Json은 string 형태로 저장됨

        // print(Path); // 로컬 저장경로 확인하기

        // 3. Json을 외부에 저장
        File.WriteAllText(Path + FileName, data);

        print("데이터 저장 완료");
    }

    public void LoadData()
    {
        // 1. 외부에 저장된 제이슨을 가져옴
        // 2. Json을 데이터 형태로 변환
        string data = File.ReadAllText(Path + FileName); // Json이기 때문에 string 형태로 받아짐

        // 3. 불러온 데이터 사용
        monsterdata = JsonUtility.FromJson<MonsterData>(data); // 불러온 데이터가 monsterdata에 덮어씌워짐

        print("데이터 불러오기 완료");
    }
}
