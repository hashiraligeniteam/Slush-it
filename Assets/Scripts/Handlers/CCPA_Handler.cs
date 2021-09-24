using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCPA_Handler : MonoBehaviour
{
    [SerializeField] bool isAppChildDirected = false;

    private void Start()
    {
        if (isAppChildDirected)
        {
            // For Admob
            IronSource.Agent.setMetaData("AdMob_TFCD", "true");
            IronSource.Agent.setMetaData("AdMob_TFUA", "true");

            // For Apploving
            IronSource.Agent.setMetaData("AppLovin_AgeRestrictedUser", "true");

            // For Adcolony
            IronSource.Agent.setMetaData("AdColony_COPPA", "true");
        }
        else
        {
            // For Admob
            IronSource.Agent.setMetaData("AdMob_TFCD", "false");
            IronSource.Agent.setMetaData("AdMob_TFUA", "false");

            // For Apploving
            IronSource.Agent.setMetaData("AppLovin_AgeRestrictedUser", "false");

            // For Adcolony
            IronSource.Agent.setMetaData("AdColony_COPPA", "true");
        }
    }
}
