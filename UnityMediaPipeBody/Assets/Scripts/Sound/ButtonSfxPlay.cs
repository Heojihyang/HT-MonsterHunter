using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSfxPlay : MonoBehaviour
{
    // 버튼 클릭 효과음
    /*
    SoundManager가 MainScene에서 먼저 생성되어야해서 효과음 출력만 스크립트 따로 만듦.
    */
    public void ButtonSoundPlay()
    {
        SoundManager.instance.PlaySFX("SFX_Btn");
    }
    
}
