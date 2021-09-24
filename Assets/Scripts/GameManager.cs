
using System;
using System.Collections;
using DG.Tweening;
using GameAnalyticsSDK;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Outline = cakeslice.Outline;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    [Header("Players properties")] 
    public GameObject MainPlayer;
    [SerializeField] private int CurrentPlayer = 0;

    public bool isGameStarted;
    public GameObject[] Levels;
    public int[] LevelOptimalMoves;

    [Header("Tile properties")] 
    public int TotalTiles;
    public int TotalFilled = 0;
    public GameObject TilePrefab, DummyTilePrefab;


    [Header("Object Pooler")] 
    public ObjectPooler objectPooler;


    [Header("Settings")] 
    public bool isMainMenu;
    public Image menuFadeIn;
    public string nextScene;
    public GameObject CameraObject;
    public float[] CameraOrthoSizes;
    public Vector4[] FloatingClampLimit;
     public bool GameOverCheck = false;
     public bool GameSound = true;
     public int CurrentLevel;
     public Text LevelNumberText;
     public Text CompleteText;
     public string RestartScene, NextScene;
     public int DesiredLevel;
     public float GameOverDelay;
     
     // Level timer variables
     public float updateInterval = 0.5F;
     public double lastInterval;
     private int frames;
     private float fps;
     public double TotalTime;


     [Header("UI Elements")]
     public Text BloodCount;
     public GameObject HandAnim;
     public GameObject GameplayPanel, CompletePanel, FailPanel, PausePanel;
     
     [Header("Scoring elements")] 
     public Text movesText;
     public Text timetakenText;
     public Text bloodAwardedText;
     public Text bloodAwardedTop;
     public Image[] starImages;
     
     
     
     [Header("Other mechanics")] 
     public Material LiftMat;

     public GameObject[] PortalPrefab;
     public GameObject LiftPrefab;
     public GameObject FadeEffect;
     public GameObject[] PortalFill;
     public Material[] PortalMat;
     public GameObject LiftBar;

     [Header("Sound Manager")] 
     public SoundManager SoundController;
     


     public void RequestAd()
     {
         AnalyticsAdsManager.instance.RequestIronsourceInterstitialAd();
     }

     public void LoadLevel()
     {
         if (Levels.Length > 0)
         {
             CurrentLevel = PlayerPrefs.GetInt("CurrentLevel", 0);
             Levels[PlayerPrefs.GetInt("CurrentLevel", 0)].SetActive(true);
             GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start,
                 PlayerPrefs.GetInt("CurrentLevel") + 1.ToString());
             if(LevelNumberText)
                 LevelNumberText.text = "LEVEL " + (PlayerPrefs.GetInt("CurrentLevel", 0) + 1);
             TotalTiles = GameObject.FindGameObjectsWithTag("Ground").Length;
             TotalTiles += GameObject.FindGameObjectsWithTag("DummyTile").Length;
             if ((PlayerPrefs.GetInt("CurrentLevel", 0) + 1) > 0 &&
                 (PlayerPrefs.GetInt("CurrentLevel", 0) + 1) <= 5)
             {
                 CameraObject.GetComponent<Camera>().orthographicSize = CameraOrthoSizes[0];
                 MainPlayer.GetComponent<FloatingController>().xLimit = new Vector2(FloatingClampLimit[0].x, FloatingClampLimit[0].y);
                 MainPlayer.GetComponent<FloatingController>().zLimit = new Vector2(FloatingClampLimit[0].z, FloatingClampLimit[0].w);
             }else if ((PlayerPrefs.GetInt("CurrentLevel", 0) + 1) > 5 &&
                       (PlayerPrefs.GetInt("CurrentLevel", 0) + 1) <= 10)
             {
                 CameraObject.GetComponent<Camera>().orthographicSize = CameraOrthoSizes[1];
                 MainPlayer.GetComponent<FloatingController>().xLimit = new Vector2(FloatingClampLimit[1].x, FloatingClampLimit[1].y);
                 MainPlayer.GetComponent<FloatingController>().zLimit = new Vector2(FloatingClampLimit[1].z, FloatingClampLimit[1].w);
             }else if ((PlayerPrefs.GetInt("CurrentLevel", 0) + 1) > 10 &&
                       (PlayerPrefs.GetInt("CurrentLevel", 0) + 1) <= 15)
             {
                 CameraObject.GetComponent<Camera>().orthographicSize = CameraOrthoSizes[2];
                 MainPlayer.GetComponent<FloatingController>().xLimit = new Vector2(FloatingClampLimit[2].x, FloatingClampLimit[2].y);
                 MainPlayer.GetComponent<FloatingController>().zLimit = new Vector2(FloatingClampLimit[2].z, FloatingClampLimit[2].w);
             }else if ((PlayerPrefs.GetInt("CurrentLevel", 0) + 1) > 15 &&
                       (PlayerPrefs.GetInt("CurrentLevel", 0) + 1) <= 20)
             {
                 CameraObject.GetComponent<Camera>().orthographicSize = CameraOrthoSizes[3];
                 MainPlayer.GetComponent<FloatingController>().xLimit = new Vector2(FloatingClampLimit[3].x, FloatingClampLimit[3].y);
                 MainPlayer.GetComponent<FloatingController>().zLimit = new Vector2(FloatingClampLimit[3].z, FloatingClampLimit[3].w);
             }
         }
     }

     public void Pause()
     {
         if (!GameOverCheck)
         {
             PausePanel.SetActive(true);
             Time.timeScale = 0;
         }
     }

     public void ResumeGame()
     {
         Time.timeScale = 1;
         PausePanel.SetActive(false);
     }

     public void StartGame()
     {
         if (!isGameStarted)
         {
             ResumeGame();
             isGameStarted = true;
         }
         
     }

     public void SoundToggle(bool SwitchSound)
     {
         AudioListener.pause = SwitchSound;
     }
     


     private void Awake()
     {
         instance = this;
         Time.timeScale = 1.0f;
         Invoke("RequestAd", 2.0f);
         LoadLevel();
         InitializePlayers();
         initializeTimer();
         
     }

     void initializeTimer()
     {
         lastInterval = Time.realtimeSinceStartup;
         frames = 0;
     }


     private void Update()
     {
         if (!isGameStarted)
         {
             if (Input.GetMouseButtonDown(0))
             {
                 if(HandAnim)
                     HandAnim.SetActive(false);
                 isGameStarted = true;
             }
         }


         if (!GameOverCheck)
         {
             ++frames;
             float timeNow = Time.realtimeSinceStartup;
             if (timeNow > lastInterval + updateInterval)
             {
                 fps = (float)(frames / (timeNow - lastInterval));
                 frames = 0;
                 lastInterval = timeNow;
             }
         }
     }

     public void InitializePlayers()
    {
        
    }

    public void SwitchPlayer(int PlayerNo)
    {
//        for(int i = 0; i < MainPlayers.Length; i++)
//        {
////            MainPlayers[i].GetComponent<vThirdPersonInput>().Moving = 0;
////            PlayersButton[i].GetComponent<Image>().color = Color.gray;
//        }
//
//        CurrentPlayer = PlayerNo;
//        PlayersButton[CurrentPlayer].GetComponent<Image>().color = Color.white;
//        MainPlayers[CurrentPlayer].GetComponent<vThirdPersonInput>().Moving = 1;
    }


    public void ChangeAnimation(int State)
    {
//        MainPlayers[CurrentPlayer].GetComponent<PlayerMovement>().AnimationState(State);
    }

    public void UnlockMovement()
    { 
//        MainPlayers[CurrentPlayer].GetComponent<QuickerFixed>().UnlockMovement();
    }

    public void PlayerCurrentState(bool state)
    {
//        if(MainPlayers[CurrentPlayer].GetComponent<QuickerFixed>())
//            MainPlayers[CurrentPlayer].GetComponent<QuickerFixed>().isRunning = state;
//        if (state == true)
//        {
//            MainPlayers[CurrentPlayer].GetComponent<Animator>().SetBool("Crouch", false);
////            if (MainPlayers[CurrentPlayer].GetComponent<QuickerFixed>())
////            {
////                MainPlayers [CurrentPlayer].GetComponent<QuickerFixed>().isHiding = true;
////            }
//        }
//        else
//        {
////            if (MainPlayers[CurrentPlayer].GetComponent<QuickerFixed>())
////            {
////                MainPlayers[CurrentPlayer].GetComponent<QuickerFixed>().isHiding = false;
////            }
//        }
    }


    public void StarBonusCalculation()
    {
        
        int tempCount = MainPlayer.GetComponent<PlayerController>().MovesCounter;
        float givenStars = 0;
        if (tempCount <= LevelOptimalMoves[CurrentLevel])
        {
            givenStars = 3;
        }else if (tempCount <= LevelOptimalMoves[CurrentLevel] * 1.5f)
        {
            givenStars = 2f;
        }else if (tempCount <= LevelOptimalMoves[CurrentLevel] * 2f)
        {
            givenStars = 1f;
        }

        starImages[0].DOFillAmount(1, 0.75f).SetUpdate(true).OnComplete(delegate
        {
            starImages[0].transform.DOScale(1.25f, 0.5f).SetEase(Ease.InOutQuad).SetLoops(2,LoopType.Yoyo).SetUpdate(true);
            givenStars -= 1;
            if (givenStars > 0)
            {
                starImages[1].DOFillAmount(1, 0.75f).SetUpdate(true).OnComplete(delegate
                {
                    starImages[1].transform.DOScale(1.25f, 0.5f).SetEase(Ease.InOutQuad).SetLoops(2,LoopType.Yoyo).SetUpdate(true);
                    givenStars -= 1;
                    if (givenStars > 0)
                    {
                        starImages[2].DOFillAmount(1, 0.75f).SetUpdate(true).OnComplete(delegate
                        {
                            starImages[2].transform.DOScale(1.25f, 0.5f).SetEase(Ease.InOutQuad).SetLoops(2,LoopType.Yoyo).SetUpdate(true);
                            givenStars -= 1;
                            if (givenStars > 0)
                            {
                
                            }
                        });
                    }
                });
            }
        });

    }

    public void LevelComplete()
    {

        if (isMainMenu)
        {
            menuFadeIn.DOFade(1, 1).SetUpdate(true).OnComplete(delegate 
                { SceneManager.LoadScene(nextScene); });
        }
        else
        {
            if (!GameOverCheck)
            {
                SoundController.WinSound();
                GameOverCheck = true;
//            MainPlayers[CurrentPlayer].GetComponent<vThirdPersonInput>().AutoMovement = 1;
                GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete,
                    PlayerPrefs.GetInt("CurrentLevel") + 1.ToString());

                movesText.text = MainPlayer.GetComponent<PlayerController>().MovesCounter.ToString();
                timetakenText.text = lastInterval + " S";
                bloodAwardedText.text = MainPlayer.GetComponent<PlayerController>().bloodFill.ToString();
                bloodAwardedTop.text = MainPlayer.GetComponent<PlayerController>().bloodFill.ToString();
                StarBonusCalculation();
                CompleteText.text =
                    "LEVEL " + (PlayerPrefs.GetInt("CurrentLevel") + 1) + " CLEARED!";
                PlayerPrefs.SetInt("CurrentLevel", PlayerPrefs.GetInt("CurrentLevel") + 1);
                if(PlayerPrefs.GetInt("CurrentLevel") >= Levels.Length)
                {
                    CompleteText.text =
                        "Game complete! Restart from first level";
                    PlayerPrefs.SetInt("CurrentLevel", 0);
                }
                StartCoroutine(GameOver(GameOverDelay, true));
            }
        }
    }
    
    IEnumerator GameOver(float timer, bool complete) {
        yield return new WaitForSeconds(timer);
        GameplayPanel.SetActive(false);
        if (complete)
        {
            CompletePanel.SetActive(true);
        }
        else
        {
            FailPanel.SetActive(true);
        }
        yield return new WaitForSeconds(timer);
//        Time.timeScale = 0.0f;
    }

    public void LevelFail()
    {
        if (!GameOverCheck)
        {
            GameOverCheck = true;
            StartCoroutine(GameOver(GameOverDelay, false));
        }
    }



    public void RestartGame()
    {
        AnalyticsAdsManager.instance.ShowIronsourceInterstitialAd();
        SceneManager.LoadScene(RestartScene);
    }
    
    public void MainMenu()
    {
//        AnalyticsAdsManager.instance.ShowIronsourceInterstitialAd();
        SceneManager.LoadScene(NextScene);
    }
    
    
    
    
    
    [ContextMenu("SetOutline")]
    public void SetOutlineObject()
    {
        Outline[] tempObjects = GameObject.FindObjectsOfType<Outline>();
        foreach (var tempObject in tempObjects)
        {
            tempObject.color = 1;
        }
    }
    
    [ContextMenu("SetTileFade")]
    public void SetTileFade()
    {
        GameObject[] tempObjects = GameObject.FindGameObjectsWithTag("Ground");
        foreach (var tempObject in tempObjects)
        {
//            GameObject Tile = PrefabUtility.InstantiatePrefab(TilePrefab) as GameObject;
//            Tile.transform.SetParent(Levels[DesiredLevel].transform);
//            Tile.transform.localPosition = tempObject.transform.localPosition;
//            Tile.transform.localRotation = tempObject.transform.localRotation;
//            DestroyImmediate(tempObject.gameObject);
            
            
//            if (tempObject.transform.childCount <= 0)
//            {
//                GameObject temp = Instantiate(FadeEffect);
//                temp.transform.SetParent(tempObject.transform);
//                temp.transform.localPosition = new Vector3(0,0.515f,0);
//            }
        }
        
        GameObject[] tempTiles = GameObject.FindGameObjectsWithTag("DummyTile");
        foreach (var tempObject in tempTiles)
        {
            
            
//            GameObject Tile = PrefabUtility.InstantiatePrefab(DummyTilePrefab) as GameObject;
//            Tile.transform.SetParent(Levels[DesiredLevel].transform);
//            Tile.transform.localPosition = tempObject.transform.localPosition;
//            Tile.transform.localRotation = tempObject.transform.localRotation;
//            DestroyImmediate(tempObject.gameObject);
            
            
//            if (tempObject.transform.childCount <= 0)
//            {
//                GameObject temp = Instantiate(FadeEffect);
//                temp.transform.SetParent(tempObject.transform);
//                temp.transform.localPosition = new Vector3(0,0.515f,0);
//            }
        }
    }
    
    [ContextMenu("SetLiftBar")]
    public void SetLiftBar()
    {
        
        int tempCount = 0;
        GameObject[] tempLifts = GameObject.FindGameObjectsWithTag("Lift");
        for(int i = 0; i < tempLifts.Length; i++)
        {
            tempCount++;
            if (tempCount == 1)
            {
                tempLifts[i].GetComponent<LiftController>().InverseMovement = false;
            }
            else if (tempCount == 2)
            {
                tempLifts[i].GetComponent<LiftController>().InverseMovement = true;
                tempCount = 0;
            }
        }
        
//        int tempCount = 0;
//        GameObject[] tempObjects = GameObject.FindGameObjectsWithTag("Lift");
//        foreach (var tempObject in tempObjects)
//        {
//            tempCount++;
//            if (tempCount >= 2)
//            {
//                tempPortals[i].GetComponent<PortalController>().OtherPortal = tempPortals[i - 1];
//                if (tempPortals[i].transform.childCount <= 0)
//                {
//                    GameObject temp = Instantiate(PortalFill[0]);
//                    temp.transform.SetParent(tempPortals[i].transform);
//                    temp.transform.localPosition = new Vector3(0,0.505f,0);
//                }
//
////                    tempPortals[i].GetComponent<MeshRenderer>().material = PortalMat[0];
//                tempPortals[i].name = "PortalO";
//                    
//                if (tempPortals[i - 1].transform.childCount <= 0)
//                {
//                    GameObject temp = Instantiate(PortalFill[1]);
//                    temp.transform.SetParent(tempPortals[i - 1].transform);
//                    temp.transform.localPosition = new Vector3(0,0.505f,0);
//                }
//                tempPortals[i - 1].GetComponent<PortalController>().OtherPortal = tempPortals[i];
////                    tempPortals[i - 1].GetComponent<MeshRenderer>().material = PortalMat[1];
//                tempPortals[i - 1].name = "PortalB";
//                tempCount = 0;
//            }
            
            
//            for (int i = 0; i < tempObject.transform.childCount; i++)
//            {
//                DestroyImmediate(tempObject.transform.GetChild(i).gameObject);
//            }
//            tempObject.GetComponent<MeshRenderer>().material = LiftMat;
//            tempObject.transform.name = "LiftObject";

//            if (tempObject.transform.childCount <= 0)
//            {
//                GameObject temp = Instantiate(LiftBar);
//                temp.transform.SetParent(tempObject.transform);
//                temp.transform.localPosition = new Vector3(0,0.505f,0);
//                tempObject.GetComponent<LiftController>().liftFillBar =
//                    temp.transform.GetChild(0).GetChild(0).GetComponent<Image>();
//            }

//            GameObject temp = Instantiate(LiftBar);
//            temp.transform.SetParent(tempObject.transform);
//            temp.transform.localPosition = new Vector3(0,0.505f,0);
//        }
    }
    
    [ContextMenu("SetPortalBar")]
    public void SetPortalBar()
    {
//        GameObject[] tempObjects = GameObject.FindGameObjectsWithTag("Portal");
//        foreach (var tempObject in tempObjects)
//        {
//            for (int i = 0; i < tempObject.transform.childCount; i++)
//            {
//                DestroyImmediate(tempObject.transform.GetChild(i).gameObject);
//            }

            int tempCount = 0;
            tempPortals = GameObject.FindGameObjectsWithTag("Portal");
            for(int i = 0; i < tempPortals.Length; i++)
            {
                tempPortals[i].GetComponent<MeshRenderer>().material = LiftMat;
                tempPortals[i].layer = 0;
                tempPortals[i].transform.localScale = Vector3.one;
                if(!tempPortals[i].GetComponent<PortalController>())
                    tempPortals[i].AddComponent<PortalController>();
                tempCount++;
                if (tempCount >= 2)
                {
                    tempPortals[i].GetComponent<PortalController>().OtherPortal = tempPortals[i - 1];
                    if (tempPortals[i].transform.childCount <= 0)
                    {
                        GameObject temp = Instantiate(PortalFill[0]);
                        temp.transform.SetParent(tempPortals[i].transform);
                        temp.transform.localPosition = new Vector3(0,0.505f,0);
                    }

//                    tempPortals[i].GetComponent<MeshRenderer>().material = PortalMat[0];
                    tempPortals[i].name = "PortalO";
                    
                    if (tempPortals[i - 1].transform.childCount <= 0)
                    {
                        GameObject temp = Instantiate(PortalFill[1]);
                        temp.transform.SetParent(tempPortals[i - 1].transform);
                        temp.transform.localPosition = new Vector3(0,0.505f,0);
                    }
                    tempPortals[i - 1].GetComponent<PortalController>().OtherPortal = tempPortals[i];
//                    tempPortals[i - 1].GetComponent<MeshRenderer>().material = PortalMat[1];
                    tempPortals[i - 1].name = "PortalB";
                    tempCount = 0;
                }
                Debug.Log("Ran");
            }

//            if (tempObject.transform.childCount <= 0)
//            {
//                GameObject temp = Instantiate(LiftBar);
//                temp.transform.SetParent(tempObject.transform);
//                temp.transform.localPosition = new Vector3(0,0.505f,0);
//                tempObject.GetComponent<LiftController>().liftFillBar =
//                    temp.transform.GetChild(0).GetChild(0).GetComponent<Image>();
//            }

//            GameObject temp = Instantiate(LiftBar);
//            temp.transform.SetParent(tempObject.transform);
//            temp.transform.localPosition = new Vector3(0,0.505f,0);
//        }
    }
    
    [ContextMenu("SetDesiredLevel")]
    public void SetDesiredLevel(){
        PlayerPrefs.SetInt("CurrentLevel", DesiredLevel);
    }
    
    [ContextMenu("SetTileLayer")]
    public void SetNewTileLayer()
    {
        GameObject[] tempTiles = GameObject.FindGameObjectsWithTag("Ground");
        foreach (var tile in tempTiles)
        {
            tile.layer = 8;
            tile.transform.localScale = new Vector3(0.95f,0.95f, 0.95f);
        }
    }
    
    [ContextMenu("SetLiftInLevels")]
    public void SetLiftObject()
    {
        
        GameObject[] tempTiles = GameObject.FindGameObjectsWithTag("Lift");
        foreach (var tempObject in tempTiles)
        {
//            Transform tempParent = tempObject.transform.parent;
//            GameObject Tile = PrefabUtility.InstantiatePrefab(LiftPrefab) as GameObject;
//            Tile.transform.SetParent(tempParent);
//            Tile.transform.localPosition = tempObject.transform.localPosition;
//            Tile.transform.localRotation = tempObject.transform.localRotation;
//            DestroyImmediate(tempObject.gameObject);
        }
//        GameObject[] tempTiles = GameObject.FindGameObjectsWithTag("Lift");
//        foreach (var lift in tempTiles)
//        {
//            lift.name = "LiftObject";
//            lift.layer = 0;
//            lift.transform.localScale = Vector3.one;
//            lift.GetComponent<MeshRenderer>().material = LiftMat;
//            lift.AddComponent<LiftController>();
//            lift.GetComponent<LiftController>().distance = 1.0f;
//        }
    }


    public GameObject[] tempPortals;
    public GameObject[] newPortals;
    [ContextMenu("SetportalInLevels")]
    public void SetportalObjects()
    {
        int tempCount = 0;
//        GameObject[] tempPortals = new GameObject[GameObject.FindGameObjectsWithTag("Portal").Length];
        newPortals = new GameObject[GameObject.FindGameObjectsWithTag("Portal").Length];
        tempPortals = GameObject.FindGameObjectsWithTag("Portal");
        for(int i = 0; i < tempPortals.Length; i++)
        {
            tempCount++;
            Transform tempParent = tempPortals[i].transform.parent;
//            if (tempCount == 1)
//            {
//                GameObject Tile = PrefabUtility.InstantiatePrefab(PortalPrefab[0]) as GameObject;
//                Tile.transform.SetParent(tempParent);
//                Tile.transform.localPosition = tempPortals[i].transform.localPosition;
//                Tile.transform.localRotation = tempPortals[i].transform.localRotation;
//                newPortals[i] = Tile.gameObject;
//                DestroyImmediate(tempPortals[i].gameObject);
//            }else if (tempCount == 2)
//            {
//                GameObject Tile = PrefabUtility.InstantiatePrefab(PortalPrefab[1]) as GameObject;
//                Tile.transform.SetParent(tempParent);
//                Tile.transform.localPosition = tempPortals[i].transform.localPosition;
//                Tile.transform.localRotation = tempPortals[i].transform.localRotation;
//                newPortals[i] = Tile.gameObject;
//                DestroyImmediate(tempPortals[i].gameObject);
//                
//                newPortals[i].GetComponent<PortalController>().OtherPortal = newPortals[i - 1];
//                newPortals[i].name = "PortalB";
//                
//                newPortals[i - 1].GetComponent<PortalController>().OtherPortal = newPortals[i];
//                newPortals[i - 1].name = "PortalO";
//                
//                tempCount = 0;
//            }
        }
    }
    

}


[Serializable]

public class LevelDescription
{
    public bool[] PlayersActive = new bool[2];
    public Transform[] PlayersPositions;

}
