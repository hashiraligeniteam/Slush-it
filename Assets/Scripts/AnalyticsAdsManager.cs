using System;
using Facebook.Unity;
using Facebook.Unity.Settings;
using Firebase.Analytics;
using GameAnalyticsSDK;
using GameAnalyticsSDK.Setup;
using UnityEngine;

#if UNITY_IOS
using Unity.Advertisement.IosSupport;
#endif

public class AnalyticsAdsManager : MonoBehaviour
{
    //Analytics Manager by Arslan Asif

    public static AnalyticsAdsManager instance;
    private bool isInitialized = false;

    [Header("Analytics Manager")] public Platform PlatformType;
    public bool SubscribeIRLDImressions = false;
    public bool GameAnalyticsIntegrated = false;
    public bool MopubIntegrated = false;
    public bool FacebookIntegrated = false;
    public bool FirebaseIntegrated = false;
    public bool GameUtilsIntegrated = false;
    public bool GameAdjustIntegrated = false;


    [Header("Game Analytics Keys")] public String GAGameKey;
    public String GASecretKey;
    public GameObject GameAnalyticsManager;

    [Header("Mopub Keys")] public String AdUnitId;
    public String[] InterstitialId;
    public GameObject MoPubManagerObject;


    [Header("Facebook Keys")] public String FBKey;

    [Header("Game Utils Keys")] 
    public String GameUtilsKey;
    public GameObject GameUtilsManager;


    [Header("Game Adjust Keys")] public String GameAdjustToken;
    private GameObject GameAdjustManager;

    [Header(("Ironsource"))] public IronsourceManager IronsourceManager;


    private void OnDisable()
    {
        if (SubscribeIRLDImressions)
        {
//            MoPubManager.OnInterstitialLoadedEvent -= InterstitialLoadedEvent;
//            MoPubManager.OnInterstitialShownEvent -= InterstitialShownEvent;
//            MoPubManager.OnInterstitialFailedEvent -= InterstitialFailedEvent;
//            MoPubManager.OnInterstitialClickedEvent -= InterstitialClickedEvent;
        }
    }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
#if UNITY_IOS
        if (ATTrackingStatusBinding.GetAuthorizationTrackingStatus() == 
            ATTrackingStatusBinding.AuthorizationTrackingStatus.NOT_DETERMINED)
        {

            ATTrackingStatusBinding.RequestAuthorizationTracking();

        }

#endif

        if (!isInitialized)
        {
            InitializeGameAnalytics();
            //Invoke("InitializeMopub", 2);
            InitializeFacebook();
            InitializeGameUtils();
            InitializeGameAdjust();
            InitializeFirebase();
            isInitialized = true;
        }

        if (SubscribeIRLDImressions)
        {
//            MoPubManager.OnInterstitialLoadedEvent += InterstitialLoadedEvent;
//            MoPubManager.OnInterstitialShownEvent += InterstitialShownEvent;
//            MoPubManager.OnInterstitialFailedEvent += InterstitialFailedEvent;
//            MoPubManager.OnInterstitialClickedEvent += InterstitialClickedEvent;
        }
    }


    public bool CheckNullValues()
    {
        bool Checked = true;

        if (GameAnalyticsIntegrated)
        {
            if (GAGameKey.Equals(""))
                Checked = false;
            if (GASecretKey.Equals(""))
                Checked = false;
        }

        if (MopubIntegrated)
        {
            if (AdUnitId.Equals(""))
                Checked = false;
            if (InterstitialId[0].Equals(""))
                Checked = false;
        }

        if (FacebookIntegrated)
        {
            if (FBKey.Equals(""))
                Checked = false;
        }

        if (GameUtilsIntegrated)
        {
            if (GameUtilsKey.Equals(""))
                Checked = false;
        }

        if (GameAdjustIntegrated)
        {
            if (GameAdjustToken.Equals(""))
                Checked = false;
        }

        return Checked;
    }


    //Run the following function once you have filled all the above sections

    [ContextMenu("Populate All Values")]
    public void PopulateAll()
    {
        if (CheckNullValues())
        {
            PopulateGameAnalytics();
            //PopulateMoPub();
            PopulateFacebookAnalytics();
            PopulateGameUtils();
            PopulateGameAdjust();
        }
        else
        {
            Debug.LogError("Please check if the required values are filled properly and try again!");
        }
    }

    public void PopulateGameAnalytics()
    {
        if (GameAnalyticsIntegrated)
        {
            Settings GameAnalyticsSettings = Resources.Load<Settings>("GameAnalytics/Settings");
            if (PlatformType == Platform.Android)
            {
                GameAnalyticsSettings.AddPlatform(RuntimePlatform.Android);
                GameAnalyticsSettings.UpdateGameKey(0, GAGameKey);
                GameAnalyticsSettings.UpdateSecretKey(0, GASecretKey);
                GameAnalyticsSettings.Build[0] = Application.version;
            }
            else if (PlatformType == Platform.iOS)
            {
                GameAnalyticsSettings.AddPlatform(RuntimePlatform.IPhonePlayer);
                GameAnalyticsSettings.UpdateGameKey(0, GAGameKey);
                GameAnalyticsSettings.UpdateSecretKey(0, GASecretKey);
                GameAnalyticsSettings.Build[0] = Application.version;
            }

//            GameAnalyticsManager = Resources.Load<GameObject>("GameAnalytics");
//            Instantiate(GameAnalyticsManager);
        }
    }


    public void InitializeGameAnalytics()
    {
        if (GameAnalyticsIntegrated)
            GameAnalytics.Initialize();

        if (SubscribeIRLDImressions)
        {
//            GameAnalyticsILRD.SubscribeMoPubImpressions();
            GameAnalyticsILRD.SubscribeIronSourceImpressions();
        }
    }


    public void PopulateMoPub()
    {
        if (MopubIntegrated)
        {
//            MoPubManagerObject = Resources.Load<GameObject>("MoPubManager");
//            MoPubManagerObject.GetComponent<MoPubManager>().AdUnitId = AdUnitId;
//            Instantiate(MoPubManagerObject);
        }
    }


