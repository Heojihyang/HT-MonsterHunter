using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // GameData.instance.LoadMonsterData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeMainScene()
    {
        SceneManager.LoadScene("MainScene");
    }
}
