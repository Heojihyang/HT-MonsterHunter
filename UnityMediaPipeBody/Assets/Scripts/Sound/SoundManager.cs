using UnityEngine;
using System;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance; // �̱��� �ν��Ͻ�

    public Sound[] bgmSounds;            // ��� ���� �迭
    public Sound[] sfxSounds;            // ȿ���� �迭

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // �� �̵��ÿ��� Destroy X (MainSecen�� �־��)
        DontDestroyOnLoad(gameObject);    

        // BGM ����
        foreach (Sound s in bgmSounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }

        // ȿ���� ����
        foreach (Sound s in sfxSounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    // BGM Play
    public void PlayBGM(string soundName)
    {
        Sound s = Array.Find(bgmSounds, sound => sound.name == soundName);
        if (s == null)
        {
            Debug.LogWarning("BGM: '" + soundName + "'�� ã�� �� �����ϴ�!");
            return;
        }
        s.source.Play();
    }

    // BGM Stop
    public void StopBGM(string soundName)
    {
        Sound s = Array.Find(bgmSounds, sound => sound.name == soundName);
        if (s == null)
        {
            Debug.LogWarning("BGM: '" + soundName + "'�� ã�� �� �����ϴ�!");
            return;
        }
        s.source.Stop();
    }

    // ȿ���� Play
    public void PlaySFX(string soundName)
    {
        Sound s = Array.Find(sfxSounds, sound => sound.name == soundName);
        if (s == null)
        {
            Debug.LogWarning("SFX: '" + soundName + "'�� ã�� �� ��������!");
            return;
        }
        s.source.Play();
    }

    // ȿ���� Stop
    public void StopSFX(string soundName)
    {
        Sound s = Array.Find(sfxSounds, sound => sound.name == soundName);
        if (s == null)
        {
            Debug.LogWarning("SFX: '" + soundName + "' �� ã�� �� �����ϴ�!");
            return;
        }
        s.source.Stop();
    }
}
