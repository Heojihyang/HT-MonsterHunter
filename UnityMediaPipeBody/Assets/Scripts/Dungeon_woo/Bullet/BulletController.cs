using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BulletController : MonoBehaviour
{
    public Transform target;    // 목표 지점
    public float speed = 10f;    // 이동 속도
    public float height = 1.5f;   // 곡선의 높이

    private Vector3 startPos;       // 시작 위치
    private Vector3 controlPoint;   // 곡선의 제어점
    private float journeyLength;    // 이동 거리

    public GameObject effect;       // 이펙트

    void Start()
    {
        controlPoint = startPos + (target.position - startPos) / 2 + Vector3.up * height; // 제어점 계산
        journeyLength = Vector3.Distance(startPos, target.position);                      // 이동 거리 계산

        // 코루틴 시작
        StartCoroutine(MoveBullet());                                                     
    }

    IEnumerator MoveBullet()
    {
        float startTime = Time.time; // 시작 시간
        float distanceCovered = 0f;     // 이동한 거리

        while (distanceCovered < journeyLength)
        {
            float fracComplete = (Time.time - startTime) * speed / journeyLength; // 이동 진행도 계산
            transform.position = CalculateBezierPoint(startPos, controlPoint, target.position, fracComplete); // 현재 위치 설정
            distanceCovered = Vector3.Distance(startPos, transform.position); // 이동한 거리 업데이트
            yield return null;
        }

        // 총알
        Destroy(gameObject);

        // 이펙트
        GameObject newEffect = Instantiate(effect, target.position, Quaternion.identity);
        Destroy(newEffect, 1.5f);
    }

    // 베지어 곡선 위의 점 계산
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
