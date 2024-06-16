using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideTest1 : MonoBehaviour
{
    public GameObject guideModel;
    public Animator animator;

    void Start()
    {
        animator = guideModel.GetComponent<Animator>();
        animator.SetBool("Squat", true);
    }
}
