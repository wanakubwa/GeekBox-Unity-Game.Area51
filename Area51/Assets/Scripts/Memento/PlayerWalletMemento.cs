using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using PlayerWalletPattern;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace PlayerWalletMemento
{
    [System.Serializable]
    public class WalletDataSave
    {
        public List<SingleLvl> SavedleLvls { get; set; } = new List<SingleLvl>();
        public int EnergyCounter { get; set; } = 0;

        public WalletDataSave(List<SingleLvl> singleLvls, int energyCounter)
        {
            this.SavedleLvls = singleLvls;
            this.EnergyCounter = energyCounter;
        }
    }

    public class WalletMemento
    {
        private WalletDataSave CreateWalletSave(List<SingleLvl> singleLvls, int energyCounter)
        {
            WalletDataSave walletDataSave = new WalletDataSave(singleLvls, energyCounter);
            return walletDataSave;
        }

        public void SavePlayerWallet(List<SingleLvl> singleLvls, int energyCounter)
        {
            WalletDataSave walletDataSave = CreateWalletSave(singleLvls, energyCounter);

            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + "/playerWalletSave.save");
            bf.Serialize(file, walletDataSave);
            file.Close();

            Debug.Log("saved");
        }

        public bool LoadPlayerWallet(ref List<SingleLvl> savedlvls, ref int energyCounter)
        {
            if(File.Exists(Application.persistentDataPath + "/playerWalletSave.save"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/playerWalletSave.save", FileMode.Open);
                WalletDataSave walletDataSave = (WalletDataSave)bf.Deserialize(file);
                file.Close();

                savedlvls = walletDataSave.SavedleLvls;
                energyCounter = walletDataSave.EnergyCounter;
                Debug.Log("loaded");
                return true;
            }

            return false;
        }
    }
}
