using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDungeonSceneManager : MonoBehaviour
{
    // � ������ ���°��� ������ �޾ƿ���
    //PlayerPrefs.SetInt("MonsterNumberToSend", sendMonsterNumber);
    public int receivedMonsterNumber;

    void Start()
    {
        receivedMonsterNumber = PlayerPrefs.GetInt("MonsterNumberToSend", 0);
        Debug.Log("Received MonsterNumber: " + receivedMonsterNumber);
    }
}
