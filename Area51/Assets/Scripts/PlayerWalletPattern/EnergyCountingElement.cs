using UnityEngine;
using UnityEditor;
using PlayerWalletPattern;

public class EnergyCountingElement : WalletObserwer
{
    EnergyCounting energyCounting;

    public EnergyCountingElement(EnergyCounting energyCounting)
    {
        this.energyCounting = energyCounting;
    }

    public override void OnEnergyNotify(int energyCounter)
    {
        Debug.Log(energyCounter);
        if (energyCounter == 5)
            energyCounting.DisableCounting();
        else
            energyCounting.EnableCounting();
    }
}

//namespace PlayerWalletPattern.Controllers
//{
//    class EnergyCountingController
//    {
//        EnergyCounting energyCounting;
//        PlayerWalletAPI playerWalletAPI;

//        public EnergyCountingController(EnergyCounting energyCounting)
//        {
//            this.energyCounting = energyCounting;
//            playerWalletAPI = PlayerWalletAPI.Instance;
//            playerWalletAPI.energyChangedEvent += EnergyChangedEvent_Handler;
//        }

//        void EnergyChangedEvent_Handler(int EnergyCounter)
//        {
//            if (EnergyCounter == 5)
//                energyCounting.DisableCounting();
//            else
//                energyCounting.EnableCounting();
//        }
//    }
//}