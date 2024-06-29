using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BulletGenerator : MonoBehaviour
{
    public GameObject[] bulletPrefabs; // ������ �Ѿ� ������
    //public GameObject[] effects;       // ����Ʈ

    public Transform startPoint;    // ���� ��ġ
    public Transform endPoint;      // ��ǥ ����
    
    
    public void ShootBullet(int n)
    {
        // �ǰ� �ܰ迡 ���� �Ѿ� ������ ������ ����Ʈ ����
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
            Debug.Log("�ǰ� �ܰ� ����, n�� �̻�");
        }        
    }
}
