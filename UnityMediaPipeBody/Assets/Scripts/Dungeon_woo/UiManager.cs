using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 싱글톤 UI매니저
public class UiManager : MonoBehaviour
{
    private static UiManager instance;

    public Text actionNameLabel;    // 동작 이름을 표시할 텍스트 UI
    public Text actionCountLabel;   // 동작 횟수와 세트를 표시할 텍스트 UI
    public Text adviceLabel;        // 코멘트 UI

    public static UiManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }


    // 운동명 업데이트()
    public void UpdateActionName(string actionName)
    {
        actionNameLabel.text = "어떤 자세" + actionName;
    }

    // 카운트 업데이트()
    public void UpdateActionCount(int count, int maxCount, int sets)
    {
        actionCountLabel.text = count + "회/ " + maxCount + "회 (" + sets + "세트)";
    }

    // 코멘트 업데이트()
    public void UpdateAdviceLabel(string coment)
    {
        adviceLabel.text = coment;
    }
}
