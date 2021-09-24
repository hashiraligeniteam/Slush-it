using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IronsourceAdsTesting : MonoBehaviour
{

    public Text interstitialStatus;
    public Text rewardedVideoStatus;

    private void Awake()
    {
        IronsourceEventsHandler.onInterstitialAdReadyEvent = OnInterstitialAdReady;
        IronsourceEventsHandler.onInterstitialAdLoadFailedEvent = OnInterstitialAdLoadFailed;
        IronsourceEventsHandler.onInterstitialAdShowSucceededEvent = onInterstitialAdShowSucceeded;
        IronsourceEventsHandler.onInterstitialAdShowFailedEvent = onInterstitialAdShowFailed;

        IronsourceEventsHandler.onRewardedVideoAdOpenedEvent = OnRewarededAdReady;
        IronsourceEventsHandler.onRewardedVideoAdShowFailedEvent = OnRewarededAdLoadFailed;

    }

    public void LoadInterstitialAd()
    {
        IronsourceAdCallingHandler.LoadInterstitialAd();
    }

    public void ShowInterstitialAd()
    {
        IronsourceAdCallingHandler.ShowInterstitialAd();
    }

    public void ShowRewardedVideoAd()
    {
        IronsourceAdCallingHandler.ShowInterstitialAd();
    }

    public void OnInterstitialAdReady()
    {
        interstitialStatus.text = "Interstitial Ad Ready";
    }

    public void OnInterstitialAdLoadFailed(IronSourceError e)
    {

        interstitialStatus.text = "Interstitial Ad Load Failed. Reason: " + e.getDescription();
    }

    public void onInterstitialAdShowSucceeded()
    {
        interstitialStatus.text = "Interstitial Ad Show Succeeded";
    }

    public void onInterstitialAdShowFailed(IronSourceError e)
    {
        interstitialStatus.text = "Interstitial Ad Show Failed. Reason: " + e.getDescription();
    }

    public void OnRewarededAdReady()
    {
        rewardedVideoStatus.text = "Interstitial Ad Ready";
    }

    public void OnRewarededAdLoadFailed(IronSourceError e)
    {
        rewardedVideoStatus.text = "Interstitial Ad Load Failed. Reason: " + e.getDescription();
    }
}
