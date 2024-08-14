using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectExAnim : MonoBehaviour
{
    private Animator GuideAnim1;
    private Animator GuideAnim2;
    private Animator GuideAnim3;

    public GameObject Guide1;
    public GameObject Guide2;
    public GameObject Guide3;

    public GameObject Explain_LeftLeg1;    // 허벅지
    public GameObject Explain_RightArm1;   // 삼두

    void Start()
    {
        GuideAnim1 = Guide1.GetComponent<Animator>();
        GuideAnim2 = Guide2.GetComponent<Animator>();
        GuideAnim3 = Guide3.GetComponent<Animator>();

        if (Explain_LeftLeg1.activeSelf)
        {
            GuideAnim1.SetBool("SideLegRaise", true);
            GuideAnim2.SetBool("Squat", true);
            GuideAnim3.SetBool("Launge", true);
            

        }
        else if (Explain_RightArm1.activeSelf)
        {
            GuideAnim1.SetBool("Reize", true);
            GuideAnim2.SetBool("ShoulderPress", true);
            GuideAnim3.SetBool("KickBack", true);
        }
            
    }

    
    // 게임 오브젝트가 활성화될 때마다 호출
    void OnEnable()
    {
        if (Explain_LeftLeg1.activeSelf)
        {
            if (GuideAnim1 != null)
            {
                GuideAnim1.SetBool("SideLegRaise", true);
            }
            if (GuideAnim2 != null)
            {
                GuideAnim2.SetBool("Squat", true);
            }
            if (GuideAnim3 != null)
            {
                GuideAnim3.SetBool("Launge", true);
            }
        }
        else if (Explain_RightArm1.activeSelf)
        {
            if (GuideAnim1 != null)
            {
                GuideAnim1.SetBool("Reize", true);
            }
            if (GuideAnim2 != null)
            {
                GuideAnim2.SetBool("ShoulderPress", true);
            }
            if (GuideAnim3 != null)
            {
                GuideAnim3.SetBool("KickBack", true);
            }

        }
        
    }
       

}
