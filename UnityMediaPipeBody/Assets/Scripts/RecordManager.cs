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

    public GameObject[] DayButton = new GameObject[42];

    int daysInMonth;

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
            DayButton[i].transform.GetChild(0).GetComponent<Text>().text = "";
        }

        // �޷� ����
        for (int day = 1; day <= daysInMonth; day++)
        {
            int index = startDay + day - 1;
            DayButton[index].transform.GetChild(0).GetComponent<Text>().text = day.ToString();
        }


        //// �ش� ���� �� ���� ���� �޷� ��� heightũ�� ���� 


        #region * ����, ������ ��� �� ���� 

        /*
        // ������ ��� �� : ������ ��¥ �� ����
        foreach (Transform child in datesParent)
        {
            Destroy(child.gameObject);
        }
        */

        // ������ ��� �� : �ش� ���� �� ��¥�� ���� ��¥ �� ����
        /*
        for (int i = 1; i <= daysInMonth; i++)
        {
            // DateTime date = new DateTime(currentDate.Year, currentDate.Month, i);

            // ��¥ �� �������� �����Ͽ� ��¥�� �ؽ�Ʈ�� ǥ��
            // GameObject dateCell = Instantiate(dateCellPrefab, datesParent);
            // dateCell.GetComponentInChildren<Text>().text = i.ToString();

        }
        */
        #endregion
    }

}
