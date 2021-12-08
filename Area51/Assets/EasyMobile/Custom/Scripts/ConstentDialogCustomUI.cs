using EasyMobile.Internal.Privacy;
using GeekBox.Ads;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GeekBox.GDPR
{
    class ConstentDialogCustomUI : EditorConsentDialogUI
    {
        #region Fields

        [Space]
        [SerializeField]
        private TextMeshProUGUI formNameText;
        [SerializeField]
        private TextMeshProUGUI titleCustomText;
        [SerializeField]
        private TextMeshProUGUI descriptionText;
        [SerializeField]
        private Toggle acceptToggle;
        [SerializeField]
        private Button acceptButton;
        [SerializeField]
        private TextMeshProUGUI buttonText;
        [SerializeField]
        private TextMeshProUGUI toggleText;
        [SerializeField]
        private TextMeshProUGUI hintText;

        [Header("Localization")]
        [SerializeField]
        private string formNameLoc;
        [SerializeField]
        private string titleLoc;
        [SerializeField]
        private string descriptionLoc;
        [SerializeField]
        private string toggleLoc;
        [SerializeField]
        private string hintLoc;

        #endregion

        #region Propeties

        public TextMeshProUGUI TitleCustomText { 
            get => titleCustomText;
        }

        public TextMeshProUGUI DescriptionText { 
            get => descriptionText; 
        }

        public Toggle AcceptToggle { get => acceptToggle; }
        public Button AcceptButton { get => acceptButton; }
        public TextMeshProUGUI ButtonText { get => buttonText; }
        public TextMeshProUGUI FormNameText { get => formNameText; }
        public TextMeshProUGUI ToggleText { get => toggleText; }
        public TextMeshProUGUI HintText { get => hintText; }

        public string FormNameLoc { get => formNameLoc;  }
        public string TitleLoc { get => titleLoc; }
        public string DescriptionLoc { get => descriptionLoc; }
        public string ToggleLoc { get => toggleLoc; }
        public string HintLoc { get => hintLoc; }

        #endregion

        #region Methods

        public void SetAcceptStatus(bool isAccept)
        {
            //todo;
        }

        public void AcceptButtonClick()
        {
            if(AcceptToggle.isOn == true)
            {
                NotifyOnAccept();
                NotifyCompleteDefault();
            }
            else
            {
                NotifyOnDecline();
            }
        }

        public override EditorConsentDialogUI Construct(string title, bool isDimissible)
        {
            AcceptToggle.SetIsOnWithoutNotify(false);
            SetFormText(title);
            AcceptButton.interactable = true;

            IsConstructed = true;
            IsDismissible = isDimissible;

            RefreshFormContent();
            return this;
        }

        public void RefreshFormContent()
        {
            EasyMobileManager easyMobileManager = EasyMobileManager.Instance;
            if(easyMobileManager == null)
            {
                return;
            }

            SetDescriptionText(easyMobileManager.GetLocalizedText(DescriptionLoc));
            SetFormText(easyMobileManager.GetLocalizedText(FormNameLoc));
            SetTitleText(easyMobileManager.GetLocalizedText(TitleLoc));
            SetHintText(easyMobileManager.GetLocalizedText(HintLoc));
            SetToggleText(easyMobileManager.GetLocalizedText(ToggleLoc));
        }

        private void SetDescriptionText(string text)
        {
            if(text == string.Empty)
            {
                return;
            }

            DescriptionText.text = text;
        }

        private void SetTitleText(string text)
        {
            if (text == string.Empty)
            {
                return;
            }

            TitleCustomText.text = text;
        }

        private void SetFormText(string text)
        {
            if (text == string.Empty)
            {
                return;
            }

            FormNameText.text = text;
        }

        private void SetHintText(string text)
        {
            if (text == string.Empty)
            {
                return;
            }

            HintText.text = text;
        }

        private void SetToggleText(string text)
        {
            if (text == string.Empty)
            {
                return;
            }

            ToggleText.text = text;
        }

        #endregion

        #region Enums



        #endregion
    }
}

