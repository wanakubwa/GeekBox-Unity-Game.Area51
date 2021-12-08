using UnityEngine;
using System.Collections;
using System;
using EasyMobile;

namespace GeekBox.Ads
{
    [Serializable]
    public class ADController
    {
        #region Fields



        #endregion

        #region Propeties

        private Action OnRewardedADSuccess
        {
            get;
            set;
        } = delegate { };

        private Action OnRewardedADFail
        {
            get;
            set;
        } = delegate { };

        private Action OnInterstitialADClosed
        {
            get;
            set;
        } = delegate { };

        #endregion

        #region Methods

        #region INTERSTITIAL_AD

        public void ShowInterstitialAD(Action onADClosed)
        {
            if (onADClosed == null)
            {
                onADClosed = delegate { };
            }

            OnInterstitialADClosed = onADClosed;

            bool isReady = Advertising.IsInterstitialAdReady();

            if (isReady == true)
            {
                SubscribeInterstitialADEvents();
                Advertising.ShowInterstitialAd();
            }
            else
            {
                Debug.Log("Interstitial AD - Not ready.");
                OnInterstitialADClosed();
            }
        }

        private void SubscribeInterstitialADEvents()
        {
            Advertising.InterstitialAdCompleted += InterstitialAdCompletedHandler;
        }

        private void UnSubscribeInterstitialADEvents()
        {
            Advertising.InterstitialAdCompleted -= InterstitialAdCompletedHandler;
        }

        void InterstitialAdCompletedHandler(InterstitialAdNetwork network, AdLocation location)
        {
            Debug.Log("Interstitial ad has been closed.");
            OnInterstitialADClosed();
            ResetInterstitialADHandlers();
        }

        private void ResetInterstitialADHandlers()
        {
            OnInterstitialADClosed = delegate { };
            UnSubscribeInterstitialADEvents();
        }

        #endregion

        #region REWARDED_AD

        public void ShowRewardedAD(Action onSuccessCallback, Action onFailCallback = null)
        {
            if(onFailCallback == null)
            {
                onFailCallback = delegate { };
            }

            OnRewardedADSuccess = onSuccessCallback;
            OnRewardedADFail = onFailCallback;

            SubscribeRewardedADEvents();
            bool isReady = Advertising.IsRewardedAdReady();
            if(isReady == true)
            {
                Advertising.ShowRewardedAd();
            }
            else
            {
                Debug.Log("Rewarded AD - Not ready.");
                OnRewardedADFail();
                ResetRewardedADHandlers();
            }
        }

        private void SubscribeRewardedADEvents()
        {
            Advertising.RewardedAdCompleted += RewardedAdCompletedHandler;
            Advertising.RewardedAdSkipped += RewardedAdSkippedHandler;
        }

        private void UnSubscribeRewardedADEvents()
        {
            Advertising.RewardedAdCompleted -= RewardedAdCompletedHandler;
            Advertising.RewardedAdSkipped -= RewardedAdSkippedHandler;
        }

        // Event handler called when a rewarded ad has completed
        private void RewardedAdCompletedHandler(RewardedAdNetwork network, AdPlacement location)
        {
            Debug.Log("Rewarded ad has completed.");

            OnRewardedADSuccess();
            ResetRewardedADHandlers();
        }

        // Event handler called when a rewarded ad has been skipped
        private void RewardedAdSkippedHandler(RewardedAdNetwork network, AdPlacement location)
        {
            Debug.Log("Rewarded ad was skipped.");

            OnRewardedADFail();
            ResetRewardedADHandlers();
        }

        private void ResetRewardedADHandlers()
        {
            OnRewardedADFail = delegate { };
            OnRewardedADSuccess = delegate { };
            UnSubscribeRewardedADEvents();
        }

        #endregion

        #endregion

        #region Enums



        #endregion
    }
}
