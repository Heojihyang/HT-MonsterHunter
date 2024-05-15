using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// �̱��� UI�Ŵ���
public class UiManager : MonoBehaviour
{
    private static UiManager instance;

    public Text actionNameLabel;    // ���� �̸��� ǥ���� �ؽ�Ʈ UI
    public Text actionCountLabel;   // ���� Ƚ���� ��Ʈ�� ǥ���� �ؽ�Ʈ UI
    public Text adviceLabel;        // �ڸ�Ʈ UI

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


    // ��� ������Ʈ()
    public void UpdateActionName(string actionName)
    {
        actionNameLabel.text = "� �ڼ�" + actionName;
    }

    // ī��Ʈ ������Ʈ()
    public void UpdateActionCount(int count, int maxCount, int sets)
    {
        actionCountLabel.text = count + "ȸ/ " + maxCount + "ȸ (" + sets + "��Ʈ)";
    }

    // �ڸ�Ʈ ������Ʈ()
    public void UpdateAdviceLabel(string coment)
    {
        adviceLabel.text = coment;
    }
}
