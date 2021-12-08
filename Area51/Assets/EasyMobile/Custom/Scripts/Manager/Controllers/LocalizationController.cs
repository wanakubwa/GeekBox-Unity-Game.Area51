using EasyMobile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GeekBox.Ads
{
    [Serializable]
    class LocalizationController
    {
        #region Fields

        [SerializeField]
        private TextAsset adsLocAsset;
        [SerializeField]
        private char linesDivider = '\n';
        [SerializeField]
        private char fieldsDivider = ';';

        [Header("Link settings")]
        [SerializeField]
        private string privacyPolicyTag = "$POLICY_URL";
        [SerializeField]
        private string privacyPolicyUrl = "https://luckdoors.flycricket.io/privacy.html";


        #endregion

        #region Propeties

        public TextAsset AdsLocAsset { get => adsLocAsset; }
        public char LinesDivider { get => linesDivider; set => linesDivider = value; }
        public char FieldsDivider { get => fieldsDivider; set => fieldsDivider = value; }

        public Dictionary<string, string> LocalizedValues
        {
            get;
            private set;
        } = new Dictionary<string, string>();

        public string PrivacyPolicyTag { get => privacyPolicyTag; }
        public string PrivacyPolicyUrl { get => privacyPolicyUrl; }

        #endregion

        #region Methods

        public string GetText(string key)
        {
            key = key.Trim();
            string output = key;

            if(LocalizedValues.Count < 1)
            {
                return output;
            }

            foreach(var dictKey in LocalizedValues)
            {
                if(dictKey.Key.Equals(key) == true)
                {
                    output = dictKey.Value;
                }
            }

            output = output.Replace(PrivacyPolicyTag, PrivacyPolicyUrl);
            return output;
        }

        public void Initialize()
        {
            string assetContent = AdsLocAsset.text;
            string[] rows = assetContent.Split(LinesDivider);
            if(rows == null || rows.Length < 1)
            {
                Debug.Log("ADS - Brak danych w pliku do wczytania!");
            }

            LocalizedValues.Clear();
            int locIndex = GetLocIndex();
            for(int i =1; i < rows.Length; i++)
            {
                string[] lineFields = rows[i].Split(FieldsDivider);
                LocalizedValues.Add(lineFields[0].Trim(), lineFields[locIndex].Trim());
            }
        }

        private int GetLocIndex()
        {
            // Podstawowo angielski.
            int index = 2;
            EEACountries currentCode = Privacy.GetCurrentCountryCode();

            switch(currentCode)
            {
                case EEACountries.GB:
                case EEACountries.UK:
                case EEACountries.EL:
                    index = 2;
                    break;
                case EEACountries.FR:
                    index = 3;
                    break;
                case EEACountries.DE:
                    index = 4;
                    break;
                case EEACountries.PL:
                    index = 1;
                    break;
                default:
                    index = 2;
                    break;
            }

            return index;
        }

        #endregion

        #region Enums



        #endregion
    }
}
