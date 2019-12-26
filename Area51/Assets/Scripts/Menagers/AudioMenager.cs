using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMenager : MonoBehaviour
{
    [SerializeField] AudioClip buttonUIPress;
    [SerializeField] float buttonUIPressVolume = 0.75f;
    [SerializeField] AudioClip noiseTransition;
    [SerializeField] float noiseTransitionVolume = 0.55f;
    [SerializeField] GameObject lvlMusicSource;
    [SerializeField] GameObject uiMusicSouce;

    private void Start()
    {
        InGameEvents.uiButtonPressEvent += UIButtonPress_Handler;
        InGameEvents.startLvlMusicEvent += StartPlayLvlMusic_Handler;
        InGameEvents.playerDeadEvent += EndLvl_Handler;
        InGameEvents.playerWinLvlEvent += EndLvl_Handler;
        InUIEvents.mainMenuButtonEvent += MenuButtonCall_Handler;
        lvlMusicSource.SetActive(false);
        uiMusicSouce.SetActive(false);
    }

    private void UIButtonPress_Handler()
    {
        AudioSource.PlayClipAtPoint(buttonUIPress, Camera.main.transform.position, buttonUIPressVolume);
    }

    private void StartPlayLvlMusic_Handler()
    {
        lvlMusicSource.SetActive(true);
        uiMusicSouce.SetActive(false);
    }

    private void EndLvl_Handler()
    {
        lvlMusicSource.SetActive(false);
        AudioSource.PlayClipAtPoint(noiseTransition, Camera.main.transform.position, noiseTransitionVolume);
        uiMusicSouce.SetActive(true);
    }

    private void MenuButtonCall_Handler()
    {
        uiMusicSouce.SetActive(false);
    }
}
