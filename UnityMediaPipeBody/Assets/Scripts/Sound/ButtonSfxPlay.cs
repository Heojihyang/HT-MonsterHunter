using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSfxPlay : MonoBehaviour
{
    // ��ư Ŭ�� ȿ����
    /*
    SoundManager�� MainScene���� ���� �����Ǿ���ؼ� ȿ���� ��¸� ��ũ��Ʈ ���� ����.
    */
    public void ButtonSoundPlay()
    {
        SoundManager.instance.PlaySFX("SFX_Btn");
    }
    
}
