using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagMonsterController : MonoBehaviour
{
    public Animator bagAnimator;
    void Start()
    {
        bagAnimator = GetComponent<Animator>();
    }

    public void Hit()
    {
        bagAnimator.SetBool("ani_Damage", true);
    }

    public void Idle()
    {
        bagAnimator.SetBool("ani_Damage", false);
    }
}
