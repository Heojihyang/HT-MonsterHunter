using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetResolution : MonoBehaviour
{
    void Start()
    {
        // ����� �ػ󵵷� ���� (��: 1080x1920)
        Screen.SetResolution(1080, 1920, false);
    }
}
