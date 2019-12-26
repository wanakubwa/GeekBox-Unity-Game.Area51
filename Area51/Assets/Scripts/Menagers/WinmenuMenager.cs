using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class WinmenuMenager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI currentTime;
    [SerializeField] TextMeshProUGUI bestTime;
    [SerializeField] TextMeshProUGUI energyTimeRefill;
    [SerializeField] GameObject batteryElementPrefab;
    [SerializeField] Texture emptyBatteryTex;
    [SerializeField] Texture normalBatteryTex;
    [SerializeField] RawImage battery;

    GameMenager gameMenager;
    public int BatteryToActive { set; get; } = 0;
    
    List<GameObject> batteryElements = new List<GameObject>();

    // Start is called before the first frame update
    void Awake()
    {
        gameMenager = FindObjectOfType<GameMenager>();
        InitializeBatteryElementsList();
    }

    private void OnEnable()
    {
        currentTime.text = gameMenager.CurrentTimeEdited.ToString("00:00s");
        bestTime.text = gameMenager.BestTimeForCurrentLvl().ToString("00:00s");
    }

    private void Update()
    {
        if (BatteryToActive != 5)
            energyTimeRefill.text = gameMenager.GetActualEnergyTime().ToString("00:00s");
        else
            energyTimeRefill.text = "FULL!";
    }

    public void InitializeBatteryElementsList()
    {
        for(int i = 0; i<5;i++)
        {
            var tmp = Instantiate(batteryElementPrefab, batteryElementPrefab.transform, true) as GameObject;
            tmp.transform.SetParent(batteryElementPrefab.transform.parent);
            tmp.SetActive(false);
            batteryElements.Add(tmp);
        }
    }

    public void DeactiveAllBatteryElements()
    {
        foreach(var element in batteryElements)
        {
            element.SetActive(false);
        }
    }

    public void SetBatteryStatus(int value)
    {
        if (batteryElements == null)
            InitializeBatteryElementsList();

        for (int i = 0; i< value; i++)
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

    public void MenuButton()
    {
        InGameEvents.CallUIButtonPress();
        InUIEvents.CallMainMenuButtonEvent();
        gameMenager.LoadLvlNumber(0);
    }

    public void AddEnergyButton()
    {
        gameMenager.CreateADPopout();
    }

    public void NextLvlButton()
    {
        InGameEvents.CallUIButtonPress();
        gameMenager.LoadNextLvl();
    }
}
