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

    private bool sideLegRaiseState;
    private bool SquatState;
    private bool LaungeState;

    // Start is called before the first frame update
    void Start()
    {
        GuideAnim1 = Guide1.GetComponent<Animator>();
        GuideAnim2 = Guide2.GetComponent<Animator>();
        GuideAnim3 = Guide3.GetComponent<Animator>();

        sideLegRaiseState = true;
        SquatState = true;
        LaungeState = true;

        GuideAnim1.SetBool("SideLegRaise", sideLegRaiseState);
        GuideAnim2.SetBool("Squat", SquatState);
        GuideAnim3.SetBool("Launge", LaungeState);
    }

    // 게임 오브젝트가 활성화될 때마다 호출
    void OnEnable()
    {
        if (GuideAnim1 != null)
        {
            GuideAnim1.SetBool("SideLegRaise", sideLegRaiseState);
        }
        if (GuideAnim2 != null)
        {
            GuideAnim2.SetBool("Squat", SquatState);
        }
        if (GuideAnim3 != null)
        {
            GuideAnim3.SetBool("Launge", LaungeState);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
