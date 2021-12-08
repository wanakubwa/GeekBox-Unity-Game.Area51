using UnityEngine;
using System;
using System.Collections.Generic;
using EasyMobile;
using GeekBox.GDPR;

namespace GeekBox.Ads
{ 
    [Serializable]
    public class PrivacyCustomController
    {
		#region Fields

		public const string PLAYER_PREFS_GDPR_KEY = "GDPR_DISPLAYED";

		[Header("Custom GDPR")]
		[SerializeField]
		private ConstentDialogCustomUI gdprConstentPrefab;

		#endregion

		#region Propeties

		public event Action OnGdprDialogShown = delegate { };

		internal ConstentDialogCustomUI GdprConstentPrefab
		{
			get => gdprConstentPrefab;
		}

		internal ConstentDialogCustomUI CurrentGdprConstent
		{
			get;
			private set;
		}

		public bool IsGdprDisplayed
		{
			get { return CurrentGdprConstent != null; }
		}

		#endregion

		#region Methods

		private void CheckEEARegionCallback(EEARegionStatus result)
		{
			if (result == EEARegionStatus.InEEA || result == EEARegionStatus.Unknown)
			{
				if (PlayerPrefs.HasKey(PLAYER_PREFS_GDPR_KEY) == true)
				{
					int gdprStatus = PlayerPrefs.GetInt(PLAYER_PREFS_GDPR_KEY);
					if (gdprStatus == 1)
					{
						OnGdprDialogShown();
						return;
					}
				}

				if (IsGdprDisplayed == false)
				{
					BuildGdprDialog();
				}
			}
			else
			{
				OnGdprDialogShown();
			}
		}

		private void CheckEEARegionCallbackForce(EEARegionStatus result)
		{
			if (result == EEARegionStatus.InEEA || result == EEARegionStatus.Unknown)
			{
				if (IsGdprDisplayed == false)
				{
					BuildGdprDialog();
				}
			}
			else
			{
				OnGdprDialogShown();
			}

			//Debug.Log("Calback recived.".SetColor(Color.cyan));
		}

		public void ShowGdprDialog()
		{
			Privacy.IsInEEARegion(CheckEEARegionCallback);
		}

		public void ForceShowGdprDialog()
		{
			//Debug.Log("Calback SENDED.".SetColor(Color.cyan));
			Privacy.IsInEEARegion(CheckEEARegionCallbackForce);
			//if (IsGdprDisplayed == false)
			//{
			//	BuildGdprDialog();
			//}
		}

		private void BuildGdprDialog()
		{
			if (GdprConstentPrefab == null)
				return;

			CurrentGdprConstent = GameObject.Instantiate(GdprConstentPrefab);

			SubscribeConsentEvents(CurrentGdprConstent);

			// Budowa dialogu tutaj dac wartoci.
			CurrentGdprConstent.Construct("Night", false);
			CurrentGdprConstent.Show();
		}

		private void SubscribeConsentEvents(ConstentDialogCustomUI consentDialogUI)
		{
			if (consentDialogUI != null)
            {
                consentDialogUI.OnCompleteted += OnCompletedHandler;
                consentDialogUI.OnDismissed += OnDismissedHandler;
                consentDialogUI.OnAccept += DefaultDialogAcceptHandler;
                consentDialogUI.OnDecline += DefaultDialogDeclainHandler;
            }
		}

		private void UnSubscribeConsentEvents(ConstentDialogCustomUI consentDialogUI)
		{
			if(consentDialogUI != null)
			{
                consentDialogUI.OnCompleteted -= OnCompletedHandler;
                consentDialogUI.OnDismissed -= OnDismissedHandler;
                consentDialogUI.OnAccept -= DefaultDialogAcceptHandler;
                consentDialogUI.OnDecline -= DefaultDialogDeclainHandler;
            }
		}

		private void DefaultDialogAcceptHandler()
		{
			// Grants global consent.
			PlayerPrefs.SetInt(PLAYER_PREFS_GDPR_KEY, 1);
			GlobalConsentManager.Instance.GrantDataPrivacyConsent();
		}

		private void DefaultDialogDeclainHandler()
		{
			// Revokes global consent.
			PlayerPrefs.SetInt(PLAYER_PREFS_GDPR_KEY, 0);
			GlobalConsentManager.Instance.RevokeDataPrivacyConsent();
			Application.Quit();
		}

		private void OnDismissedHandler()
		{
			DestroyOldDialog();
			DefaultDialogDeclainHandler();
		}

		private void OnCompletedHandler(string buttonId, Dictionary<string, bool> toggleResults)
		{
			OnGdprDialogShown();
			DestroyOldDialog();
		}

		private void DestroyOldDialog()
		{
			if (CurrentGdprConstent != null)
			{
				UnSubscribeConsentEvents(CurrentGdprConstent);
				GameObject.Destroy(CurrentGdprConstent.gameObject);
			}
		}

		#endregion

		#region Enums



		#endregion
	}
}

