using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]                                    // �� Ŭ������ JSON���� ����ȭ�� �� ������ �ǹ�
public class DataDictionary<TKey, TValue>         // �� ���� Ű�� ���� �����ϴ� �뵵�� ���
{
    public TKey Key;
    public TValue Value;
}

[Serializable]
public class JsonDataArray<TKey, TValue>          // ��� Ű-�� ���� �����ϴ� ����Ʈ�� ����
{
    public List<DataDictionary<TKey, TValue>> data;
}

public static class DictionaryJsonUtility
{

    // Dictionary -> Json���� �Ľ�
    public static string ToJson<TKey, TValue>(Dictionary<TKey, TValue> jsonDicData, bool pretty = false)
    {
        List<DataDictionary<TKey, TValue>> dataList = new List<DataDictionary<TKey, TValue>>();
        DataDictionary<TKey, TValue> dictionaryData;
        foreach (TKey key in jsonDicData.Keys)
        {
            dictionaryData = new DataDictionary<TKey, TValue>();
            dictionaryData.Key = key;
            dictionaryData.Value = jsonDicData[key];
            dataList.Add(dictionaryData);
        }
        JsonDataArray<TKey, TValue> arrayJson = new JsonDataArray<TKey, TValue>();
        arrayJson.data = dataList;

        return JsonUtility.ToJson(arrayJson, pretty); // ��ȯ��: JSON ���ڿ�
    }


    // Json Data -> Dictionary�� �Ľ�
    public static Dictionary<TKey, TValue> FromJson<TKey, TValue>(string jsonData)
    {
        JsonDataArray<TKey, TValue> arrayJson = JsonUtility.FromJson<JsonDataArray<TKey, TValue>>(jsonData);
        List<DataDictionary<TKey, TValue>> dataList = arrayJson.data;

        Dictionary<TKey, TValue> returnDictionary = new Dictionary<TKey, TValue>();

        for (int i = 0; i < dataList.Count; i++)
        {
            DataDictionary<TKey, TValue> dictionaryData = dataList[i];
            returnDictionary[dictionaryData.Key] = dictionaryData.Value;
        }

        return returnDictionary;
    }
}