using UnityEngine;
using UnityEditor;
using PlayerWalletPattern;

//namespace PlayerWalletPattern.Controllers
//{
//    public class WinMenuController
//    {
//        WinmenuMenager winmenuMenager;
//        PlayerWalletAPI playerWalletAPI;

//        public WinMenuController(WinmenuMenager winmenuMenager)
//        {
//            this.winmenuMenager = winmenuMenager;
//            playerWalletAPI = PlayerWalletAPI.Instance;
//            playerWalletAPI.energyChangedEvent += EnergyChangedEvent_Handler;
//        }

//        void EnergyChangedEvent_Handler(int actualEnergy)
//        {
//            winmenuMenager.DeactiveAllBatteryElements();
//            winmenuMenager.BatteryToActive = actualEnergy;

//            if (actualEnergy > 0)
//            {
//                winmenuMenager.SetNormalBatteryTexture();
//                winmenuMenager.SetBatteryStatus(actualEnergy);
//            }
//            else
//                winmenuMenager.SetEmptyBatteryTexture();
//        }
//    }

//    public class DeadMenuController
//    {
//        DeadMenuMenager deadMenuMenager;
//        PlayerWalletAPI playerWalletAPI;

//        public DeadMenuController(DeadMenuMenager deadMenuMenager)
//        {
//            this.deadMenuMenager = deadMenuMenager;
//            playerWalletAPI = PlayerWalletAPI.Instance;
//            playerWalletAPI.energyChangedEvent += EnergyChangedEvent_Handler;
//        }

//        void EnergyChangedEvent_Handler(int actualEnergy)
//        {
//            Debug.Log("tu");
//            deadMenuMenager.DeactiveAllBatteryElements();
//            deadMenuMenager.BatteryToActive = actualEnergy;
//            deadMenuMenager.IsEnoughEnergy = playerWalletAPI.IsEnoughEnergy();

//            if (actualEnergy > 0)
//            {
//                deadMenuMenager.SetNormalBatteryTexture();
//                deadMenuMenager.SetBatteryStatus(actualEnergy);
//            }
//            else
//                deadMenuMenager.SetEmptyBatteryTexture();
//        }
//    }
//}

namespace PlayerWalletPattern
{
    public class WinMenuElement : WalletObserwer
    {
        private WinmenuMenager winmenuMenager;

        public WinMenuElement(WinmenuMenager winmenuMenager)
        {
            this.winmenuMenager = winmenuMenager;
            winmenuMenager.InitializeBatteryElementsList();
        }

        public override void OnEnergyNotify(int actualEnergy)
        {
            winmenuMenager.DeactiveAllBatteryElements();
            winmenuMenager.BatteryToActive = actualEnergy;

            if (actualEnergy > 0)
            {
                winmenuMenager.SetNormalBatteryTexture();
                winmenuMenager.SetBatteryStatus(actualEnergy);
            }
            else
                winmenuMenager.SetEmptyBatteryTexture();
        }
    }

    public class DeadMenuElement : WalletObserwer
    {
        private DeadMenuMenager deadMenuMenager;

        public DeadMenuElement(DeadMenuMenager deadMenuMenager)
        {
            this.deadMenuMenager = deadMenuMenager;
            deadMenuMenager.InitializeBatteryElementsList();
        }

        public override void OnEnergyNotify(int actualEnergy)
        {
            deadMenuMenager.DeactiveAllBatteryElements();
            deadMenuMenager.BatteryToActive = actualEnergy;

            if (actualEnergy > 0)
            {
                deadMenuMenager.SetNormalBatteryTexture();
                deadMenuMenager.SetBatteryStatus(actualEnergy);
            }
            else
                deadMenuMenager.SetEmptyBatteryTexture();
        }
    }

    public class MainMenuElement : WalletObserwer
    {
        private MainMenuMenager mainMenuMenager;

        public MainMenuElement(MainMenuMenager mainMenuMenager)
        {
            this.mainMenuMenager = mainMenuMenager;
            mainMenuMenager.InitializeBatteryElementsList();
        }

        public override void OnEnergyNotify(int actualEnergy)
        {
            mainMenuMenager.DeactiveAllBatteryElements();
            mainMenuMenager.BatteryToActive = actualEnergy;

            if (actualEnergy > 0)
            {
                mainMenuMenager.SetNormalBatteryTexture();
                mainMenuMenager.SetBatteryStatus(actualEnergy);
            }
            else
                mainMenuMenager.SetEmptyBatteryTexture();
        }
    }
}
