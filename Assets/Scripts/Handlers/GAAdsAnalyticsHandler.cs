using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAnalyticsSDK;

public class GAAdsAnalyticsHandler : MonoBehaviour
{
    public static GAAdsAnalyticsHandler instance;

    private void Awake()
    {
        instance = this;
    }

    #region GA Ad Events

    string latestRewardedVideoError = "";
    string latestInterstitialError = "";
    string currentRewardedVideoPlacement = "";

    public void GA_HandleRewardedAdFailedToLoad(string error)
    {
        // keep track of the latest error (optional, only needed if you want to track errors for "FailedShow" ad event)
        latestRewardedVideoError = error;

        Debug.Log("GAAdsAnalyticsHandler: GA_HandleRewardedAdFailedToLoad()");
    }

    // Rewarded Video Ads
    public void GA_LogRewardedVideoAdFailed(string adSdkName, string adPlacement)
    {
        GameAnalytics.NewAdEvent(GAAdAction.FailedShow, GAAdType.RewardedVideo, adSdkName, adPlacement);
        Debug.Log("GAAdsAnalyticsHandler: GA_LogRewardedVideoAdFailed()");
    }

    public void GA_HandleRewardedAdOpening()
    {
        // keep track of current rewarded video ad
        currentRewardedVideoPlacement = "rewardedVideo";
        // start timer for this ad identifier
        GameAnalytics.StartTimer(currentRewardedVideoPlacement);
        Debug.Log("GAAdsAnalyticsHandler: GA_HandleRewardedAdOpening()");
    }

    public void GA_HandleUserEarnedReward(string adSdkName, string adPlacement)
    {
        // send ad event - reward recieved
        GameAnalytics.NewAdEvent(GAAdAction.RewardReceived, GAAdType.RewardedVideo, adSdkName, adPlacement);
        Debug.Log("GAAdsAnalyticsHandler: GA_HandleUserEarnedReward()");
    }

    public void GA_HandleRewardedAdClosed(string adSdkName, string adPlacement)
    {
        if (currentRewardedVideoPlacement != null)
        {
            long elapsedTime = GameAnalytics.StopTimer(currentRewardedVideoPlacement);

            // send ad event for tracking elapsedTime
            GameAnalytics.NewAdEvent(GAAdAction.Show, GAAdType.RewardedVideo, adSdkName, adPlacement, elapsedTime);

            currentRewardedVideoPlacement = null;
            Debug.Log("GAAdsAnalyticsHandler: GA_HandleRewardedAdClosed()");
        }
    }

    // Interstattial Ads

    public void GA_HandleInterstitialFailedToLoad(string error)
    {
        // keep track of latest error (optional, only needed if you want to track errors for "FailedShow" ad event)
        latestInterstitialError = error;
        Debug.Log("GAAdsAnalyticsHandler: GA_HandleInterstitialFailedToLoad()");
    }

    public void GA_LogInterstatialAdFailed(string adSdkName, string adPlacement)
    {
        GameAnalytics.NewAdEvent(GAAdAction.FailedShow, GAAdType.Interstitial, adSdkName, adPlacement, getLatestAdError(this.latestRewardedVideoError));
        Debug.Log("GAAdsAnalyticsHandler: GA_LogInterstatialAdFailed()");
    }

    public void GA_HandleInterstitialOpened(string adSdkName, string adPlacement)
    {
        // send ad event
        GameAnalytics.NewAdEvent(GAAdAction.Show, GAAdType.Interstitial, adSdkName, adPlacement);
        Debug.Log("GAAdsAnalyticsHandler: GA_HandleInterstitialOpened()");
    }

    public void GA_HandleInterstitialLeftApplication(string adSdkName, string adPlacement)
    {
        // send ad event - ad click
        GameAnalytics.NewAdEvent(GAAdAction.Clicked, GAAdType.Interstitial, adSdkName, adPlacement);
        Debug.Log("GAAdsAnalyticsHandler: GA_HandleInterstitialLeftApplication()");
    }

    public void GA_NewAdEvent(GAAdAction gAAdAction,GAAdType gAAdType, string adSdkName, string adPlacement)
    {
        GameAnalytics.NewAdEvent(gAAdAction, gAAdType, adSdkName, adPlacement);
    }

    GAAdError getLatestAdError(string errorString)
    {
        GAAdError result = GAAdError.Unknown;
        switch (errorString)
        {
            case "Unknown":
                result = GAAdError.Unknown;
                break;
            case "Offline":
                result = GAAdError.Offline;
                break;
            case "NoFill":
                result = GAAdError.NoFill;
                break;
            case "InternalError":
                result = GAAdError.InternalError;
                break;
            case "InvalidRequest":
                result = GAAdError.InvalidRequest;
                break;
            case "UnableToPrecached":
                result = GAAdError.UnableToPrecache;
                break;
            default:
                result = GAAdError.Unknown;
                break;
        }

        return result;

    }
    #endregion

    void OnApplicationPause(bool paused)
    {
        if (paused)
        {
            if (currentRewardedVideoPlacement != null)
            {
                GameAnalytics.PauseTimer(currentRewardedVideoPlacement);
            }
        }
        else
        {
            if (currentRewardedVideoPlacement != null)
            {
                GameAnalytics.ResumeTimer(currentRewardedVideoPlacement);
            }
        }
    }
}
