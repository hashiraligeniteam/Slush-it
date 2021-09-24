using System.Collections;
using System.Collections.Generic; 
using UnityEngine;
using com.adjust.sdk;

public class AdjustExample :
    MonoBehaviour
{
    void Start()
    {
        //   li import this package into the project:
        // li https:llgithub.comladjustlunity_sdklreleases
#if UNITY_IOS
        /* Mandatory - set your iOS app token here */
        InitAdjust("91g4n1zsoo3k");

#elif UNITY_ANDROID
/* Mandatory - set your Android app token here */
        InitAdjust("YOUR_AMDROID_APP_TOKEN_HERE");
#endif
    }

    private void InitAdjust(string adjustAppToken)
    {
        var adjustConfig = new AdjustConfig(adjustAppToken,
            AdjustEnvironment.Production, //li AdjustEnvironment.Sandbox to test in dashboard
            true);
        adjustConfig.setLogLevel(AdjustLogLevel.Info); // li Adjustloglevel.Suppress to disable logs adjustConfig.setSendinBackground(true);
       new GameObject("Adjust").AddComponent<Adjust>(); // li do not remove or rename
//li Adjust.addSessionCallbackParameter("foo","'bar"');//li ifrequestedtosetsession-levelparameters
        Adjust.start(adjustConfig);
    }
}