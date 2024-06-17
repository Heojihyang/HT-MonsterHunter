using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// �̱��� UI�Ŵ���
public class UiManager : MonoBehaviour
{
    private static UiManager instance;

    // UI ������Ʈ
    public Text moderatorLabel;      // ������ UI
    public Text actionNameLabel;    // ���� �̸� UI
    public Text actionCountLabel;   // ���� ī��Ʈ UI
    public Text adviceLabel;        // �ڸ�Ʈ UI
    public Image progressBarImage;  // ��ô�� UI

    // �����ڿ� UI
    public Text angle1Label;
    public Text angle2Label;
    public Text overallLabel;
    public Text scoreLabel;

    // ���ð�
    public float duration = 389.0f;   // ��ü �÷��� Ÿ��
    private float elapsedTime = 0f; // �÷��� ��� �ð�

    // �̱���
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

    // �ν��Ͻ� ����
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

    private void Start()
    {
        // �� �ʱⰪ ����
        /*
        UpdateActionName("");
        UpdateActionCount(0, 0);
        UpdateAdviceLabel("");
        */

        // ��ô�� �ʱⰪ ����
        progressBarImage.fillAmount = 0;
        elapsedTime = 0;
    }

    private void Update()
    {
        UpdateProgress();
    }

    // ���� �ؽ�Ʈ ������Ʈ()
    public void UpdateModeratorLabel(string description)
    {
        moderatorLabel.text = description;
    }

    // ��� ������Ʈ()
    public void UpdateActionName(string actionName)
    {
        actionNameLabel.text = actionName;
    }

    // ī��Ʈ ������Ʈ()
    public void UpdateActionCount(int count, int maxCount)
    {
        actionCountLabel.text = count + "ȸ/ " + maxCount + "ȸ";
    }

    // �ڸ�Ʈ ������Ʈ()
    public void UpdateAdviceLabel(string coment)
    {
        adviceLabel.text = coment;
    }

    // ��ô�� ������Ʈ()
    public void UpdateProgress()
    {
        elapsedTime += Time.deltaTime;
        float progress = Mathf.Clamp01(elapsedTime / duration);
        progressBarImage.fillAmount = progress;
    }

    // �����ڿ� UI ������Ʈ
    // (1) 1�� �������׸� ������Ʈ
    public void UpdateAngle1Label(string description)
    {
        angle1Label.text = description;
    }

    // (2) 2�� �������׸� ������Ʈ
    public void UpdateAngle2Label(string description)
    {
        angle2Label.text = description;
    }

    // (3) ���� ������ ������Ʈ
    public void UpdateOverallLabel(string description)
    {
        overallLabel.text = description;
    }

    // (4) ���� ���ھ� ������ ������Ʈ
    public void UpdateScorelLabel(string description)
    {
        scoreLabel.text = description;
    }
}
