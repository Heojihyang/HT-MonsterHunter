using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecordManager : MonoBehaviour
{
    // ���� ������ ǥ���� UI �ؽ�Ʈ ���
    public Text monthYearText;

    // ��¥�� ǥ���� ������Ʈ : ����Ʈ�� �޾ƾ��ҵ�...
    public GameObject dateCellPrefab;

    // ���� �޷��� ��¥�� �����ϴ� ����
    private DateTime currentDate;


    void Start()
    {
        // ���� ��¥
        // MM/DD/YYYY 00:00:00 
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

        /*
        // ������ ��� �� : ������ ��¥ �� ����
        foreach (Transform child in datesParent)
        {
            Destroy(child.gameObject);
        }
        */

        // �ش� ���� ù° ���� ���
        DateTime firstDayOfMonth = new DateTime(currentDate.Year, currentDate.Month, 1);
        DayOfWeek dayOfWeek = firstDayOfMonth.DayOfWeek;
        print("1���� ���� = " + dayOfWeek);

        // �ش� ���� ��¥ �� ���
        int daysInMonth = DateTime.DaysInMonth(currentDate.Year, currentDate.Month);
        print("�ش� ���� ��¥ ��  = " + daysInMonth);

        // ������ ��� �� : �ش� ���� �� ��¥�� ���� ��¥ �� ����
        for (int i = 1; i <= daysInMonth; i++)
        {
            // DateTime date = new DateTime(currentDate.Year, currentDate.Month, i);

            // ��¥ �� �������� �����Ͽ� ��¥�� �ؽ�Ʈ�� ǥ��
            // GameObject dateCell = Instantiate(dateCellPrefab, datesParent);
            // dateCell.GetComponentInChildren<Text>().text = i.ToString();

        }
        
    }

}
