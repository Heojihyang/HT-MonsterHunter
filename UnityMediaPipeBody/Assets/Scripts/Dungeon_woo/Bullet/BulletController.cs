using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BulletController : MonoBehaviour
{
    public Transform target;    // ��ǥ ����
    public float speed = 10f;    // �̵� �ӵ�
    public float height = 1.5f;   // ��� ����

    private Vector3 startPos;       // ���� ��ġ
    private Vector3 controlPoint;   // ��� ������
    private float journeyLength;    // �̵� �Ÿ�

    public GameObject effect;       // ����Ʈ

    void Start()
    {
        controlPoint = startPos + (target.position - startPos) / 2 + Vector3.up * height; // ������ ���
        journeyLength = Vector3.Distance(startPos, target.position);                      // �̵� �Ÿ� ���

        // �ڷ�ƾ ����
        StartCoroutine(MoveBullet());                                                     
    }

    IEnumerator MoveBullet()
    {
        float startTime = Time.time; // ���� �ð�
        float distanceCovered = 0f;     // �̵��� �Ÿ�

        while (distanceCovered < journeyLength)
        {
            float fracComplete = (Time.time - startTime) * speed / journeyLength; // �̵� ���൵ ���
            transform.position = CalculateBezierPoint(startPos, controlPoint, target.position, fracComplete); // ���� ��ġ ����
            distanceCovered = Vector3.Distance(startPos, transform.position); // �̵��� �Ÿ� ������Ʈ
            yield return null;
        }

        // �Ѿ�
        Destroy(gameObject);

        // ����Ʈ
        GameObject newEffect = Instantiate(effect, target.position, Quaternion.identity);
        Destroy(newEffect, 1.5f);
    }

    // ������ � ���� �� ���
    private Vector3 CalculateBezierPoint(Vector3 p0, Vector3 p1, Vector3 p2, float t)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        Vector3 p = uu * p0;
        p += 2 * u * t * p1;
        p += tt * p2;
        return p;
    }
}
