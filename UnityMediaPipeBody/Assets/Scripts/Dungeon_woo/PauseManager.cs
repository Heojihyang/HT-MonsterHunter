using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PauseManager : MonoBehaviour
{
    public GameObject pausePanel;       // �Ͻ����� ���¸� ǥ���ϴ� �г�
    private bool isPaused = false;      // ������ �Ͻ����� ���¸� ����
    private List<AudioSource> allAudioSources; // ��� ����� �ҽ��� ����

    void Start()
    {
        // �ʱ�ȭ
        pausePanel.SetActive(false);
        allAudioSources = new List<AudioSource>(FindObjectsOfType<AudioSource>());
    }

    // �Ͻ����� ��ư�� ��������(�������� ��ư, [ESC])
    public void TogglePause()
    {
        isPaused = !isPaused;   // �Ͻ����� ���� ����

        
        if (isPaused)   // ��������
        {
            Time.timeScale = 0f;        // ���� �Ͻ�����
            pausePanel.SetActive(true); // �������� �г� Ȱ��ȭ
            PauseAllAudio();            // ��� ������ҽ��� �Ͻ�����
        }
        else            // Ȱ��ȭ
        {
            Time.timeScale = 1f;            // ���� �簳
            pausePanel.SetActive(false);    // �������� �г� ��Ȱ��ȭ
            UnPauseAllAudio();              // ��� ������ҽ��� �簳
        }
    }

    // ������ҽ� �Ͻ�����
    private void PauseAllAudio()
    {
        foreach (AudioSource audio in allAudioSources)
        {
            if (audio.isPlaying)
            {
                audio.Pause();
            }
        }
    }

    // ������ҽ� �簳
    private void UnPauseAllAudio()
    {
        foreach (AudioSource audio in allAudioSources)
        {
            if (audio.clip != null && audio.time > 0)
            {
                audio.UnPause();
            }
        }
    }
}
