using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// �̱��� UI�Ŵ���
public class UiManager : MonoBehaviour
{
    private static UiManager instance;

    // UI ������Ʈ
    public Text actionNameLabel;    // ���� �̸� UI
    public Text actionCountLabel;   // ���� ī��Ʈ UI
    public Text adviceLabel;        // �ڸ�Ʈ UI
    public Image progressBarImage;  // ��ô�� UI

    // ���ð�
    public float duration = 120f;   // ��ü �÷��� Ÿ��
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
        UpdateActionName("");
        UpdateActionCount(0, 0, 0);
        UpdateAdviceLabel("");

        // ��ô�� �ʱⰪ ����
        progressBarImage.fillAmount = 0;
        elapsedTime = 0;
    }

    private void Update()
    {
        UpdateProgress();
    }


    // ��� ������Ʈ()
    public void UpdateActionName(string actionName)
    {
        actionNameLabel.text = actionName;
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

    // ��ô�� ������Ʈ()
    public void UpdateProgress()
    {
        elapsedTime += Time.deltaTime;
        float progress = Mathf.Clamp01(elapsedTime / duration);
        progressBarImage.fillAmount = progress;

        if(elapsedTime > duration)
        {
            elapsedTime = 0;
        }
    }
}
