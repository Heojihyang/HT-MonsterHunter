using UnityEngine;
using System;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance; // 싱글톤 인스턴스

    public Sound[] bgmSounds;            // 배경 음악 배열
    public Sound[] sfxSounds;            // 효과음 배열

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

        // 씬 이동시에도 Destroy X (MainSecen에 넣어둠)
        DontDestroyOnLoad(gameObject);    

        // BGM 세팅
        foreach (Sound s in bgmSounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }

        // 효과음 세팅
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
            Debug.LogWarning("BGM: '" + soundName + "'을 찾을 수 없습니다!");
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
            Debug.LogWarning("BGM: '" + soundName + "'을 찾을 수 없습니다!");
            return;
        }
        s.source.Stop();
    }

    // 효과음 Play
    public void PlaySFX(string soundName)
    {
        Sound s = Array.Find(sfxSounds, sound => sound.name == soundName);
        if (s == null)
        {
            Debug.LogWarning("SFX: '" + soundName + "'을 찾을 수 없습ㄴ디ㅏ!");
            return;
        }
        s.source.Play();
    }

    // 효과음 Stop
    public void StopSFX(string soundName)
    {
        Sound s = Array.Find(sfxSounds, sound => sound.name == soundName);
        if (s == null)
        {
            Debug.LogWarning("SFX: '" + soundName + "' 을 찾을 수 없습니다!");
            return;
        }
        s.source.Stop();
    }
}
