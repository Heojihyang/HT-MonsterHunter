using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BulletGenerator : MonoBehaviour
{
    public GameObject bulletPrefab;         // 생성할 총알 프리팹
    public Transform startPoint;    // 시작 위치
    public Transform endPoint;      // 목표 지점

    public GameObject effect;       // 이펙트

    public void ShootBullet()
    {
        // 총알 프리팹을 생성
        GameObject bullet = Instantiate(bulletPrefab, startPoint.position, Quaternion.identity);

        bullet.GetComponent<BulletController>().target = endPoint;
        bullet.GetComponent<BulletController>().effect = this.effect;
    }
}
