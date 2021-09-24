using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAnalyticsSDK;

public class GameAnalyticsManager : MonoBehaviour
{
    private void Awake()
    {
        GameAnalytics.Initialize();
    }

    /// <summary>
	/// Logs the business event.
	/// </summary>
	/// <param name="amount">Amount of Purchaseable in Cents i.e. 0.99$ = 99cents .</param>
	/// <param name="itemId">Give your inApp item Key..cncoins1 , cncoins2 etc </param>
	public static void LogBusinessEvent(int amount, string itemId, string reciept, string signature)
    {
#if UNITY_IPHONE
        GameAnalytics.NewBusinessEventIOS("USD", amount, "CoinsPack", itemId, "InAPP" , reciept);
#endif
#if UNITY_ANDROID
        GameAnalytics.NewBusinessEventGooglePlay("USD", amount, "HintPack", itemId, "InAPP", reciept, signature);
#endif
    }

    /// <summary>
    /// Logs progression event.
    /// </summary>
    /// <param name="status">progression status</param>
    /// <param name="progression">progression string</param>
    public static void LogProgressionEvent(GAProgressionStatus status, string progression)
    {
        GameAnalytics.NewProgressionEvent(status, progression);
    }

    /// <summary>
    /// Logs progression event.
    /// </summary>
    /// <param name="status">progression status</param>
    /// <param name="progression01">progression string 1</param>
    /// <param name="progression02">progression string 2</param>
    public static void LogProgressionEvent(GAProgressionStatus status, string progression01, string progression02)
    {
        GameAnalytics.NewProgressionEvent(status, progression01, progression02);
    }

    /// <summary>
    /// Logs progression event.
    /// </summary>
    /// <param name="status">progression status</param>
    /// <param name="progression01">progression string 1</param>
    /// <param name="progression02">progression string 2</param>
    /// <param name="progression03">progression string 3</param>
    public static void LogProgressionEvent(GAProgressionStatus status, string progression01, string progression02, string progression03)
    {
        GameAnalytics.NewProgressionEvent(status, progression01, progression02, progression03);
    }

    /// <summary>
    /// Logs progression event.
    /// </summary>
    /// <param name="status">progression status</param>
    /// <param name="progression01">progression string 1</param>
    /// <param name="score">score</param>
    public static void LogProgressionEvent(GAProgressionStatus status, string progression01, int score)
    {
        GameAnalytics.NewProgressionEvent(status, progression01, score);
    }

    /// <summary>
    /// Logs progression event.
    /// </summary>
    /// <param name="status">progression status</param>
    /// <param name="progression01">progression string 1</param>
    /// <param name="progression02">progression string 2</param>
    /// <param name="score">Score</param>
    public static void LogProgressionEvent(GAProgressionStatus status, string progression01, string progression02, int score)
    {
        GameAnalytics.NewProgressionEvent(status, progression01, progression02, score);
    }

    /// <summary>
    /// Logs progression event.
    /// </summary>
    /// <param name="status">progression status</param>
    /// <param name="progression01">progression string 1</param>
    /// <param name="progression02">progression string 2</param>
    /// <param name="progression03">progression string 2</param>
    /// <param name="score">Score</param>
    public static void LogProgressionEvent(GAProgressionStatus status, string progression01, string progression02, string progression03, int score)
    {
        GameAnalytics.NewProgressionEvent(status, progression01, progression02, progression03, score);
    }

    /// <summary>
    /// Logs Design event.
    /// </summary>
    /// <param name="eventName">Event Name</param>
    public static void LogDesignEvent(string eventName)
    {
        //Debug.Log("GA desigin event: " + eventName);
        GameAnalytics.NewDesignEvent(eventName);
    }

    /// <summary>
	/// Logs the info of Virtual currency(Coins) Gained ( source ) or spent (sink) resource event.
	/// </summary>
	/// <param name="flowType">Flow type can be GAResourceFlowType.Sink in case of spending or GAResourceFlowType.Source in case of earning ..</param>
	/// <param name="currency">Currency = Pass Constant.currency </param>
	/// <param name="amount">Amount = coins amount earned from RewardedVid,LevelComplete,InAPP OR Spent on Buying store Items.   </param>
	/// <param name="itemType"> for Earning = RewardedVid,LevelComplete,InAPP : For Spending = Store </param>
	/// <param name="itemId"> for earning it can be empty For spending it should be in store = Pack1 , Pack2 , Pack3 , pack4..  </param>
	public static void LogResourceEvent(GAResourceFlowType flowType, string currency, float amount, string itemType, string itemId)
    {
        //Debug.Log("Prog: : " + GAResourceFlowType.Sink.ToString() + ":" + currency + ":" + amount.ToString() + ":" + itemType + ":" + itemId);
        GameAnalytics.NewResourceEvent(flowType, currency, amount, itemType, itemId);
    }
}
