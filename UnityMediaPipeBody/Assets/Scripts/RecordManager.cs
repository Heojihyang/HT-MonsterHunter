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

    public GameObject WhiteBackground;

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
            DayButton[index].transform.GetChild(0).GetComponent<Text>().text = day.ToString();
            DayButton[index].SetActive(true);
        }



    }

    // � ���â ���� �ڷΰ��� ��ư Ŭ��
    public void ChangeMainScene()
    {
        SceneManager.LoadScene("MainScene");
    }
}
