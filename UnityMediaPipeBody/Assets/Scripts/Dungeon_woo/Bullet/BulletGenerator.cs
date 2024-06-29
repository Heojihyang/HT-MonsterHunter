using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BulletGenerator : MonoBehaviour
{
    public GameObject[] bulletPrefabs; // 생성할 총알 프리팹
    //public GameObject[] effects;       // 이펙트

    public Transform startPoint;    // 시작 위치
    public Transform endPoint;      // 목표 지점
    
    
    public void ShootBullet(int n)
    {
        // 피격 단계에 따라 총알 프리팹 생성과 이펙트 전달
        if (n == 0)
        {
            GameObject bullet = Instantiate(bulletPrefabs[0], new Vector3(-1.19f, 0.57f, 2.0f), Quaternion.identity);
            bullet.GetComponent<BulletController>().endPosition = new Vector3(-6.33f, -2.6f, 1.88f);
            //bullet.GetComponent<BulletController>().effect = this.effects[0];
        }
        else if(n == 1)
        {
            GameObject bullet = Instantiate(bulletPrefabs[1], new Vector3(-1.19f, 0.57f, 2.0f), Quaternion.identity);
            bullet.GetComponent<BulletController>().endPosition = new Vector3(-6.33f, -2.6f, 1.88f);
            //bullet.GetComponent<BulletController>().effect = this.effects[1];
        }
        else if(n==2)
        {
            GameObject bullet = Instantiate(bulletPrefabs[2], new Vector3(-1.19f, 0.57f, 2.0f), Quaternion.identity);
            bullet.GetComponent<BulletController>().endPosition = new Vector3(-6.33f, -2.6f, 1.88f);
            //bullet.GetComponent<BulletController>().effect = this.effects[2];
        }
        else
        {
            Debug.Log("피격 단계 오류, n값 이상");
        }        
    }
}
