using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronsourceAdCallingHandler : MonoBehaviour
{
    #region Interstitial

    /// <summary>
    /// Loads an Interstitial Ad.
    /// </summary>
    public static void LoadInterstitialAd()
    {
        IronSource.Agent.loadInterstitial();
    }

    /// <summary>
    /// Shows an Interstitial Ad.
    /// </summary>
    public static void ShowInterstitialAd()
    {
        Debug.Log("Show interstitial in AdCallingHandler");
        IronSource.Agent.showInterstitial();
    }

    /// <summary>
    /// Shows an Interstitial Ad.
    /// </summary>
    /// <param name="placementName">Ad PlacementName on ironsource dashboard</param>
    public static void ShowInterstitialAd(string placementName)
    {
        IronSource.Agent.showInterstitial(placementName);
    }

    /// <summary>
    /// Checks if Interstitial Ad is ready.
    /// </summary>
    /// <returns>bool</returns>
    public static bool IsInterstitialReady()
    {
        return IronSource.Agent.isInterstitialReady();
    }

    /// <summary>
    /// Checks if Interstitial Ad is capped.
    /// </summary>
    /// <param name="placementName"></param>
    /// <returns>bool</returns>
    public static bool isInterstitialPlacementCapped(string placementName)
    {
        return IronSource.Agent.isInterstitialPlacementCapped(placementName);
    }
    #endregion

    #region Rewarded Video

    /// <summary>
    /// Shows a rewarded video Ad.
    /// </summary>
    public void ShowRewardedVideoAd()
    {
        IronSource.Agent.showRewardedVideo();
    }

    /// <summary>
    /// Shows a rewarded video Ad.
    /// </summary>
    /// <param name="placementName">Ad PlacementName on ironsource dashboard</param>
    public void ShowRewardedVideoAd(string placementName)
    {
        IronSource.Agent.showRewardedVideo(placementName);
    }

    /// <summary>
    /// Gets the placement Info of an Ad.
    /// </summary>
    /// <param name="placementName">Ad PlacementName on ironsource dashboard</param>
    /// <param name="placementInfo">Placement Informantion</param>
    public void GetPlacementInfo(string placementName, System.Action<string, int> placementInfo)
    {
        IronSourcePlacement placement = IronSource.Agent.getPlacementInfo(placementName);

        if (placement != null)
        {
            placementInfo.Invoke(placement.getRewardName(), placement.getRewardAmount());
        }
    }

    /// <summary>
    /// Checks if rewardede video ad is available.
    /// </summary>
    /// <returns></returns>
    public bool IsRewardedVideoAdAvailable()
    {
        return IronSource.Agent.isRewardedVideoAvailable();
    }

    /// <summary>
    /// Checks if rewarded video Ad is capped.
    /// </summary>
    /// <param name="placementName">Ad PlacementName on ironsource dashboard</param>
    /// <returns>bool</returns>
    public static bool isRewardedVideoPlacementCapped(string placementName)
    {
        return IronSource.Agent.isRewardedVideoPlacementCapped(placementName);
    }
    #endregion

    #region Offerwall
    /// <summary>
    /// Shows Offerwall.
    /// </summary>
    public void ShowOfferWall()
    {
        IronSource.Agent.showOfferwall();
    }

    /// <summary>
    /// Shows Offerwall
    /// </summary>
    /// <param name="placementName">Ad PlacementName on ironsource dashboard</param>
    public void ShowOfferWall(string placementName)
    {
        IronSource.Agent.showOfferwall(placementName);
    }

    public void GetOfferwallCredits()
    {
        IronSource.Agent.getOfferwallCredits();
    }

    /// <summary>
    /// Sets the client side callbacks of offerwall true. IMPORTANT NOTE: This code MUST be implemented before calling the init.
    /// </summary>
    public void SetClientSideCallbacks()
    {
        IronSourceConfig.Instance.setClientSideCallbacks(true);
    }
    #endregion

    #region Banner

    /// <summary>
    /// Loads a banner ad
    /// </summary>
    /// <param name="placementName">Ad PlacementName on ironsource dashboard</param>
    /// <param name="ironSourceBannerPosition">Banner position</param>
    public void LoadBannerAd(string placementName, IronSourceBannerPosition ironSourceBannerPosition = IronSourceBannerPosition.BOTTOM)
    {
        IronSource.Agent.loadBanner(IronSourceBannerSize.BANNER, ironSourceBannerPosition, placementName);
    }


    /// <summary>
    /// Hide the banner ad.
    /// </summary>
    public void HideBanner()
    {
        IronSource.Agent.hideBanner();
    }
    /// <summary>
    /// Displays the previously hidden banner ad.
    /// </summary>
    public void DisplayBanner()
    {
        IronSource.Agent.displayBanner();
    }

    /// <summary>
    /// Destroys the banner ad.
    /// </summary>
    public void DestroyBanner()
    {
        IronSource.Agent.destroyBanner();
    }
    #endregion
}
