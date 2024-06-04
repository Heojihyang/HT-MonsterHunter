using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BulletController : MonoBehaviour
{
    public Vector3 startPosition; // ���� ����
    public Vector3 endPosition;   // ��ǥ ����
    
    public float speed = 10.0f;      // �̵� �ӵ�
    private float startTime;         // ���� �ð�
    private float journeyLength;     // �̵� �Ÿ�

    public GameObject effect;        // ����Ʈ

    void Start()
    {
        // ���� ��ġ ����
        startPosition = this.transform.position;

        startTime = Time.time;
        journeyLength = Vector3.Distance(startPosition, endPosition);
    }

    private void Update()
    {
        float distanceCovered = (Time.time - startTime) * speed;
        float fractionOfJourney = distanceCovered / journeyLength;
        transform.position = Vector3.Lerp(startPosition, endPosition, fractionOfJourney);

        if (fractionOfJourney >= 1.0f)
        {
            // �Ѿ� ���丮
            Destroy(gameObject);

            // ����Ʈ
            GameObject newEffect = Instantiate(effect, endPosition, Quaternion.identity);
            Destroy(newEffect, 1.5f);
        }        
    }
}
