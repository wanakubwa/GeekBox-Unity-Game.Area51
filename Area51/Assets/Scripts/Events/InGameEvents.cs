using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InGameEvents
{
    // When lvl counter can start
    public delegate void StartCountingLvlTime();
    public static event StartCountingLvlTime startCountingTimeEvent;

    // Player win lvl event
    public delegate void PLayerWinLvl();
    public static event PLayerWinLvl playerWinLvlEvent;

    // Player dead - event
    public delegate void PlayerDead();
    public static event PlayerDead playerDeadEvent;

    // Bullet destroyed - event
    public delegate void DestroyBullet();
    public static event DestroyBullet destroyBulletEvent;

    public delegate void UIButtonPress();
    public static event UIButtonPress uiButtonPressEvent;

    // Music event
    public delegate void StartLvlMusic();
    public static event StartLvlMusic startLvlMusicEvent;

    // Entry lvl animation was ended - Invoke
    public static void CallStartCountingLvlTimeEvent()
    {
        // no subscribers == null
        if (startCountingTimeEvent != null)
            startCountingTimeEvent.Invoke();
    }

    // Player win lvl - Invoke
    public static void CallPlayerWinLvlEvent()
    {
        if (playerWinLvlEvent != null)
            playerWinLvlEvent.Invoke();
    }

    // Player dead - Invoke
    public static void CallPlayerDeadEvent()
    {
        if (playerDeadEvent != null)
            playerDeadEvent.Invoke();
    }

    public static void CallDestroyBulletEvent()
    {
        if (destroyBulletEvent != null)
            destroyBulletEvent.Invoke();
    }

    public static void CallUIButtonPress()
    {
        if (uiButtonPressEvent != null)
            uiButtonPressEvent.Invoke();
    }

    public static void CallStartLvlMusicEvent()
    {
        if (startLvlMusicEvent != null)
            startLvlMusicEvent.Invoke();
    }
}