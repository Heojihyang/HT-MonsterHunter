using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorrectMoving : MonoBehaviour
{
    public Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        transform.SetParent(mainCamera.transform, false);
        Debug.Log("ī�޶�(�θ�) - �����(�ڽ�) ���� ���� ����");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
