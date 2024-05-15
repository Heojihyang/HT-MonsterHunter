using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 싱글톤 UI매니저
public class UiManager : MonoBehaviour
{
    private static UiManager instance;

    // UI 오브젝트
    public Text actionNameLabel;    // 동작 이름 UI
    public Text actionCountLabel;   // 동작 카운트 UI
    public Text adviceLabel;        // 코멘트 UI
    public Image progressBarImage;  // 진척도 UI

    // 세팅값
    public float duration = 120f;   // 전체 플레이 타임
    private float elapsedTime = 0f; // 플레이 경과 시간

    // 싱글톤
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

    // 인스턴스 생성
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
        // 라벨 초기값 세팅
        UpdateActionName("");
        UpdateActionCount(0, 0, 0);
        UpdateAdviceLabel("");

        // 진척도 초기값 세팅
        progressBarImage.fillAmount = 0;
        elapsedTime = 0;
    }

    private void Update()
    {
        UpdateProgress();
    }


    // 운동명 업데이트()
    public void UpdateActionName(string actionName)
    {
        actionNameLabel.text = actionName;
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

    // 진척도 업데이트()
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
