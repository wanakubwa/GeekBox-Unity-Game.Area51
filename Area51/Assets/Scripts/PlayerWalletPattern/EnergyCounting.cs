using UnityEngine;
using PlayerWalletPattern;
using System;
using EnergyMemento;

public class EnergyCounting : MonoBehaviour
{
    public float EnergyTimeDelay { get; set; }
    public float ActualTimecounter { get; set; }
    private float energyTimeDelayinSeconds;

    GameMenager gameMenager;
    bool isEnable = true;
       
    private void Awake()
    {
        gameMenager = FindObjectOfType<GameMenager>();
    }

    // Update is called once per frame
    private void Update()
    {
        if(isEnable)
        {
            ActualTimecounter += Time.deltaTime;
            if (ActualTimecounter >= energyTimeDelayinSeconds)
                AddEnergyToWallet(1);
        }
    }

    private void AddEnergyToWallet(int value)
    {
        if(gameMenager == null)
            gameMenager = FindObjectOfType<GameMenager>();

        gameMenager.AddEnergy(value);
        ActualTimecounter = 0f;
    }

    public void EnableCounting()
    {
        isEnable = true;
    }
        
    public void DisableCounting()
    {
        ActualTimecounter = 0f;
        isEnable = false;
    }

    public void InitializeCountingValues(int timeDelay)
    {
        EnergyTimeDelay = timeDelay;
        ActualTimecounter = 0f;
        energyTimeDelayinSeconds = EnergyTimeDelay * 60f;
    }

    public float GetActualEnergyTime()
    {
        var delta = energyTimeDelayinSeconds - ActualTimecounter;
        var fullMin = (int)(delta / 60f);
        var fullSec = delta - fullMin * 60f;

        var toRet = 100f * fullMin + fullSec;
        return toRet;
    }

    private void CalculateIdleEnergy(DateTime savedDateTime, float savedTimeCounter, float savedEnergyDelaySec)
    {
        var actualDate = DateTime.Now;
        var dateDeltaInSeconds = (float)(actualDate - savedDateTime).TotalSeconds;

        //TO DO: check out of range
        var fullDeltatime = dateDeltaInSeconds + savedTimeCounter;
        var restTime = fullDeltatime % savedEnergyDelaySec;
        int energyToAdd = (int)((fullDeltatime - restTime) / savedEnergyDelaySec);

        AddEnergyToWallet(energyToAdd);
        ActualTimecounter = restTime;
    }

    public void LoadEnergyCountingTime()
    {
        EnergyTimeMemento energyTimeMemento = new EnergyTimeMemento();
        DateTime savedDateTime = new DateTime();
        float savedTimeCounter = 0f;
        float savedTimeDelay = 0f;
        if(energyTimeMemento.LoadEnergyStatus(ref savedDateTime, ref savedTimeCounter, ref savedTimeDelay))
        {
            CalculateIdleEnergy(savedDateTime, savedTimeCounter, savedTimeDelay);
        }
    }

    public void SaveEnergyCountingTime()
    {
        EnergyTimeMemento energyTimeMemento = new EnergyTimeMemento();
        energyTimeMemento.SaveEnergyStatus(ActualTimecounter, energyTimeDelayinSeconds);
    }
}
