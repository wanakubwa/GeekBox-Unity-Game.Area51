using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using PlayerWalletPattern;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

public class MainMenuMenager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI energyTimeRefill;
    [SerializeField] GameObject batteryElementPrefab;
    [SerializeField] Texture emptyBatteryTex;
    [SerializeField] Texture normalBatteryTex;
    [SerializeField] RawImage battery;
    [SerializeField] GameObject audioObject;
    [SerializeField] List<GameObject> listButtons;

    GameMenager gameMenager;
    MainMenuElement mainMenuElement;
    public int BatteryToActive { set; get; } = 0;

    List<GameObject> batteryElements = new List<GameObject>();

    // Start is called before the first frame update
    void Awake()
    {
        gameMenager = FindObjectOfType<GameMenager>();
        InitializeBatteryElementsList();
        mainMenuElement = new MainMenuElement(GetComponent<MainMenuMenager>());
        audioObject.SetActive(true);
    }

    private void Start()
    {
        gameMenager.AddObserwer(mainMenuElement);
        StartCoroutine(CheckSceneFullyLoaded());
        FindLvlTimeText();
        LockButtonsList();
    }

    private void FindLvlTimeText()
    {
        var objTmp = GameObject.FindGameObjectWithTag("LvlTime");
        objTmp.GetComponent<TextMeshProUGUI>().text = "";
    }

    private IEnumerator CheckSceneFullyLoaded()
    {
        while (!SceneManager.GetActiveScene().isLoaded)
        {
            yield return null;
        }

        gameMenager.LvlLoadedSuccess();
    }

    private void OnDestroy()
    {
        gameMenager.RemoveObserwer(mainMenuElement);
    }

    private void Update()
    {
        if (BatteryToActive != 5)
            energyTimeRefill.text = gameMenager.GetActualEnergyTime().ToString("00:00s");
        else
            energyTimeRefill.text = "FULL!";
    }

    public void StopPlayingMenuMusic()
    {
        audioObject.SetActive(false);
    }

    public void LockButtonsList()
    {
        var unlockedLvls = gameMenager.GetUnlockedLvlsList();
        var isUnlocked = true;
        var counter = 0;

        foreach(var element in listButtons)
        {
            if(!isUnlocked)
            {
                element.GetComponent<MainMenuButton>().SetButtonInactive();
            }
            if (!unlockedLvls[counter])
                isUnlocked = false;
            counter++;
        }
    }

    public void InitializeBatteryElementsList()
    {
        for (int i = 0; i < 5; i++)
        {
            var tmp = Instantiate(batteryElementPrefab, batteryElementPrefab.transform, true) as GameObject;
            tmp.transform.SetParent(batteryElementPrefab.transform.parent);
            tmp.SetActive(false);
            batteryElements.Add(tmp);
        }
    }

    public void DeactiveAllBatteryElements()
    {
        foreach (var element in batteryElements)
        {
            element.SetActive(false);
        }
    }

    public void SetBatteryStatus(int value)
    {
        if (batteryElements == null)
            InitializeBatteryElementsList();

        for (int i = 0; i < value; i++)
        {
            batteryElements[i].SetActive(true);
        }
    }

    public void SetEmptyBatteryTexture()
    {
        battery.texture = emptyBatteryTex;
    }

    public void SetNormalBatteryTexture()
    {
        battery.texture = normalBatteryTex;
    }

    public void LeaderBoardButton()
    {
        InGameEvents.CallUIButtonPress();
    }


}
