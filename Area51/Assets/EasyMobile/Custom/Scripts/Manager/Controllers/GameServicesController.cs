using EasyMobile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
public class GameServicesController
{
    #region Fields



    #endregion

    #region Propeties



    #endregion

    #region Methods

    #region COMMON

    public void AttachEvents()
    {
        GameServices.UserLoginSucceeded += OnUserLoginSucceeded;
        GameServices.UserLoginFailed += OnUserLoginFailed;
    }

    public void DetachEvents()
    {
        GameServices.UserLoginSucceeded -= OnUserLoginSucceeded;
        GameServices.UserLoginFailed -= OnUserLoginFailed;
    }

    public void Initialize()
    {
        GameServices.ManagedInit();
    }

    private void OnUserLoginSucceeded()
    {
        Debug.Log("User logged in successfully.");
    }
    private void OnUserLoginFailed()
    {
        Debug.Log("User login failed.");
    }

    #endregion
    #region LEADERBOARD

    public void ShowLederboard()
    {
        // Check for initialization before showing leaderboard UI
        if (GameServices.IsInitialized() == true)
        {
            GameServices.ShowLeaderboardUI();
        }
        else
        {
#if UNITY_ANDROID && !UNITY_EDITOR
GameServices.Init(); // start a new initialization process
#elif UNITY_IOS && !UNITY_EDITOR
Debug.Log("Cannot show leaderboard UI: The user is not logged in to Game Center.");
#else
            Debug.Log("[SERVICES] - Not supported platform");
#endif
        }
    }

    public void ShowSpecificLeaderBoard(string name)
    {
        // Check for initialization before showing leaderboard UI
        if (GameServices.IsInitialized() == true)
        {
            GameServices.ShowLeaderboardUI(name);
        }
        else
        {
#if UNITY_ANDROID
GameServices.Init(); // start a new initialization process
#elif UNITY_IOS
Debug.Log("Cannot show leaderboard UI: The user is not logged in to Game Center.");
#else
            Debug.Log("[SERVICES] - Not supported platform");
#endif
        }
    }

    public void ReportScoreForSpecificLeaderboard(float value, string name)
    {
        // Check for initialization before showing leaderboard UI
        if (GameServices.IsInitialized() == true)
        {
            GameServices.ReportScore((long)value, name);
        }
    }

    #endregion

    #region ACHIEVEMENTS

    public void ShowAchievementsUI()
    {
        if (GameServices.IsInitialized())
        {
            GameServices.ShowAchievementsUI();
        }
        else
        {
#if UNITY_ANDROID && !UNITY_EDITOR
GameServices.Init(); // start a new initialization process
#elif UNITY_IOS
Debug.Log("Cannot show achievements UI: The user is not logged in to Game Center.");
#else
            Debug.Log("[SERVICES] - Not supported platform");
#endif
        }
    }

    public void UnlockAchievement(string achievementName)
    {
        GameServices.UnlockAchievement(achievementName);
    }

    public void RevealAchievement(string achievementName)
    {
        GameServices.RevealAchievement(achievementName);
    }

    #endregion

    #endregion

    #region Enums



    #endregion
}
