using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecordManager : MonoBehaviour
{
    // 월과 연도를 표시할 UI 텍스트 요소
    public Text monthYearText;

    // 날짜를 표시할 오브젝트 : 리스트로 받아야할듯...
    public GameObject dateCellPrefab;

    // 현재 달력의 날짜를 저장하는 변수
    private DateTime currentDate;


    void Start()
    {
        // 현재 날짜
        // MM/DD/YYYY 00:00:00 
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

        /*
        // 프리팹 사용 시 : 기존의 날짜 셀 삭제
        foreach (Transform child in datesParent)
        {
            Destroy(child.gameObject);
        }
        */

        // 해당 월의 첫째 날을 계산
        DateTime firstDayOfMonth = new DateTime(currentDate.Year, currentDate.Month, 1);
        DayOfWeek dayOfWeek = firstDayOfMonth.DayOfWeek;
        print("1일의 요일 = " + dayOfWeek);

        // 해당 월의 날짜 수 계산
        int daysInMonth = DateTime.DaysInMonth(currentDate.Year, currentDate.Month);
        print("해당 월의 날짜 수  = " + daysInMonth);

        // 프리팹 사용 시 : 해당 월의 각 날짜에 대해 날짜 셀 생성
        for (int i = 1; i <= daysInMonth; i++)
        {
            // DateTime date = new DateTime(currentDate.Year, currentDate.Month, i);

            // 날짜 셀 프리팹을 복제하여 날짜를 텍스트로 표시
            // GameObject dateCell = Instantiate(dateCellPrefab, datesParent);
            // dateCell.GetComponentInChildren<Text>().text = i.ToString();

        }
        
    }

}
