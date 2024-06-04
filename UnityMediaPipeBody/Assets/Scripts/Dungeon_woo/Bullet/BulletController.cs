using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BulletController : MonoBehaviour
{
    public Vector3 startPosition; // 시작 지점
    public Vector3 endPosition;   // 목표 지점
    
    public float speed = 10.0f;      // 이동 속도
    private float startTime;         // 시작 시간
    private float journeyLength;     // 이동 거리

    public GameObject effect;        // 이펙트

    void Start()
    {
        // 시작 위치 설정
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
            // 총알 디스토리
            Destroy(gameObject);

            // 이펙트
            GameObject newEffect = Instantiate(effect, endPosition, Quaternion.identity);
            Destroy(newEffect, 1.5f);
        }        
    }
}
