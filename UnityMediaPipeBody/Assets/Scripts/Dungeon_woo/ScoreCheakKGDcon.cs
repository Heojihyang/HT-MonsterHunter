using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ScoreCheakKGDcon : MonoBehaviour
{
    public GameObject d;

    void Start()
    {
        if (d != null)
        {
            d.SetActive(false);
        }
        else Debug.Log("≥Œ¿Ã∑°");
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (d != null)
            {
                d.SetActive(!d.activeSelf);
            }
        }
    }
}

