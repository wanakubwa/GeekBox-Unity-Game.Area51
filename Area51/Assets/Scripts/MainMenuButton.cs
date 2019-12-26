using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenuButton : MonoBehaviour
{
    [SerializeField] Button buttonScript;
    [SerializeField] int lvlIndex = 0;
    GameMenager gameMenager;
    MainMenuMenager mainMenuMenager;

    private void Start()
    {
        gameMenager = FindObjectOfType<GameMenager>();
        mainMenuMenager = FindObjectOfType<MainMenuMenager>();
    }

    public void SetButtonInactive()
    {
        buttonScript.interactable = false;
    }

    public void OnButtonPress()
    {
        InGameEvents.CallUIButtonPress();
        if(gameMenager.IsEnoughEnergyForPlay())
        {
            gameMenager.LoadLvlNumber(lvlIndex);
            mainMenuMenager.StopPlayingMenuMusic();
        }
    }
}