using UnityEngine;
using System;

[Serializable]
public class Sound
{
    public string name;         // ������ �̸�
    public AudioClip clip;      // ����� Ŭ��
    [Range(0f, 1f)]
    public float volume = 0.7f; // ����
    [Range(0.1f, 3f)]
    public float pitch = 1f;    // ��ġ
    public bool loop = false;   // �ݺ� ����

    [HideInInspector]
    public AudioSource source;  // ����� �ҽ�
}
