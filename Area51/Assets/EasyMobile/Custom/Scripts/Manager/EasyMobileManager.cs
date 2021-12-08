#define EASY_MOBILE_GEEKBOX

using UnityEngine;
using EasyMobile;
using System;

namespace GeekBox.Ads
{
	class EasyMobileManager : MonoBehaviour, IInitializable
	{
        #region Fields

        private static EasyMobileManager instance;

		[Space]
		[SerializeField]
		private PrivacyCustomController customPrivacy = new PrivacyCustomController();
		[SerializeField]
		private LocalizationController localization = new LocalizationController();
		[SerializeField]
		private ADController ads = new ADController();
		[SerializeField]
		private GameServicesController services = new GameServicesController();
		[SerializeField]
		private UtilitiesController utilities = new UtilitiesController();

		#endregion

		#region Propeties

		public event Action OnInitialized = delegate { };

		public static EasyMobileManager Instance
		{
			get
			{
				if (instance == null)
				{
					instance = (EasyMobileManager)FindObjectOfType(typeof(EasyMobileManager));
				}
				return instance;
			}
			private set
			{
				instance = value;
			}
		}

		public PrivacyCustomController CustomPrivacy { 
			get => customPrivacy; 
			private set => customPrivacy = value; 
		}

		internal LocalizationController Localization { 
			get => localization; 
			private set => localization = value; 
		}

		public ADController ADS { 
			get => ads; 
			private set => ads = value; 
		}

		public GameServicesController Services {
			get => services;
			private set => services = value; 
		}

		internal UtilitiesController Utilities { 
			get => utilities; 
			private set => utilities = value; 
		}

		#endregion

		#region Methods

		public void ShowRatingDialog()
		{
			Utilities.TryShowRatingDialog();
		}

		public void RevealAchievementByName(string achievementName)
		{
			Services.RevealAchievement(achievementName);
		}

		public void UnlockAchievementByName(string achievementName)
		{
			Services.UnlockAchievement(achievementName);
		}

		public void ShowAchievementsUI()
		{
			Services.ShowAchievementsUI();
		}

		public void ReportScoreToLeaderboard(float value, string boardName)
		{
			Services.ReportScoreForSpecificLeaderboard(value, boardName);
		}

		public void ShowLeaderboard(string boardName = null)
		{
			if(boardName == null || boardName.Length < 1)
			{
				Services.ShowLederboard();
			}
			else
			{
				Services.ShowSpecificLeaderBoard(boardName);
			}
		}

		public void ShowInterstitialAD(Action onADClosed = null)
		{
			ADS.ShowInterstitialAD(onADClosed);
		}

		public void ShowRewardedAD(Action onSuccessCallback, Action onFailCallback = null)
		{
			ADS.ShowRewardedAD(onSuccessCallback, onFailCallback);
		}

		public string GetLocalizedText(string key)
		{
			return Localization.GetText(key);
		}

		public void ShowGdprDialog(bool isForced = false)
		{
			if(isForced == false)
			{
                CustomPrivacy.ShowGdprDialog();
            }
			else
			{
				CustomPrivacy.ForceShowGdprDialog();
			}
		}

		private void OnEnable()
		{
			Localization.Initialize();
		}

		private void OnDisable()
		{
			Services.DetachEvents();
		}

		public void Initialize()
		{
			InitializeEasyMobile();
		}

		private void InitializeEasyMobile()
		{
			Localization.Initialize();

			// Checks if EM has been initialized and initialize it if not.
			// This must be done once before other EM APIs can be used.
			CustomPrivacy.OnGdprDialogShown += InitializeRuntimeManager;
			ShowGdprDialog();
		}

		// Inicjalizacja managerow dostepnych ze strony easymobile. Po uzyskaniu zgody usera.
		private void InitializeRuntimeManager()
		{
			CustomPrivacy.OnGdprDialogShown -= InitializeRuntimeManager;
			if (RuntimeManager.IsInitialized() == false)
			{
				RuntimeManager.Init();
			}

			Services.AttachEvents();
			Services.Initialize();

			OnInitialized();
		}

		#endregion

		#region Enums



		#endregion
	}
}

