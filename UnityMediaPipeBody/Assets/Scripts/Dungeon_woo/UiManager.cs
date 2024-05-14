using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public Text actionNameLabel;    // 동작 이름을 표시할 텍스트 UI
    public Text actionCountLabel;   // 동작 횟수와 세트를 표시할 텍스트 UI
    public Text adviceLabel;        // 코멘트 UI


    // 현재 진행되는 운동 라벨 업데이트
    public void UpdateActionName(string actionName)
    {
        // 동작 이름 업데이트
        actionNameLabel.text = "어떤 자세" + actionName;
    }

    // 횟수와 세트 카운트
    public void UpdateActionCount(int count, int maxCount, int sets)
    {
        // 횟수와 세트 업데이트
        actionCountLabel.text = count + "회/ " + maxCount + "회 (" + sets + "세트)";
    }
}
