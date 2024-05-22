using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecordManager : MonoBehaviour
{
    // 월과 연도를 표시할 UI 텍스트 요소
    public Text monthYearText;

    // 현재 달력의 날짜를 저장하는 변수
    private DateTime currentDate;

    public GameObject WhiteBackground;

    public GameObject[] DayButton = new GameObject[42];

    int daysInMonth;


    void Start()
    {
        // 현재 날짜  MM/DD/YYYY 00:00:00 
        currentDate = DateTime.Today; 

        // 현재 날짜로 달력 업데이트 
        UpdateCalendar();
    }

    void Update()
    {
        
    }

    // 다음 달로 이동하는 함수
    public void NextMonth()
    {
        currentDate = currentDate.AddMonths(1);
        UpdateCalendar();
    }

    // 이전 달로 이동하는 함수
    public void PreviousMonth()
    {
        currentDate = currentDate.AddMonths(-1);
        UpdateCalendar();
    }

    // 달력을 업데이트하는 함수
    private void UpdateCalendar()
    {
        // 상단에 현재 연도와 월 표시
        monthYearText.text = currentDate.ToString("yyyy년 M월");

        // 해당 월의 첫째 날 (요일) : 캘린더의 시작 위치 
        DateTime firstDayOfMonth = new DateTime(currentDate.Year, currentDate.Month, 1);
        DayOfWeek dayOfWeek = firstDayOfMonth.DayOfWeek;
        print("1일의 요일 = " + dayOfWeek);

        // 해당 월의 날짜 수 계산 
        daysInMonth = DateTime.DaysInMonth(currentDate.Year, currentDate.Month);
        print("해당 월의 날짜 수  = " + daysInMonth);



        //// 날짜 정보 시각화 

        // 일요일 기준 시작 위치 설정
        int startDay = (int)dayOfWeek;

        // 날짜 버튼 초기화
        for (int i = 0; i < DayButton.Length; i++)
        {
            DayButton[i].SetActive(false);
        }

        // 달력 구성
        for (int day = 1; day <= daysInMonth; day++)
        {
            int index = startDay + day - 1;
            DayButton[index].transform.GetChild(0).GetComponent<Text>().text = day.ToString();
            DayButton[index].SetActive(true);
        }



        //// 해당 월의 주 수에 따라 달력 배경 크기 변경 
       
        RectTransform rectTran = WhiteBackground.GetComponent<RectTransform>();
        Vector2 anchoredPosition = rectTran.anchoredPosition;

        if (!DayButton[28].activeSelf)
        {
            // 두 줄 줄이기 
            rectTran.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 640);

            anchoredPosition.y = 180;
            rectTran.anchoredPosition = anchoredPosition;

        }
        else if (!DayButton[35].activeSelf)
        {
            // 한 줄 줄이기
            rectTran.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 700);

            anchoredPosition.y = 120;
            rectTran.anchoredPosition = anchoredPosition;

        }
        else
        {
            // 원래 크기로 늘리기
            rectTran.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 760);

            anchoredPosition.y = 60;
            rectTran.anchoredPosition = anchoredPosition;

        }

        
    }

}
