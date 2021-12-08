using EasyMobile;
using System;
using UnityEngine;

namespace GeekBox.Ads
{
    [Serializable]
    class UtilitiesController
    {
        #region Fields



        #endregion

        #region Propeties



        #endregion

        #region Methods

        public void TryShowRatingDialog()
        {
            if (StoreReview.CanRequestRating())
            {
                StoreReview.RequestRating(null, RatingCallback);
            }
            else
            {
                Debug.Log("[EasyMobile-Info] Cant show rating dialog!");
            }
        }

        //private RatingDialogContent GetLocalizedRatingDialog()
        //{
        //    var localized = new RatingDialogContent(
        //    YOUR_LOCALIZED_TITLE + RatingDialogContent.PRODUCT_NAME_PLACEHOLDER,
        //    YOUR_LOCALIZED_MESSAGE + RatingDialogContent.PRODUCT_NAME_PLACEHOLDER + "?",
        //    YOUR_LOCALIZED_LOW_RATING_MESSAGE,
        //    YOUR_LOCALIZED_HIGH_RATING_MESSAGE,
        //    YOUR_LOCALIZED_POSTPONE_BUTTON_LABEL,
        //    YOUR_LOCALIZED_REFUSE_BUTTON_LABEL,
        //    YOUR_LOCALIZED_RATE_BUTTON_LABEL,
        //    YOUR_LOCALIZED_CANCEL_BUTTON_LABEL,
        //    YOUR_LOCALIZED_FEEDBACK_BUTTON_LABEL
        //);
        //}

        private void RatingCallback(StoreReview.UserAction action)
        {
            switch (action)
            {
                case StoreReview.UserAction.Refuse:
                    // Don't ask again. Disable the rating dialog
                    // to prevent it from being shown in the future.
                    StoreReview.DisableRatingRequest();
                    break;
                case StoreReview.UserAction.Postpone:
                    // User selects Not Now/Cancel button.
                    // The dialog automatically closes.
                    break;
                case StoreReview.UserAction.Feedback:
                    // Bad rating, user opts to send feedback email.
                    break;
                case StoreReview.UserAction.Rate:
                    // Good rating, user wants to rate.
                    break;
            }
        }

        #endregion

        #region Enums



        #endregion
    }
}