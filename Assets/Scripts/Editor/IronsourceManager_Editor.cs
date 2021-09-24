
#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(IronsourceManager))]
public class IronsourceManager_Editor : Editor
{
    private SerializedProperty validateIntegration;
    private SerializedProperty shouldTrackNetworkState;
    private SerializedProperty appKey;
    private SerializedProperty initUsingSeperateAdUnits;
    private SerializedProperty interstitial;
    private SerializedProperty rewardedVideo;
    private SerializedProperty offerWall;
    private SerializedProperty banner;
    private SerializedProperty logEventsToGA;
    protected virtual void OnEnable()
    {
        validateIntegration = serializedObject.FindProperty("validateIntegration");
        shouldTrackNetworkState = serializedObject.FindProperty("shouldTrackNetworkState");
        appKey = serializedObject.FindProperty("appKey");
        initUsingSeperateAdUnits = serializedObject.FindProperty("initUsingSeperateAdUnits");
        interstitial = serializedObject.FindProperty("interstitial");
        rewardedVideo = serializedObject.FindProperty("rewardedVideo");
        offerWall = serializedObject.FindProperty("offerWall");
        banner = serializedObject.FindProperty("banner");
        logEventsToGA = serializedObject.FindProperty("logEventsToGA");
    }

    public override void OnInspectorGUI()
    {
        // base.OnInspectorGUI();
        // serializedObject.Update();
        DrawComponents();

        serializedObject.ApplyModifiedProperties();


    }

    void DrawComponents()
    {
        IronsourceManager ironsourceManager = (IronsourceManager)target;

        EditorGUILayout.PropertyField(validateIntegration, new GUIContent("Validate Integration", "Validate ironsource integrations"));
        EditorGUILayout.PropertyField(shouldTrackNetworkState, new GUIContent("Should Track Network State", "Should the sdk track network state?"));
        EditorGUILayout.PropertyField(appKey, new GUIContent("App Key", "Ironsource app key."));
        ironsourceManager.initUsingSeperateAdUnits = EditorGUILayout.Toggle("Init Using Seperate Ad Units", ironsourceManager.initUsingSeperateAdUnits);

        if (ironsourceManager.initUsingSeperateAdUnits)
        {
            EditorGUILayout.PropertyField(interstitial, new GUIContent("interstitial", "Init with intersititial."));
            EditorGUILayout.PropertyField(rewardedVideo, new GUIContent("rewardedVideo", "Init with rewarded video."));
            EditorGUILayout.PropertyField(offerWall, new GUIContent("offerWall", "Init with offerwall."));
            EditorGUILayout.PropertyField(banner, new GUIContent("banner", "Init with banner."));
        }
        else
        {
            EditorGUILayout.HelpBox("As you have 'initUsingSeperateAdUnits' set to false, please make sure that ad units are configured on ironsource platform.", MessageType.Warning);
        }

        EditorGUILayout.Space(20f);
        EditorGUILayout.PropertyField(logEventsToGA, new GUIContent("Log Events To Game Analytics", "Log events to Game Analytics."));
    }
}
#endif
