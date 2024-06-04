using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BulletGenerator : MonoBehaviour
{
    public GameObject bulletPrefab; // ������ �Ѿ� ������
    public Transform startPoint;    // ���� ��ġ
    public Transform endPoint;      // ��ǥ ����

    public GameObject effect;       // ����Ʈ

    public void ShootBullet()
    {

        // �Ѿ� �������� ����
        GameObject bullet = Instantiate(bulletPrefab, new Vector3(-1.19f, 0.57f, 2.0f), Quaternion.identity);

        bullet.GetComponent<BulletController>().endPosition = new Vector3(-6.33f, -2.6f, 1.88f);
        bullet.GetComponent<BulletController>().effect = this.effect;
    }
}
