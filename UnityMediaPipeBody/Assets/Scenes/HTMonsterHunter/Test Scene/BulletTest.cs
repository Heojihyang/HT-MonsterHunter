using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTest : MonoBehaviour
{
    public GameObject bulletGenerator;

    // Start is called before the first frame update
    void Start()
    {
        bulletGenerator.GetComponent<BulletGenerator>().ShootBullet(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
