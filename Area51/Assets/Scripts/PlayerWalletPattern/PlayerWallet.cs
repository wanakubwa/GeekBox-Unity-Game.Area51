using UnityEngine;
using System.Collections.Generic;
using PlayerWalletMemento;

namespace PlayerWalletPattern
{
    [System.Serializable]
    public class SingleLvl
    {
        int lvlId;
        float bestTime;

        public int LvlId
        {
            get { return lvlId; }
            set { lvlId = value; }
        }
        public float BestTime
        {
            get { return bestTime; }
            set { bestTime = value; }
        }

        public SingleLvl(int lvlId, float bestTime)
        {
            this.LvlId = lvlId;
            this.BestTime = bestTime;
        }
    }

    public class PlayerWallet
    {
        public int EnergyCounter { get; private set; }
        private int maxEnergy = 5;

        // List with obserwers
        List<WalletObserwer> walletObserwers = new List<WalletObserwer>();

        // List with lvl's data
        List<SingleLvl> singleLvls = new List<SingleLvl>();

        // Adding new obserwer to list
        public void AddObserwer(WalletObserwer walletObserwer)
        {
            walletObserwers.Add(walletObserwer);
            walletObserwer.OnEnergyNotify(EnergyCounter);
        }

        public void RemoveObserwer(WalletObserwer walletObserwer)
        {
            var tmp = walletObserwers.Find(x => x == walletObserwer);
            walletObserwers.Remove(tmp);
        }

        // Return unlocked lvls list
        public List<bool> GetUnlockedLvlsList()
        {
            List<bool> unlockedLvlsList = new List<bool>();
            foreach (var element in singleLvls)
            {
                if (element.BestTime != 0f)
                    unlockedLvlsList.Add(true);
                else
                    unlockedLvlsList.Add(false);
            }
            return unlockedLvlsList;
        }

        // Initialize starting params
        public void InitializeStartsValues(List<int> availableLvlsIndex)
        {
            EnergyCounter = maxEnergy;

            // initialize starts lvls list
            InitializeSingleLvlsList(availableLvlsIndex);
        }

        private void InitializeSingleLvlsList(List<int> availableLvls)
        {
            foreach(var element in availableLvls)
            {
                var tmp = new SingleLvl(element, 0f);
                singleLvls.Add(tmp);
            }
        }

        // initialize all obserwers
        public void InitializeAllObserwers()
        {
            EnergyNotify();
        }

        private void EnergyNotify()
        {
            foreach(var element in walletObserwers)
            {
                element.OnEnergyNotify(EnergyCounter);
            }
        }

        public void AddEnergy(int value)
        {
            if (value + EnergyCounter >= maxEnergy)
                EnergyCounter = maxEnergy;
            else
                EnergyCounter += value;

            EnergyNotify();
        }

        // to fix 
        public void SubstractEnergy(int value)
        {
            if(EnergyCounter - value >= 0)
            {
                EnergyCounter -= value;
                EnergyNotify();
            }
            else
            {
                EnergyCounter = 0;
            }
        }

        public bool IsEnoughEnergy()
        {
            if (EnergyCounter == 0)
                return false;
            else
                return true;
        }

        private SingleLvl FindLvlById(int id)
        {
            var tmp = singleLvls.Find(x => x.LvlId == id);
            return tmp;
        }

        // Check is time best for actual lvl
        public void AddNewEndTimeToLvl(int lvlId, float time)
        {
            var currentLvl = FindLvlById(lvlId);
            if(currentLvl != null)
            {
                if (currentLvl.BestTime > time || currentLvl.BestTime == 0f)
                    currentLvl.BestTime = time;
            }
        }

        // Return best time for current lvl
        public float GetBestTimeBylvlId(int lvlId)
        {
            var tmp = FindLvlById(lvlId);
            if (tmp != null)
                return tmp.BestTime;
            else
                return 0f;
        }

        // Load Data from file
        public void LoadPlayerWallet()
        {
            // if save exists load all best times to list 
            WalletMemento walletMemento = new WalletMemento();
            List<SingleLvl> savedLvls = null;
            int savedEnergy = 0;
            if(walletMemento.LoadPlayerWallet(ref savedLvls, ref savedEnergy))
            {
                foreach(var element in savedLvls)
                {
                    var tmp = FindLvlById(element.LvlId);
                    if (tmp != null)
                        tmp.BestTime = element.BestTime;
                }
                EnergyCounter = savedEnergy;
            }
        }

        // Save data to file
        public void SavePlayerWallet()
        {
            WalletMemento walletMemento = new WalletMemento();
            walletMemento.SavePlayerWallet(singleLvls, EnergyCounter);
        }
    }

    public abstract class WalletObserwer
    {
        public virtual void OnEnergyNotify(int EnergyCounter) { }
    }
}