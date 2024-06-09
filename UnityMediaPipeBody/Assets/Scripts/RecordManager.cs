using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RecordManager : MonoBehaviour
{
    // 월과 연도를 표시할 UI 텍스트 요소
    public Text monthYearText;

    // 현재 달력의 날짜를 저장하는 변수
    private DateTime currentDate;

    private GameObject selectedDay; // 선택된 날짜 버튼 
    public GameObject calendarParent; // 달력 버튼을 포함하는 부모 오브젝트

    public GameObject[] DayButton = new GameObject[42];

    int daysInMonth;

    // 선택한 날짜 이전의 색상을 저장하는 변수
    private Color previousDayColor;
    private Color previousDayTextColor;

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
            Text dayText = DayButton[index].transform.GetChild(0).GetComponent<Text>();
            dayText.text = day.ToString();
            DayButton[index].SetActive(true);

            // 오늘 날짜 확인
            DateTime currentDateInLoop = new DateTime(currentDate.Year, currentDate.Month, day);

            // 오늘과 같은 달에 해당하는 운동 기록이 있는지 확인
            string dateString = currentDateInLoop.ToString("yyyy-MM-dd");
            if (GameData.instance.recordData.dailyRecords.ContainsKey(dateString))
            {
                // 운동 기록이 있는 경우
                if (currentDate.Year == DateTime.Today.Year && currentDate.Month == DateTime.Today.Month && day == DateTime.Today.Day)
                {
                    // 당일인 경우 빨간색 굵은 글씨로 변경
                    dayText.color = new Color32(0xFE, 0x22, 0x04, 0xFF); // 빨간색
                    dayText.fontStyle = FontStyle.Bold; // 굵은 글씨
                }
                else
                {
                    // 당일이 아닌 경우 버튼을 노란색으로 변경하고 텍스트를 흰색으로 설정
                    DayButton[index].GetComponent<Image>().color = new Color(0xFA / 255f, 0xB0 / 255f, 0x00 / 255f); // 노란색
                    dayText.color = Color.white;
                }
            }
            else
            {
                // 운동 기록이 없는 경우 버튼의 색을 기본 색상으로 변경
                DayButton[index].GetComponent<Image>().color = Color.white;
                dayText.fontStyle = FontStyle.Normal; // 텍스트를 기본 글씨로 설정

                // 오늘 날짜 : 텍스트 색상을 빨간색으로 변경
                if (currentDate.Year == DateTime.Today.Year && currentDate.Month == DateTime.Today.Month && day == DateTime.Today.Day)
                {
                    dayText.color = new Color32(0xFE, 0x22, 0x04, 0xFF); // 빨간색
                    DayButton[index].tag = "Today"; // 태그 부여 
                }
                else
                {
                    // 기본 색상으로 설정 (예: 검정색)
                    dayText.color = Color.black;
                }
            }
        }

    }

    // 선택한 날짜 시각화
    // 버튼 클릭 시, 클릭된 오브젝트 가져옴 
    public void SelectDay(GameObject selectDay)
    {
        // 이전에 클릭한 버튼이 있다면 && 당일이 아니었으면 -> 버튼 초기화 
        // SelectedDay : 이전에 클릭한 버튼 
        if((selectedDay != null)&&(!selectedDay.CompareTag("Today")))
        {
            // 활성화 되어있던 버튼의 이전 색상으로 변경
            // selectedDay.GetComponent<Image>().color = Color.white;
            // selectedDay.GetComponentInChildren<Text>().color = Color.black;

            selectedDay.GetComponent<Image>().color = previousDayColor;
            selectedDay.GetComponentInChildren<Text>().color = previousDayTextColor;
        }

        // 새롭게 클릭된 버튼으로 할당 변경 
        selectedDay = selectDay;

        // 당일이면 : 변화 X 
        if(selectDay.CompareTag("Today"))
        {
            print("오늘을 클릭했습니다.");
            return;
        }

        // 현재 버튼의 색상을 저장
        previousDayColor = selectDay.GetComponent<Image>().color;
        previousDayTextColor = selectDay.GetComponentInChildren<Text>().color;

        // 당일이 아니면 : 버튼색 빨간색 , 글자 하얀색 
        selectDay.GetComponent<Image>().color = new Color(0xFE / 255f, 0x22 / 255f, 0x04 / 255f); // #FE2204;
        selectDay.GetComponentInChildren<Text>().color = Color.white;

        // 하단 정보 

    }

    // 운동 기록창 에서 뒤로가기 버튼 클릭
    public void ChangeMainScene()
    {
        SceneManager.LoadScene("MainScene");
    }
}
