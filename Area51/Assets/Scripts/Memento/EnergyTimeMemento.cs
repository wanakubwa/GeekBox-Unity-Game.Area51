using UnityEngine;
using UnityEditor;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace EnergyMemento
{
    [System.Serializable]
    public class EnergyTimeSave
    {
        public float SavedTimeCounter { get; set; } = 0f;
        public float SavedEnergyTimeDelaySec { get; set; } = 0f;
        public DateTime SavedDateTime { get; set; }

        public EnergyTimeSave(float actualTimeCounter, float energyTimeDelaySec)
        {
            SavedTimeCounter = actualTimeCounter;
            SavedEnergyTimeDelaySec = energyTimeDelaySec;
            SavedDateTime = DateTime.Now;
        }
    }

    public class EnergyTimeMemento
    {
        public void SaveEnergyStatus(float actualTimeCounter, float energyTimeDelaySec)
        {
            EnergyTimeSave energyTimeSave = new EnergyTimeSave(actualTimeCounter, energyTimeDelaySec);

            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + "/energyTimeCounter.save");
            bf.Serialize(file, energyTimeSave);
            file.Close();

            Debug.Log("saved energy");
        }

        public bool LoadEnergyStatus(ref DateTime savedDateTime, ref float savedTimeCounter, ref float savedEnergyDelaySec)
        {
            if (File.Exists(Application.persistentDataPath + "/energyTimeCounter.save"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/energyTimeCounter.save", FileMode.Open);
                EnergyTimeSave energyTimeSave = (EnergyTimeSave)bf.Deserialize(file);
                file.Close();

                savedDateTime = energyTimeSave.SavedDateTime;
                savedTimeCounter = energyTimeSave.SavedTimeCounter;
                savedEnergyDelaySec = energyTimeSave.SavedEnergyTimeDelaySec;

                Debug.Log("loaded energy");
                return true;
            }

            return false;
        }
    }
}
