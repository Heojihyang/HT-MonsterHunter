using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BulletGenerator : MonoBehaviour
{
    public GameObject bulletPrefab;         // ������ �Ѿ� ������
    public Transform startPoint;    // ���� ��ġ
    public Transform endPoint;      // ��ǥ ����

    public GameObject effect;       // ����Ʈ

    public void ShootBullet()
    {
        // �Ѿ� �������� ����
        GameObject bullet = Instantiate(bulletPrefab, startPoint.position, Quaternion.identity);

        bullet.GetComponent<BulletController>().target = endPoint;
        bullet.GetComponent<BulletController>().effect = this.effect;
    }
}
