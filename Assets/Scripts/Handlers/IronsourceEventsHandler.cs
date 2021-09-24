using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronsourceEventsHandler : MonoBehaviour
{
    #region Interstitial Event variables

    public static Action onInterstitialAdReadyEvent;
    public static Action<IronSourceError> onInterstitialAdLoadFailedEvent;
    public static Action onInterstitialAdShowSucceededEvent;
    public static Action<IronSourceError> onInterstitialAdShowFailedEvent;
    public static Action onInterstitialAdClickedEvent;
    public static Action onInterstitialAdOpenedEvent;
    public static Action onInterstitialAdClosedEvent;

    #endregion

    #region RewardedVideo Event variables
    public static Action onRewardedVideoAdOpenedEvent;
    public static Action<IronSourcePlacement> onRewardedVideoAdClickedEvent;
    public static Action onRewardedVideoAdClosedEvent;
    public static Action<bool> onRewardedVideoAvailabilityChangedEvent;
    public static Action onRewardedVideoAdStartedEvent;
    public static Action onRewardedVideoAdEndedEvent;
    public static Action<IronSourcePlacement> onRewardedVideoAdRewardedEvent;
    public static Action<IronSourceError> onRewardedVideoAdShowFailedEvent;
    #endregion

    #region Offerwall Event variables
    public static Action onOfferwallClosedEvent;
    public static Action onOfferwallOpenedEvent;
    public static Action<IronSourceError> onOfferwallShowFailedEvent;
    public static Action<Dictionary<string, object>> onOfferwallAdCreditedEvent;
    public static Action<IronSourceError> onGetOfferwallCreditsFailedEvent;
    public static Action<bool> onOfferwallAvailableEvent;
    #endregion

    #region banner Event variables
    public static Action onBannerAdLoadedEvent;
    public static Action<IronSourceError> onBannerAdLoadFailedEvent;
    public static Action onBannerAdClickedEvent;
    public static Action onBannerAdScreenPresentedEvent;
    public static Action onBannerAdScreenDismissedEvent;
    public static Action onBannerAdLeftApplicationEvent;
    #endregion

    private void Awake()
    {
//        // INTERSTITIAL EVENTS
//        IronSourceEvents.onInterstitialAdReadyEvent += InterstitialAdReadyEvent;
//        IronSourceEvents.onInterstitialAdLoadFailedEvent += InterstitialAdLoadFailedEvent;
//        IronSourceEvents.onInterstitialAdShowSucceededEvent += InterstitialAdShowSucceededEvent;
//        IronSourceEvents.onInterstitialAdShowFailedEvent += InterstitialAdShowFailedEvent;
//        IronSourceEvents.onInterstitialAdClickedEvent += InterstitialAdClickedEvent;
//        IronSourceEvents.onInterstitialAdOpenedEvent += InterstitialAdOpenedEvent;
//        IronSourceEvents.onInterstitialAdClosedEvent += InterstitialAdClosedEvent;
//
//        // REWARDED VIDEO EVENTS 
//        IronSourceEvents.onRewardedVideoAdOpenedEvent += RewardedVideoAdOpenedEvent;
//        IronSourceEvents.onRewardedVideoAdClickedEvent += RewardedVideoAdClickedEvent;
//        IronSourceEvents.onRewardedVideoAdClosedEvent += RewardedVideoAdClosedEvent;
//        IronSourceEvents.onRewardedVideoAvailabilityChangedEvent += RewardedVideoAvailabilityChangedEvent;
//        IronSourceEvents.onRewardedVideoAdStartedEvent += RewardedVideoAdStartedEvent;
//        IronSourceEvents.onRewardedVideoAdEndedEvent += RewardedVideoAdEndedEvent;
//        IronSourceEvents.onRewardedVideoAdRewardedEvent += RewardedVideoAdRewardedEvent;
//        IronSourceEvents.onRewardedVideoAdShowFailedEvent += RewardedVideoAdShowFailedEvent;
//
//        // OFFERWALL EVENTS
//        IronSourceEvents.onOfferwallClosedEvent += OfferwallClosedEvent;
//        IronSourceEvents.onOfferwallOpenedEvent += OfferwallOpenedEvent;
//        IronSourceEvents.onOfferwallShowFailedEvent += OfferwallShowFailedEvent;
//        IronSourceEvents.onOfferwallAdCreditedEvent += OfferwallAdCreditedEvent;
//        IronSourceEvents.onGetOfferwallCreditsFailedEvent += GetOfferwallCreditsFailedEvent;
//        IronSourceEvents.onOfferwallAvailableEvent += OfferwallAvailableEvent;
//
//        //BANNER EVENTS
//        IronSourceEvents.onBannerAdLoadedEvent += BannerAdLoadedEvent;
//        IronSourceEvents.onBannerAdLoadFailedEvent += BannerAdLoadFailedEvent;
//        IronSourceEvents.onBannerAdClickedEvent += BannerAdClickedEvent;
//        IronSourceEvents.onBannerAdScreenPresentedEvent += BannerAdScreenPresentedEvent;
//        IronSourceEvents.onBannerAdScreenDismissedEvent += BannerAdScreenDismissedEvent;
//        IronSourceEvents.onBannerAdLeftApplicationEvent += BannerAdLeftApplicationEvent;
    }

    private void OnEnable()
    {
        // INTERSTITIAL EVENTS
        IronSourceEvents.onInterstitialAdReadyEvent += InterstitialAdReadyEvent;
        IronSourceEvents.onInterstitialAdLoadFailedEvent += InterstitialAdLoadFailedEvent;
        IronSourceEvents.onInterstitialAdShowSucceededEvent += InterstitialAdShowSucceededEvent;
        IronSourceEvents.onInterstitialAdShowFailedEvent += InterstitialAdShowFailedEvent;
        IronSourceEvents.onInterstitialAdClickedEvent += InterstitialAdClickedEvent;
        IronSourceEvents.onInterstitialAdOpenedEvent += InterstitialAdOpenedEvent;
        IronSourceEvents.onInterstitialAdClosedEvent += InterstitialAdClosedEvent;

        // REWARDED VIDEO EVENTS 
        IronSourceEvents.onRewardedVideoAdOpenedEvent += RewardedVideoAdOpenedEvent;
        IronSourceEvents.onRewardedVideoAdClickedEvent += RewardedVideoAdClickedEvent;
        IronSourceEvents.onRewardedVideoAdClosedEvent += RewardedVideoAdClosedEvent;
        IronSourceEvents.onRewardedVideoAvailabilityChangedEvent += RewardedVideoAvailabilityChangedEvent;
        IronSourceEvents.onRewardedVideoAdStartedEvent += RewardedVideoAdStartedEvent;
        IronSourceEvents.onRewardedVideoAdEndedEvent += RewardedVideoAdEndedEvent;
        IronSourceEvents.onRewardedVideoAdRewardedEvent += RewardedVideoAdRewardedEvent;
        IronSourceEvents.onRewardedVideoAdShowFailedEvent += RewardedVideoAdShowFailedEvent;

        // OFFERWALL EVENTS
        IronSourceEvents.onOfferwallClosedEvent += OfferwallClosedEvent;
        IronSourceEvents.onOfferwallOpenedEvent += OfferwallOpenedEvent;
        IronSourceEvents.onOfferwallShowFailedEvent += OfferwallShowFailedEvent;
        IronSourceEvents.onOfferwallAdCreditedEvent += OfferwallAdCreditedEvent;
        IronSourceEvents.onGetOfferwallCreditsFailedEvent += GetOfferwallCreditsFailedEvent;
        IronSourceEvents.onOfferwallAvailableEvent += OfferwallAvailableEvent;

        //BANNER EVENTS
        IronSourceEvents.onBannerAdLoadedEvent += BannerAdLoadedEvent;
        IronSourceEvents.onBannerAdLoadFailedEvent += BannerAdLoadFailedEvent;
        IronSourceEvents.onBannerAdClickedEvent += BannerAdClickedEvent;
        IronSourceEvents.onBannerAdScreenPresentedEvent += BannerAdScreenPresentedEvent;
        IronSourceEvents.onBannerAdScreenDismissedEvent += BannerAdScreenDismissedEvent;
        IronSourceEvents.onBannerAdLeftApplicationEvent += BannerAdLeftApplicationEvent;
    }

    #region Interstitial Methods
    // Invoked when the initialization process has failed.
    // @param description - string - contains information about the failure.
    public void InterstitialAdLoadFailedEvent(IronSourceError error)
    {
        onInterstitialAdLoadFailedEvent?.Invoke(error);

        if (IronsourceManager.Instance.DoLogEventsToGA())
            GAAdsAnalyticsHandler.instance.GA_NewAdEvent(GameAnalyticsSDK.GAAdAction.FailedShow, GameAnalyticsSDK.GAAdType.Interstitial, "Ironsource", IronSourceAdUnits.INTERSTITIAL);
    }
    // Invoked when the ad fails to show.
    // @param description - string - contains information about the failure.
    public void InterstitialAdShowFailedEvent(IronSourceError error)
    {
        onInterstitialAdShowFailedEvent?.Invoke(error);// replace interstit

        if (IronsourceManager.Instance.DoLogEventsToGA())
            GAAdsAnalyticsHandler.instance.GA_LogInterstatialAdFailed("Ironsource", IronSourceAdUnits.INTERSTITIAL);
    }
    // Invoked when end user clicked on the interstitial ad
    public void InterstitialAdClickedEvent()
    {
        onInterstitialAdClickedEvent?.Invoke();

        if (IronsourceManager.Instance.DoLogEventsToGA())
            GAAdsAnalyticsHandler.instance.GA_HandleInterstitialLeftApplication("Ironsource", IronSourceAdUnits.INTERSTITIAL);
    }
    // Invoked when the interstitial ad closed and the user goes back to the application screen.
    public void InterstitialAdClosedEvent()
    {
        onInterstitialAdClosedEvent?.Invoke();
    }
    // Invoked when the Interstitial is Ready to shown after load function is called
    public void InterstitialAdReadyEvent()
    {
        onInterstitialAdReadyEvent?.Invoke();
    }
    // Invoked when the Interstitial Ad Unit has opened
    public void InterstitialAdOpenedEvent()
    {
        Debug.Log("Show interstitial in IronSourceEventsHandler");
        onInterstitialAdOpenedEvent?.Invoke();

        if (IronsourceManager.Instance.DoLogEventsToGA())
            GAAdsAnalyticsHandler.instance.GA_HandleInterstitialOpened("Ironsource", IronSourceAdUnits.INTERSTITIAL);
    }
    // Invoked right before the Interstitial screen is about to open.
    // NOTE - This event is available only for some of the networks. 
    // You should treat this event as an interstitial impression, but rather use InterstitialAdOpenedEvent
    public void InterstitialAdShowSucceededEvent()
    {
        onInterstitialAdShowSucceededEvent?.Invoke();
    }

    #endregion

    #region RewardedVideo Methods
    //Invoked when the RewardedVideo ad view has opened.
    //Your Activity will lose focus. Please avoid performing heavy 
    //tasks till the video ad will be closed.
    public void RewardedVideoAdOpenedEvent()
    {
        onRewardedVideoAdOpenedEvent?.Invoke();
        GAAdsAnalyticsHandler.instance.GA_HandleRewardedAdOpening();
    }
    //Invoked when the RewardedVideo ad view is about to be closed.
    //Your activity will now regain its focus.
    public void RewardedVideoAdClosedEvent()
    {
        onRewardedVideoAdClosedEvent?.Invoke();

        if (IronsourceManager.Instance.DoLogEventsToGA())
            GAAdsAnalyticsHandler.instance.GA_HandleRewardedAdClosed("Ironsource", IronSourceAdUnits.REWARDED_VIDEO);
    }
    //Invoked when there is a change in the ad availability status.
    //@param - available - value will change to true when rewarded videos are available. 
    //You can then show the video by calling showRewardedVideo().
    //Value will change to false when no videos are available.
    public void RewardedVideoAvailabilityChangedEvent(bool available)
    {
        //Change the in-app 'Traffic Driver' state according to availability.
        bool rewardedVideoAvailability = available;
        onRewardedVideoAvailabilityChangedEvent?.Invoke(available);
    }

    //Invoked when the user completed the video and should be rewarded. 
    //If using server-to-server callbacks you may ignore this events and wait for 
    // the callback from the  ironSource server.
    //@param - placement - placement object which contains the reward data
    public void RewardedVideoAdRewardedEvent(IronSourcePlacement placement)
    {
        onRewardedVideoAdRewardedEvent?.Invoke(placement);

        if (IronsourceManager.Instance.DoLogEventsToGA())
            GAAdsAnalyticsHandler.instance.GA_HandleUserEarnedReward("Ironsource", IronSourceAdUnits.REWARDED_VIDEO);
    }
    //Invoked when the Rewarded Video failed to show
    //@param description - string - contains information about the failure.
    public void RewardedVideoAdShowFailedEvent(IronSourceError error)
    {
        onRewardedVideoAdShowFailedEvent?.Invoke(error);

        if (IronsourceManager.Instance.DoLogEventsToGA())
            GAAdsAnalyticsHandler.instance.GA_NewAdEvent(GameAnalyticsSDK.GAAdAction.FailedShow, GameAnalyticsSDK.GAAdType.RewardedVideo, "Ironsource", IronSourceAdUnits.REWARDED_VIDEO);
    }

    // ----------------------------------------------------------------------------------------
    // Note: the events below are not available for all supported rewarded video ad networks. 
    // Check which events are available per ad network you choose to include in your build. 
    // We recommend only using events which register to ALL ad networks you include in your build. 
    // ----------------------------------------------------------------------------------------

    //Invoked when the video ad starts playing. 
    public void RewardedVideoAdStartedEvent()
    {
        onRewardedVideoAdStartedEvent?.Invoke();

        if (IronsourceManager.Instance.DoLogEventsToGA())
            GAAdsAnalyticsHandler.instance.GA_HandleRewardedAdOpening();
    }
    //Invoked when the video ad finishes playing. 
    public void RewardedVideoAdEndedEvent()
    {
        onRewardedVideoAdEndedEvent?.Invoke();

        if (IronsourceManager.Instance.DoLogEventsToGA())
            GAAdsAnalyticsHandler.instance.GA_HandleRewardedAdClosed("Ironsource", IronSourceAdUnits.REWARDED_VIDEO);
    }
    //Invoked when the video ad is clicked. 
    public void RewardedVideoAdClickedEvent(IronSourcePlacement placement)
    {
        onRewardedVideoAdClickedEvent?.Invoke(placement);
    }
    #endregion

    #region Offerwall Methods

    /**
* Invoked when there is a change in the Offerwall availability status.
* @param - available - value will change to YES when Offerwall are available. 
* You can then show the video by calling showOfferwall(). Value will change to NO when Offerwall isn't available.
*/
    public void OfferwallAvailableEvent(bool canShowOfferwall)
    {
        onOfferwallAvailableEvent?.Invoke(canShowOfferwall);
    }
    /**
     * Invoked when the Offerwall successfully loads for the user.
     */
    public void OfferwallOpenedEvent()
    {
        onOfferwallOpenedEvent?.Invoke();

        if (IronsourceManager.Instance.DoLogEventsToGA())
            GAAdsAnalyticsHandler.instance.GA_NewAdEvent(GameAnalyticsSDK.GAAdAction.Show, GameAnalyticsSDK.GAAdType.OfferWall, "Ironsource", IronSourceAdUnits.BANNER);
    }
    /**
     * Invoked when the method 'showOfferWall' is called and the OfferWall fails to load.  
    *@param desc - A string which represents the reason of the failure.
     */
    public void OfferwallShowFailedEvent(IronSourceError error)
    {
        onOfferwallShowFailedEvent?.Invoke(error);

        if (IronsourceManager.Instance.DoLogEventsToGA())
            GAAdsAnalyticsHandler.instance.GA_NewAdEvent(GameAnalyticsSDK.GAAdAction.FailedShow, GameAnalyticsSDK.GAAdType.OfferWall, "Ironsource", IronSourceAdUnits.BANNER);
    }
    /**
      * Invoked each time the user completes an offer.
      * Award the user with the credit amount corresponding to the value of the ‘credits’ 
      * parameter.
      * @param dict - A dictionary which holds the credits and the total credits.   
      */
    public void OfferwallAdCreditedEvent(Dictionary<string, object> dict)
    {
        onOfferwallAdCreditedEvent?.Invoke(dict);
        //Debug.Log("I got OfferwallAdCreditedEvent, current credits = "dict["credits"] + "totalCredits = " + dict["totalCredits"]);
    }
    /**
      * Invoked when the method 'getOfferWallCredits' fails to retrieve 
      * the user's credit balance info.
      * @param desc -string object that represents the reason of the  failure. 
      */
    public void GetOfferwallCreditsFailedEvent(IronSourceError error)
    {
        onGetOfferwallCreditsFailedEvent?.Invoke(error);
    }
    /**
      * Invoked when the user is about to return to the application after closing 
      * the Offerwall.
      */
    public void OfferwallClosedEvent()
    {
        onOfferwallClosedEvent?.Invoke();
    }
    #endregion

    #region Banner Methods
    //Invoked once the banner has loaded
    public void BannerAdLoadedEvent()
    {
        onBannerAdLoadedEvent?.Invoke();

        if (IronsourceManager.Instance.DoLogEventsToGA())
            GAAdsAnalyticsHandler.instance.GA_NewAdEvent(GameAnalyticsSDK.GAAdAction.Show, GameAnalyticsSDK.GAAdType.Banner, "Ironsource", IronSourceAdUnits.BANNER);

    }
    //Invoked when the banner loading process has failed.
    //@param description - string - contains information about the failure.
    public void BannerAdLoadFailedEvent(IronSourceError error)
    {
        onBannerAdLoadFailedEvent?.Invoke(error);

        if (IronsourceManager.Instance.DoLogEventsToGA())
            GAAdsAnalyticsHandler.instance.GA_NewAdEvent(GameAnalyticsSDK.GAAdAction.FailedShow, GameAnalyticsSDK.GAAdType.Banner, "Ironsource", IronSourceAdUnits.BANNER);
    }
    // Invoked when end user clicks on the banner ad
    public void BannerAdClickedEvent()
    {
        onBannerAdClickedEvent?.Invoke();

        if (IronsourceManager.Instance.DoLogEventsToGA())
            GAAdsAnalyticsHandler.instance.GA_NewAdEvent(GameAnalyticsSDK.GAAdAction.Clicked, GameAnalyticsSDK.GAAdType.Banner, "Ironsource", IronSourceAdUnits.BANNER);
    }
    //Notifies the presentation of a full screen content following user click
    public void BannerAdScreenPresentedEvent()
    {
        onBannerAdScreenPresentedEvent?.Invoke();
    }
    //Notifies the presented screen has been dismissed
    public void BannerAdScreenDismissedEvent()
    {
        onBannerAdScreenDismissedEvent?.Invoke();
    }
    //Invoked when the user leaves the app
    public void BannerAdLeftApplicationEvent()
    {
        onBannerAdLeftApplicationEvent?.Invoke();
    }
    #endregion

    private void OnDisable()
    {
        // INTERSTITIAL EVENTS
        IronSourceEvents.onInterstitialAdReadyEvent -= InterstitialAdReadyEvent;
        IronSourceEvents.onInterstitialAdLoadFailedEvent -= InterstitialAdLoadFailedEvent;
        IronSourceEvents.onInterstitialAdShowSucceededEvent -= InterstitialAdShowSucceededEvent;
        IronSourceEvents.onInterstitialAdShowFailedEvent -= InterstitialAdShowFailedEvent;
        IronSourceEvents.onInterstitialAdClickedEvent -= InterstitialAdClickedEvent;
        IronSourceEvents.onInterstitialAdOpenedEvent -= InterstitialAdOpenedEvent;
        IronSourceEvents.onInterstitialAdClosedEvent -= InterstitialAdClosedEvent;

        // REWARDED VIDEO EVENTS 
        IronSourceEvents.onRewardedVideoAdOpenedEvent -= RewardedVideoAdOpenedEvent;
        IronSourceEvents.onRewardedVideoAdClickedEvent -= RewardedVideoAdClickedEvent;
        IronSourceEvents.onRewardedVideoAdClosedEvent -= RewardedVideoAdClosedEvent;
        IronSourceEvents.onRewardedVideoAvailabilityChangedEvent -= RewardedVideoAvailabilityChangedEvent;
        IronSourceEvents.onRewardedVideoAdStartedEvent -= RewardedVideoAdStartedEvent;
        IronSourceEvents.onRewardedVideoAdEndedEvent -= RewardedVideoAdEndedEvent;
        IronSourceEvents.onRewardedVideoAdRewardedEvent -= RewardedVideoAdRewardedEvent;
        IronSourceEvents.onRewardedVideoAdShowFailedEvent -= RewardedVideoAdShowFailedEvent;

        // OFFERWALL EVENTS
        IronSourceEvents.onOfferwallClosedEvent -= OfferwallClosedEvent;
        IronSourceEvents.onOfferwallOpenedEvent -= OfferwallOpenedEvent;
        IronSourceEvents.onOfferwallShowFailedEvent -= OfferwallShowFailedEvent;
        IronSourceEvents.onOfferwallAdCreditedEvent -= OfferwallAdCreditedEvent;
        IronSourceEvents.onGetOfferwallCreditsFailedEvent -= GetOfferwallCreditsFailedEvent;
        IronSourceEvents.onOfferwallAvailableEvent -= OfferwallAvailableEvent;

        //BANNER EVENTS
        IronSourceEvents.onBannerAdLoadedEvent -= BannerAdLoadedEvent;
        IronSourceEvents.onBannerAdLoadFailedEvent -= BannerAdLoadFailedEvent;
        IronSourceEvents.onBannerAdClickedEvent -= BannerAdClickedEvent;
        IronSourceEvents.onBannerAdScreenPresentedEvent -= BannerAdScreenPresentedEvent;
        IronSourceEvents.onBannerAdScreenDismissedEvent -= BannerAdScreenDismissedEvent;
        IronSourceEvents.onBannerAdLeftApplicationEvent -= BannerAdLeftApplicationEvent;
    }
}
