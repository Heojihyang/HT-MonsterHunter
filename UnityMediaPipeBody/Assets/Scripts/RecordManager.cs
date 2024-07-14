using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class RecordManager : MonoBehaviour
{
    public Text monthYearText;           // 월, 연도 표시

    private DateTime currentDate;        // 현재 달력의 날짜를 저장하는 변수

    private GameObject selectedDay;      // 선택된 날짜 버튼 
    public GameObject calendarParent;    // 달력 버튼을 포함하는 부모 오브젝트

    public GameObject recordNone;        // 기록 없음 
    public GameObject recordYes;         // 기록 있음 

    public Text Date;                    // 날짜
    public Text ClearMaps;               // 운동내용
    public Text MapCount;                // 클리어 운동 횟수
    public Text PlayTime;                // 플레이 시간
    public Text Kcal;                    // 칼로리 소모 

    public GameObject[] DayButton = new GameObject[42];  // 날짜 버튼
    
    private Dictionary<string, (int playTime, int calories)> bodyParts = new Dictionary<string, (int playTime, int calories)>
    {
        { "가슴", (5, 100) },
        { "등", (7, 50) },
        { "복부", (8, 90) },
        { "허리", (11, 120) },
        { "이두", (8, 80) },
        { "전완근", (10, 105) },
        { "삼두", (8, 100) },
        { "힙", (5, 80) },
        { "허벅지", (6, 70) },
        { "종아리", (4, 65) }
    };

    private int daysInMonth;

    private Color previousDayColor;      // 선택한 날짜 이전의 색상 저장
    private Color previousDayTextColor;


    void Start()
    {
        currentDate = DateTime.Today;         // 현재 날짜  MM/DD/YYYY 00:00:00 

        UpdateCalendar();                     // 현재 날짜로 달력 업데이트 

        CheckAndActivateObject(currentDate);  // 운동기록정보창 활성화/비활성화
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
        selectedDay = null;    // 이전에 선택했던 버튼 상태 초기화 (월별 이동 시 오류 제어)

        monthYearText.text = currentDate.ToString("yyyy년 M월");                           // 상단에 현재 연도와 월 표시

        DateTime firstDayOfMonth = new DateTime(currentDate.Year, currentDate.Month, 1);   // 해당 월의 첫째 날 (요일) : 캘린더의 시작 위치 
        DayOfWeek dayOfWeek = firstDayOfMonth.DayOfWeek;
        print("1일의 요일 = " + dayOfWeek);

        daysInMonth = DateTime.DaysInMonth(currentDate.Year, currentDate.Month);           // 해당 월의 날짜 수 계산 
        print("해당 월의 날짜 수  = " + daysInMonth);



        //// 날짜 정보 시각화 ----------------------------------------------------------------------------------------------------------------

        int startDay = (int)dayOfWeek;                 // 일요일 기준 시작 위치 설정

        for (int i = 0; i < DayButton.Length; i++)     // 날짜 버튼 초기화
        {
            DayButton[i].SetActive(false);
        }

        for (int day = 1; day <= daysInMonth; day++)   // 달력 구성
        {
            int index = startDay + day - 1;
            Text dayText = DayButton[index].transform.GetChild(0).GetComponent<Text>();
            dayText.text = day.ToString();
            DayButton[index].SetActive(true);

            DateTime currentDateInLoop = new DateTime(currentDate.Year, currentDate.Month, day);   // 오늘 날짜 확인

            string dateString = currentDateInLoop.ToString("yyyy-MM-dd");                          // 오늘과 같은 달에 해당하는 운동 기록이 있는지 확인

            // 운동 기록이 있는 경우
            if (GameData.instance.recordData.dailyRecords.ContainsKey(dateString))
            {
                // 오늘
                if (currentDate.Year == DateTime.Today.Year && currentDate.Month == DateTime.Today.Month && day == DateTime.Today.Day)
                {
                    dayText.color = new Color32(0xFE, 0x22, 0x04, 0xFF);   // 빨간색
                    dayText.fontStyle = FontStyle.Bold;                    // 굵은 글씨

                    DayButton[index].tag = "Today";                        // 태그 부여 
                }
                // 오늘이 아닌 경우
                else
                {
                    DayButton[index].GetComponent<Image>().color = new Color(0xFA / 255f, 0xB0 / 255f, 0x00 / 255f); // 노란색
                    dayText.color = Color.white;
                }
            }
            // 운동 기록이 없는 경우
            else
            {
                DayButton[index].GetComponent<Image>().color = Color.white;    // 기본 버튼 색 
                dayText.fontStyle = FontStyle.Normal;                          // 기본 글씨

                // 오늘
                if (currentDate.Year == DateTime.Today.Year && currentDate.Month == DateTime.Today.Month && day == DateTime.Today.Day)
                {
                    dayText.color = new Color32(0xFE, 0x22, 0x04, 0xFF);   // 빨간색

                    DayButton[index].tag = "Today";                        // 태그 부여 
                }
                // 오늘이 아닌 경우
                else
                {
                    dayText.color = Color.black;                           // 검정색
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
            selectedDay.GetComponent<Image>().color = previousDayColor;
            selectedDay.GetComponentInChildren<Text>().color = previousDayTextColor;
        }

        selectedDay = selectDay;   // 새롭게 클릭된 버튼으로 할당 변경 

        // 당일이면 : 변화 X 
        if (selectDay.CompareTag("Today"))
        {
            print("오늘을 클릭했습니다.");
            CheckAndActivateObject(DateTime.Today);
            return;
        }

        previousDayColor = selectDay.GetComponent<Image>().color;  // 현재 버튼의 색상 저장
        previousDayTextColor = selectDay.GetComponentInChildren<Text>().color;

        // 당일이 아니면 : 버튼색 빨간색 , 글자 하얀색 
        selectDay.GetComponent<Image>().color = new Color(0xFE / 255f, 0x22 / 255f, 0x04 / 255f); // #FE2204;
        selectDay.GetComponentInChildren<Text>().color = Color.white;



        //// 하단 정보 ----------------------------------------------------------------------------------------

        int day = int.Parse(selectDay.GetComponentInChildren<Text>().text);     // 선택한 날짜의 DateTime 가져오기
        DateTime selectedDate = new DateTime(currentDate.Year, currentDate.Month, day);

        CheckAndActivateObject(selectedDate);                                  // 선택한 날짜의 운동 기록 여부에 따라 오브젝트 A 또는 B 활성화

    }

    // 선택한 날짜의 운동 기록 여부에 따라 오브젝트 활성화
    private void CheckAndActivateObject(DateTime date)
    {
        string dateString = date.ToString("yyyy-MM-dd");
        if (GameData.instance.recordData.dailyRecords.ContainsKey(dateString))
        {
            // 운동 기록이 있는 경우 
            recordYes.SetActive(true);
            recordNone.SetActive(false);

            // Date 텍스트에 MM.DD 형태로 날짜 설정
            Date.text = date.ToString("MM.dd");



            // ClearMaps 클리어 운동 부위 목록 : GamePlayData 
            GamePlayData gamePlayData = GameData.instance.recordData.dailyRecords[dateString];

            ClearMaps.text = "";
            int totalPlayTime = 0;
            int totalCalories = 0;

            // 클리어한 운동 부위 별 계산 
            foreach (int map in gamePlayData.ClearedMaps)
            {
                // 운동 부위 문자열로 변환해 추가
                if (map >= 0 && map < bodyParts.Count)
                {
                    var bodyPart = bodyParts.ElementAt(map);

                    ClearMaps.text += bodyPart.Key + ", ";
                    totalPlayTime += bodyPart.Value.playTime;
                    totalCalories += bodyPart.Value.calories;
                }
            }
            ClearMaps.text = ClearMaps.text.TrimEnd(',', ' ');

            
            MapCount.text = gamePlayData.ClearedMaps.Count.ToString() + " 개";    // MapCount 클리어 운동 횟수 : GamePlayData 
            PlayTime.text = totalPlayTime.ToString() + " 분";                     // PlayTime 플레이 시간
            Kcal.text = totalCalories.ToString() + " Kcal";                       // Kcal 칼로리 소모

        }
        else
        {
            // 운동 기록이 없는 경우 
            recordNone.SetActive(true);
            recordYes.SetActive(false);
        }
    }

    // 운동 기록창 에서 뒤로가기 버튼 클릭
    public void ChangeMainScene()
    {
        SceneManager.LoadScene("MainScene");
    }
}
