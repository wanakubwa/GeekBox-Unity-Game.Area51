using PlayerWalletPattern;
using System.Collections.Generic;

//namespace PlayerWalletPattern.API
//{
//    public class PlayerWalletAPI
//    {
//        static PlayerWalletAPI instance = null;
//        static PlayerWallet playerWallet = null;
//        static readonly object oPadLock = new object();

//        public delegate void EnergyChanged(int value);
//        public event EnergyChanged energyChangedEvent;

//        public static PlayerWalletAPI Instance
//        {
//            get
//            {
//                lock(oPadLock)
//                {
//                    if (instance == null)
//                    {
//                        instance = new PlayerWalletAPI();
//                        playerWallet = new PlayerWallet();
//                    }
//                    return instance;
//                }
//            }
//        }

//        public void InitializePlayerWallet(ref List<int> availableLvlsIndex)
//        {
//            playerWallet.InitializeStartsValues(availableLvlsIndex);
//        }

//        public void InvokeAllEvents()
//        {
//            EnergyChangedEvent_Invoke(playerWallet.EnergyCounter);
//        }

//        public void AddEnergyToWallet(int value)
//        {
//            playerWallet.AddEnergy(value);
//            EnergyChangedEvent_Invoke(playerWallet.EnergyCounter);
//        }

//        public void SubstractEnergyFromWallet(int value)
//        {
//            playerWallet.SubstractEnergy(value);
//            EnergyChangedEvent_Invoke(playerWallet.EnergyCounter);
//        }

//        // Actual energy after changes - Invoke
//        private void EnergyChangedEvent_Invoke(int value)
//        {
//            if (energyChangedEvent != null)
//                energyChangedEvent.Invoke(value);
//        }

//        // Is enough energy for repeat lvl
//        public bool IsEnoughEnergy()
//        {
//            if (playerWallet.EnergyCounter == 0)
//                return false;
//            else
//                return true;
//        }

//        public void AddNewEndTimeToLvlIndex(int lvlIndex, float time)
//        {
//            playerWallet.AddNewEndTimeToLvl(lvlIndex, time);
//        }

//        public float BestTimeForCurrentLvl(int lvlId)
//        {
//            var tmp = playerWallet.GetBestTimeBylvlId(lvlId);
//            return tmp;
//        }
//    }
//}