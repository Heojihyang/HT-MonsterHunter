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
        Debug.Log("카메라(부모) - 정답모델(자식) 관계 설정 성공");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
