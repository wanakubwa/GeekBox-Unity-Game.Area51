using UnityEngine;
using UnityEditor;

public static class InUIEvents
{
    public delegate void MainMenuButton();
    public static event MainMenuButton mainMenuButtonEvent;

    public static void CallMainMenuButtonEvent()
    {
        // no subscribers == null
        if (mainMenuButtonEvent != null)
            mainMenuButtonEvent.Invoke();
    }
}