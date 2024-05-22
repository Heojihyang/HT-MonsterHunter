using UnityEngine;
using System;

[Serializable]
public class Sound
{
    public string name;         // 사운드의 이름
    public AudioClip clip;      // 오디오 클립
    [Range(0f, 1f)]
    public float volume = 0.7f; // 볼륨
    [Range(0.1f, 3f)]
    public float pitch = 1f;    // 피치
    public bool loop = false;   // 반복 여부

    [HideInInspector]
    public AudioSource source;  // 오디오 소스
}
