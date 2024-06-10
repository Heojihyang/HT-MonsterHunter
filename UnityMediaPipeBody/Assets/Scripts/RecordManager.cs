using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RecordManager : MonoBehaviour
{
    // ���� ������ ǥ���� UI �ؽ�Ʈ ���
    public Text monthYearText;

    // ���� �޷��� ��¥�� �����ϴ� ����
    private DateTime currentDate;

    private GameObject selectedDay; // ���õ� ��¥ ��ư 
    public GameObject calendarParent; // �޷� ��ư�� �����ϴ� �θ� ������Ʈ

    public GameObject recordNone; // ��� ���� 
    public GameObject recordYes; // ��� ���� 
    public Text Date; // ��¥
    public Text ClearMaps; // �����
    public Text MapCount; // Ŭ���� � Ƚ��
    public Text PlayTime; // �÷��� �ð�
    public Text Kcal; // Į�θ� �Ҹ� 

    public GameObject[] DayButton = new GameObject[42];
    private string[] bodyParts = { "����", "��", "����", "�㸮", "�̵�" , "���ϱ�", "���", "��", "�����", "���Ƹ�" };


    int daysInMonth;

    // ������ ��¥ ������ ������ �����ϴ� ����
    private Color previousDayColor;
    private Color previousDayTextColor;

    void Start()
    {
        // ���� ��¥  MM/DD/YYYY 00:00:00 
        currentDate = DateTime.Today; 

        // ���� ��¥�� �޷� ������Ʈ 
        UpdateCalendar();

        // ó�� ������ �� ���� ��¥�� � ��� ���ο� ���� RecordInfo Ȱ��ȭ 
        CheckAndActivateObject(currentDate);
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
        // ������ �����ߴ� ��ư ���� �ʱ�ȭ (���� �̵� �� ���� ����)
        selectedDay = null;
        
        // ��ܿ� ���� ������ �� ǥ��
        monthYearText.text = currentDate.ToString("yyyy�� M��");

        // �ش� ���� ù° �� (����) : Ķ������ ���� ��ġ 
        DateTime firstDayOfMonth = new DateTime(currentDate.Year, currentDate.Month, 1);
        DayOfWeek dayOfWeek = firstDayOfMonth.DayOfWeek;
        print("1���� ���� = " + dayOfWeek);

        // �ش� ���� ��¥ �� ��� 
        daysInMonth = DateTime.DaysInMonth(currentDate.Year, currentDate.Month);
        print("�ش� ���� ��¥ ��  = " + daysInMonth);



        //// ��¥ ���� �ð�ȭ 

        // �Ͽ��� ���� ���� ��ġ ����
        int startDay = (int)dayOfWeek;

        // ��¥ ��ư �ʱ�ȭ
        for (int i = 0; i < DayButton.Length; i++)
        {
            DayButton[i].SetActive(false);
        }

        // �޷� ����
        for (int day = 1; day <= daysInMonth; day++)
        {
            int index = startDay + day - 1;
            Text dayText = DayButton[index].transform.GetChild(0).GetComponent<Text>();
            dayText.text = day.ToString();
            DayButton[index].SetActive(true);

            // ���� ��¥ Ȯ��
            DateTime currentDateInLoop = new DateTime(currentDate.Year, currentDate.Month, day);

            // ���ð� ���� �޿� �ش��ϴ� � ����� �ִ��� Ȯ��
            string dateString = currentDateInLoop.ToString("yyyy-MM-dd");
            if (GameData.instance.recordData.dailyRecords.ContainsKey(dateString))
            {
                // � ����� �ִ� ���
                if (currentDate.Year == DateTime.Today.Year && currentDate.Month == DateTime.Today.Month && day == DateTime.Today.Day)
                {
                    // ������ ��� ������ ���� �۾��� ����
                    dayText.color = new Color32(0xFE, 0x22, 0x04, 0xFF); // ������
                    dayText.fontStyle = FontStyle.Bold; // ���� �۾�

                    DayButton[index].tag = "Today"; // �±� �ο� 
                }
                else
                {
                    // ������ �ƴ� ��� ��ư�� ��������� �����ϰ� �ؽ�Ʈ�� ������� ����
                    DayButton[index].GetComponent<Image>().color = new Color(0xFA / 255f, 0xB0 / 255f, 0x00 / 255f); // �����
                    dayText.color = Color.white;
                }
            }
            else
            {
                // � ����� ���� ��� ��ư�� ���� �⺻ �������� ����
                DayButton[index].GetComponent<Image>().color = Color.white;
                dayText.fontStyle = FontStyle.Normal; // �ؽ�Ʈ�� �⺻ �۾��� ����

                // ���� ��¥ : �ؽ�Ʈ ������ ���������� ����
                if (currentDate.Year == DateTime.Today.Year && currentDate.Month == DateTime.Today.Month && day == DateTime.Today.Day)
                {
                    dayText.color = new Color32(0xFE, 0x22, 0x04, 0xFF); // ������
                    DayButton[index].tag = "Today"; // �±� �ο� 
                }
                else
                {
                    // �⺻ �������� ���� (��: ������)
                    dayText.color = Color.black;
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

        // ���Ӱ� Ŭ���� ��ư���� �Ҵ� ���� 
        selectedDay = selectDay;

        // �����̸� : ��ȭ X 
        if(selectDay.CompareTag("Today"))
        {
            print("������ Ŭ���߽��ϴ�.");
            CheckAndActivateObject(DateTime.Today);
            return;
        }

        // ���� ��ư�� ������ ����
        previousDayColor = selectDay.GetComponent<Image>().color;
        previousDayTextColor = selectDay.GetComponentInChildren<Text>().color;

        // ������ �ƴϸ� : ��ư�� ������ , ���� �Ͼ�� 
        selectDay.GetComponent<Image>().color = new Color(0xFE / 255f, 0x22 / 255f, 0x04 / 255f); // #FE2204;
        selectDay.GetComponentInChildren<Text>().color = Color.white;


        //// �ϴ� ���� 

        // ������ ��¥�� DateTime ��������
        int day = int.Parse(selectDay.GetComponentInChildren<Text>().text);
        DateTime selectedDate = new DateTime(currentDate.Year, currentDate.Month, day);

        // ������ ��¥�� � ��� ���ο� ���� ������Ʈ A �Ǵ� B Ȱ��ȭ
        CheckAndActivateObject(selectedDate);

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

            GamePlayData gamePlayData = GameData.instance.recordData.dailyRecords[dateString];
            ClearMaps.text = " ";
            foreach (int map in gamePlayData.ClearedMaps)
            {
                // � ���� ���ڿ��� ��ȯ�� �߰�
                if (map >= 0 && map < bodyParts.Length)
                {
                    ClearMaps.text += bodyParts[map] + ", ";
                }
            }
            ClearMaps.text = ClearMaps.text.TrimEnd(',', ' ');

            // MapCount Ŭ���� � Ƚ�� : GamePlayData 
            MapCount.text = gamePlayData.ClearedMaps.Count.ToString() + " ��";

            // PlayTime �÷��� �ð� (���� 0)
            PlayTime.text = " 0 s";

            // Kcal Į�θ� �Ҹ� (���� 0)
            Kcal.text = "0 Kcal";

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
