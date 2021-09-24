using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAnalyticsSDK;

public class IronsourceManager : MonoBehaviour
{
    public static IronsourceManager Instance;
    [SerializeField] bool validateIntegration = false;
    [SerializeField] bool shouldTrackNetworkState = false;
    [SerializeField] string appKey;
    public bool initUsingSeperateAdUnits = false;

    #region Ad Units
    [SerializeField] bool interstitial;
    [SerializeField] bool rewardedVideo;
    [SerializeField] bool offerWall;
    [SerializeField] bool banner;
    #endregion
    [SerializeField] bool logEventsToGA;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        if (initUsingSeperateAdUnits)
        {
            if (interstitial)
            {
                IronSource.Agent.init(appKey, IronSourceAdUnits.INTERSTITIAL);
            }

            if (rewardedVideo)
            {
                IronSource.Agent.init(appKey, IronSourceAdUnits.REWARDED_VIDEO);
            }

            if (offerWall)
            {
                IronSource.Agent.init(appKey, IronSourceAdUnits.OFFERWALL);
            }

            if (banner)
            {
                IronSource.Agent.init(appKey, IronSourceAdUnits.BANNER);
            }
        }
        else
        {
            IronSource.Agent.init(appKey);
        }
    }

    private void Start()
    {
        if (validateIntegration)
            IronSource.Agent.validateIntegration();

        if (shouldTrackNetworkState)
            IronSource.Agent.shouldTrackNetworkState(true);

        if (logEventsToGA)
        {
            GameAnalyticsILRD.SubscribeIronSourceImpressions();
        }
    }

    public bool DoLogEventsToGA()
    {
        return logEventsToGA;
    }
}
