using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public Text actionNameLabel;    // ���� �̸��� ǥ���� �ؽ�Ʈ UI
    public Text actionCountLabel;   // ���� Ƚ���� ��Ʈ�� ǥ���� �ؽ�Ʈ UI
    public Text adviceLabel;        // �ڸ�Ʈ UI


    // ���� ����Ǵ� � �� ������Ʈ
    public void UpdateActionName(string actionName)
    {
        // ���� �̸� ������Ʈ
        actionNameLabel.text = "� �ڼ�" + actionName;
    }

    // Ƚ���� ��Ʈ ī��Ʈ
    public void UpdateActionCount(int count, int maxCount, int sets)
    {
        // Ƚ���� ��Ʈ ������Ʈ
        actionCountLabel.text = count + "ȸ/ " + maxCount + "ȸ (" + sets + "��Ʈ)";
    }
}
