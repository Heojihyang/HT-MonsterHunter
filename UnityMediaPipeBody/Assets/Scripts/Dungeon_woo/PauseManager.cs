using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PauseManager : MonoBehaviour
{
    public GameObject pausePanel;       // 일시정지 상태를 표시하는 패널
    private bool isPaused = false;      // 게임의 일시정지 상태를 추적
    private List<AudioSource> allAudioSources; // 모든 오디오 소스를 추적

    void Start()
    {
        // 초기화
        pausePanel.SetActive(false);
        allAudioSources = new List<AudioSource>(FindObjectsOfType<AudioSource>());
    }

    // 일시정지 버튼이 눌렸을때(일지정지 버튼, [ESC])
    public void TogglePause()
    {
        isPaused = !isPaused;   // 일시정지 상태 반전

        
        if (isPaused)   // 일지정지
        {
            Time.timeScale = 0f;        // 게임 일시정지
            pausePanel.SetActive(true); // 일지정시 패널 활성화
            PauseAllAudio();            // 모든 오디오소스를 일시정지
        }
        else            // 활성화
        {
            Time.timeScale = 1f;            // 게임 재개
            pausePanel.SetActive(false);    // 일지정시 패널 비활성화
            UnPauseAllAudio();              // 모든 오디오소스를 재개
        }
    }

    // 오디오소스 일시정지
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

    // 오디오소스 재개
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
