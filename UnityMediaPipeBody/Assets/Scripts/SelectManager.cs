using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectManager : MonoBehaviour
{
    public GameObject Explain;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // (허벅지) 클릭 시 : 운동설명창 활성화
    public void ActExerciseExplain()
    {
        Explain.SetActive(true);
    }
}
