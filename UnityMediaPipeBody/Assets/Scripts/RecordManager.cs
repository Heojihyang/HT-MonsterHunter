using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class RecordManager : MonoBehaviour
{
    public Text monthYearText;           // ��, ���� ǥ��

    private DateTime currentDate;        // ���� �޷��� ��¥�� �����ϴ� ����

    private GameObject selectedDay;      // ���õ� ��¥ ��ư 
    public GameObject calendarParent;    // �޷� ��ư�� �����ϴ� �θ� ������Ʈ

    public GameObject recordNone;        // ��� ���� 
    public GameObject recordYes;         // ��� ���� 

    public Text Date;                    // ��¥
    public Text ClearMaps;               // �����
    public Text MapCount;                // Ŭ���� � Ƚ��
    public Text PlayTime;                // �÷��� �ð�
    public Text Kcal;                    // Į�θ� �Ҹ� 

    public GameObject[] DayButton = new GameObject[42];  // ��¥ ��ư
    
    private Dictionary<string, (int playTime, int calories)> bodyParts = new Dictionary<string, (int playTime, int calories)>
    {
        { "����", (5, 100) },
        { "��", (7, 50) },
        { "����", (8, 90) },
        { "�㸮", (11, 120) },
        { "�̵�", (8, 80) },
        { "���ϱ�", (10, 105) },
        { "���", (8, 100) },
        { "��", (5, 80) },
        { "�����", (6, 70) },
        { "���Ƹ�", (4, 65) }
    };

    private int daysInMonth;

    private Color previousDayColor;      // ������ ��¥ ������ ���� ����
    private Color previousDayTextColor;


    void Start()
    {
        currentDate = DateTime.Today;         // ���� ��¥  MM/DD/YYYY 00:00:00 

        UpdateCalendar();                     // ���� ��¥�� �޷� ������Ʈ 

        CheckAndActivateObject(currentDate);  // ��������â Ȱ��ȭ/��Ȱ��ȭ
    }


    // ���� �޷� �̵��ϴ� �Լ�
    public void NextMonth()
    {
        currentDate = currentDate.AddMonths(1);
        UpdateCalendar();
    }

    // ���� �޷� �̵��ϴ� �Լ�
    public void PreviousMonth()
    {
        currentDate = currentDate.AddMonths(-1);
        UpdateCalendar();
    }


    // �޷��� ������Ʈ�ϴ� �Լ�
    private void UpdateCalendar()
    {
        selectedDay = null;    // ������ �����ߴ� ��ư ���� �ʱ�ȭ (���� �̵� �� ���� ����)

        monthYearText.text = currentDate.ToString("yyyy�� M��");                           // ��ܿ� ���� ������ �� ǥ��

        DateTime firstDayOfMonth = new DateTime(currentDate.Year, currentDate.Month, 1);   // �ش� ���� ù° �� (����) : Ķ������ ���� ��ġ 
        DayOfWeek dayOfWeek = firstDayOfMonth.DayOfWeek;
        print("1���� ���� = " + dayOfWeek);

        daysInMonth = DateTime.DaysInMonth(currentDate.Year, currentDate.Month);           // �ش� ���� ��¥ �� ��� 
        print("�ش� ���� ��¥ ��  = " + daysInMonth);



        //// ��¥ ���� �ð�ȭ ----------------------------------------------------------------------------------------------------------------

        int startDay = (int)dayOfWeek;                 // �Ͽ��� ���� ���� ��ġ ����

        for (int i = 0; i < DayButton.Length; i++)     // ��¥ ��ư �ʱ�ȭ
        {
            DayButton[i].SetActive(false);
        }

        for (int day = 1; day <= daysInMonth; day++)   // �޷� ����
        {
            int index = startDay + day - 1;
            Text dayText = DayButton[index].transform.GetChild(0).GetComponent<Text>();
            dayText.text = day.ToString();
            DayButton[index].SetActive(true);

            DateTime currentDateInLoop = new DateTime(currentDate.Year, currentDate.Month, day);   // ���� ��¥ Ȯ��

            string dateString = currentDateInLoop.ToString("yyyy-MM-dd");                          // ���ð� ���� �޿� �ش��ϴ� � ����� �ִ��� Ȯ��

            // � ����� �ִ� ���
            if (GameData.instance.recordData.dailyRecords.ContainsKey(dateString))
            {
                // ����
                if (currentDate.Year == DateTime.Today.Year && currentDate.Month == DateTime.Today.Month && day == DateTime.Today.Day)
                {
                    dayText.color = new Color32(0xFE, 0x22, 0x04, 0xFF);   // ������
                    dayText.fontStyle = FontStyle.Bold;                    // ���� �۾�

                    DayButton[index].tag = "Today";                        // �±� �ο� 
                }
                // ������ �ƴ� ���
                else
                {
                    DayButton[index].GetComponent<Image>().color = new Color(0xFA / 255f, 0xB0 / 255f, 0x00 / 255f); // �����
                    dayText.color = Color.white;
                }
            }
            // � ����� ���� ���
            else
            {
                DayButton[index].GetComponent<Image>().color = Color.white;    // �⺻ ��ư �� 
                dayText.fontStyle = FontStyle.Normal;                          // �⺻ �۾�

                // ����
                if (currentDate.Year == DateTime.Today.Year && currentDate.Month == DateTime.Today.Month && day == DateTime.Today.Day)
                {
                    dayText.color = new Color32(0xFE, 0x22, 0x04, 0xFF);   // ������

                    DayButton[index].tag = "Today";                        // �±� �ο� 
                }
                // ������ �ƴ� ���
                else
                {
                    dayText.color = Color.black;                           // ������
                }
            }
        }

    }
    


    // ������ ��¥ �ð�ȭ
    // ��ư Ŭ�� ��, Ŭ���� ������Ʈ ������ 
    public void SelectDay(GameObject selectDay)
    {
        // ������ Ŭ���� ��ư�� �ִٸ� && ������ �ƴϾ����� -> ��ư �ʱ�ȭ 
        // SelectedDay : ������ Ŭ���� ��ư 
        if((selectedDay != null)&&(!selectedDay.CompareTag("Today")))
        {
            // Ȱ��ȭ �Ǿ��ִ� ��ư�� ���� �������� ����
            selectedDay.GetComponent<Image>().color = previousDayColor;
            selectedDay.GetComponentInChildren<Text>().color = previousDayTextColor;
        }

        selectedDay = selectDay;   // ���Ӱ� Ŭ���� ��ư���� �Ҵ� ���� 

        // �����̸� : ��ȭ X 
        if (selectDay.CompareTag("Today"))
        {
            print("������ Ŭ���߽��ϴ�.");
            CheckAndActivateObject(DateTime.Today);
            return;
        }

        previousDayColor = selectDay.GetComponent<Image>().color;  // ���� ��ư�� ���� ����
        previousDayTextColor = selectDay.GetComponentInChildren<Text>().color;

        // ������ �ƴϸ� : ��ư�� ������ , ���� �Ͼ�� 
        selectDay.GetComponent<Image>().color = new Color(0xFE / 255f, 0x22 / 255f, 0x04 / 255f); // #FE2204;
        selectDay.GetComponentInChildren<Text>().color = Color.white;



        //// �ϴ� ���� ----------------------------------------------------------------------------------------

        int day = int.Parse(selectDay.GetComponentInChildren<Text>().text);     // ������ ��¥�� DateTime ��������
        DateTime selectedDate = new DateTime(currentDate.Year, currentDate.Month, day);

        CheckAndActivateObject(selectedDate);                                  // ������ ��¥�� � ��� ���ο� ���� ������Ʈ A �Ǵ� B Ȱ��ȭ

    }

    // ������ ��¥�� � ��� ���ο� ���� ������Ʈ Ȱ��ȭ
    private void CheckAndActivateObject(DateTime date)
    {
        string dateString = date.ToString("yyyy-MM-dd");
        if (GameData.instance.recordData.dailyRecords.ContainsKey(dateString))
        {
            // � ����� �ִ� ��� 
            recordYes.SetActive(true);
            recordNone.SetActive(false);

            // Date �ؽ�Ʈ�� MM.DD ���·� ��¥ ����
            Date.text = date.ToString("MM.dd");



            // ClearMaps Ŭ���� � ���� ��� : GamePlayData 
            GamePlayData gamePlayData = GameData.instance.recordData.dailyRecords[dateString];

            ClearMaps.text = "";
            int totalPlayTime = 0;
            int totalCalories = 0;

            // Ŭ������ � ���� �� ��� 
            foreach (int map in gamePlayData.ClearedMaps)
            {
                // � ���� ���ڿ��� ��ȯ�� �߰�
                if (map >= 0 && map < bodyParts.Count)
                {
                    var bodyPart = bodyParts.ElementAt(map);

                    ClearMaps.text += bodyPart.Key + ", ";
                    totalPlayTime += bodyPart.Value.playTime;
                    totalCalories += bodyPart.Value.calories;
                }
            }
            ClearMaps.text = ClearMaps.text.TrimEnd(',', ' ');

            
            MapCount.text = gamePlayData.ClearedMaps.Count.ToString() + " ��";    // MapCount Ŭ���� � Ƚ�� : GamePlayData 
            PlayTime.text = totalPlayTime.ToString() + " ��";                     // PlayTime �÷��� �ð�
            Kcal.text = totalCalories.ToString() + " Kcal";                       // Kcal Į�θ� �Ҹ�

        }
        else
        {
            // � ����� ���� ��� 
            recordNone.SetActive(true);
            recordYes.SetActive(false);
        }
    }

    // � ���â ���� �ڷΰ��� ��ư Ŭ��
    public void ChangeMainScene()
    {
        SceneManager.LoadScene("MainScene");
    }
}