//    public void InitializeMopub()
//    {
//        MoPub.LoadInterstitialPluginsForAdUnits(InterstitialId);
//    }

    public void RequestIronsourceInterstitialAd()
    {
        if (!IronsourceAdCallingHandler.IsInterstitialReady())
        {
            IronsourceAdCallingHandler.LoadInterstitialAd();
        }
    }

    public void ShowIronsourceInterstitialAd()
    {
        Debug.Log("Show interstitial in AnalyticsAdsManager");
        IronsourceAdCallingHandler.ShowInterstitialAd();
    }

    public void RequestMopub()
    {
//        if (MopubIntegrated)
//        {
//            if (!MoPub.IsInterstitialReady(InterstitialId[0]))
//            {
//                MoPub.RequestInterstitialAd(InterstitialId[0]);
//            }
//        }
    }

    public void ShowInterstitial()
    {
//        if (MoPub.IsInterstitialReady(InterstitialId[0]))
//        {
//            MoPub.ShowInterstitialAd(InterstitialId[0]);
//        }
    }


    public void PopulateFacebookAnalytics()
    {
        if (FacebookIntegrated)
        {
            FacebookSettings.AppIds[0] = FBKey;
        }
    }

    public void InitializeFacebook()
    {
        if (!FB.IsInitialized)
        {
            // Initialize the Facebook SDK
            FB.Init(InitCallback, OnHideUnity);
        }
        else
        {
            // Already initialized, signal an app activation App Event
            FB.ActivateApp();
        }
    }

    private void InitCallback()
    {
        if (FB.IsInitialized)
        {
            // Signal an app activation App Event
            FB.ActivateApp();
            // Continue with Facebook SDK
            // ...
        }
        else
        {
            Debug.Log("Failed to Initialize the Facebook SDK");
        }
    }

    private void OnHideUnity(bool isGameShown)
    {
        if (!isGameShown)
        {
            // Pause the game - we will need to hide
            Time.timeScale = 0;
        }
        else
        {
            // Resume the game - we're getting focus again
            Time.timeScale = 1;
        }
    }

    public void PopulateGameUtils()
    {
        if (GameUtilsIntegrated)
        {
//            GameUtilsObject = Resources.Load<YCConfig>("YCConfigData");
//            GameUtilsObject.gameYcId = GameUtilsKey;
//            GameUtilsObject.EditorImportConfig();
//            GameUtilsManager = Resources.Load<GameObject>("YCManager");
//            Instantiate(GameUtilsManager);
        }
    }

    public void InitializeGameUtils()
    {
        if (GameUtilsIntegrated)
        {
        }
    }


    public void PopulateGameAdjust()
    {
        if (GameAdjustIntegrated)
        {
//            AdjustManager TempGameAdjust = FindObjectOfType<MmpManager>().adjustManager;
//            TempGameAdjust.gameObject.SetActive(true);
//            TempGameAdjust.adjust.appToken = GameAdjustToken;
//            TempGameAdjust.adjust.environment = AdjustEnvironment.Production;
//            GameAdjustManager = TempGameAdjust.gameObject;
        }
    }


    public void InitializeGameAdjust()
    {
//        GameAdjustManager.SetActive(true);
    }

    public void InitializeFirebase()
    {
        if (FirebaseIntegrated)
        {
            Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
            {
                var dependencyStatus = task.Result;
                if (dependencyStatus == Firebase.DependencyStatus.Available)
                {
                    FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
                }
                else
                {
                    Debug.LogError(System.String.Format("Could not resolve all Firebase dependencies: {0}"
                        , dependencyStatus));
                }
            });
        }
    }

    #region GA Ads Events

    private void InterstitialLoadedEvent(string adId)
    {
        GameAnalytics.NewAdEvent(GAAdAction.Loaded, GAAdType.Interstitial, "mopub", InterstitialId[0]);
    }

    private void InterstitialShownEvent(string adId)
    {
        GameAnalytics.NewAdEvent(GAAdAction.Show, GAAdType.Interstitial, "mopub", InterstitialId[0]);
    }

    private void InterstitialFailedEvent(string adId, string index)
    {
        GameAnalytics.NewAdEvent(GAAdAction.FailedShow, GAAdType.Interstitial, "mopub", InterstitialId[0]);
    }

    private void InterstitialClickedEvent(string adId)
    {
        GameAnalytics.NewAdEvent(GAAdAction.Clicked, GAAdType.Interstitial, "mopub", InterstitialId[0]);
    }

    #endregion
}

public enum Platform
{
    Android,
    iOS
}