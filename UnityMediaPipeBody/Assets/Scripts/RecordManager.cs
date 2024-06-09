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

    public GameObject[] DayButton = new GameObject[42];

    int daysInMonth;


    void Start()
    {
        // ���� ��¥  MM/DD/YYYY 00:00:00 
        currentDate = DateTime.Today; 

        // ���� ��¥�� �޷� ������Ʈ 
        UpdateCalendar();
    }

    void Update()
    {
        
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

        // � ����� �ִ� �� : ��ư ���� ��Ȳ�� FAB000
        // (������ ����) 

    }

    // ������ ��¥ �ð�ȭ
    // ��ư Ŭ�� ��, Ŭ���� ������Ʈ ������ 
    public void SelectDay(GameObject selectDay)
    {
        // ������ Ŭ���� ��ư�� �ִٸ� && ������ �ƴϾ����� -> ��ư �ʱ�ȭ 
        // SelectedDay : ������ Ŭ���� ��ư 
        if((selectedDay != null)&&(!selectedDay.CompareTag("Today")))
        {
            // Ȱ��ȭ �Ǿ��ִ� ��ư��
            // ��ư ���� : ���
            // �ؽ�Ʈ ���� : ������
            // ���� ���� 
            selectedDay.GetComponent<Image>().color = Color.white;
            selectedDay.GetComponentInChildren<Text>().color = Color.black;
        }

        // ���Ӱ� Ŭ���� ��ư���� �Ҵ� ���� 
        selectedDay = selectDay;

        // �����̸� : ��ȭ X 
        if(selectDay.CompareTag("Today"))
        {
            print("������ Ŭ���߽��ϴ�.");
            return;
        }

        // ������ �ƴϸ� : ��ư�� ������ , ���� �Ͼ�� 
        selectDay.GetComponent<Image>().color = new Color(0xFE / 255f, 0x22 / 255f, 0x04 / 255f); // #FE2204;
        selectDay.GetComponentInChildren<Text>().color = Color.white;

        // �ϴ� ���� 

    }

    // � ���â ���� �ڷΰ��� ��ư Ŭ��
    public void ChangeMainScene()
    {
        SceneManager.LoadScene("MainScene");
    }
}
