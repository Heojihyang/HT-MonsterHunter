using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideModelController : MonoBehaviour
{
    public Camera mainCamera;

    void Start()
    {
        this.transform.SetParent(mainCamera.transform, false);
    }
}
