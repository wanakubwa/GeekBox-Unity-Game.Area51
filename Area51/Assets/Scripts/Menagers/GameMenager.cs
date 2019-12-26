using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System;
using PlayerWalletPattern;

public class GameMenager : MonoBehaviour
{
    [SerializeField] GameObject explosionBouncePrefab;
    [SerializeField] GameObject explosionHitPlayer;
    [SerializeField] GameObject explosionMine;
    [SerializeField] GameObject lvlEndTransition;
    [SerializeField] GameObject tvSnowTransition;
    [SerializeField] GameObject winMenu;
    [SerializeField] GameObject deadMenu;
    [SerializeField] GameObject adsPopout;
    [SerializeField] float waitTime;
    [SerializeField] int energyLoadInMin = 15;
    [SerializeField] List<int> lvlScenesIndex;
    [SerializeField] bool debug;

    List<GameObject> explosionBouncePrefabs = new List<GameObject>();
    List<GameObject> explosionsHitPlayer = new List<GameObject>();
    List<GameObject> explosionsMine = new List<GameObject>();

    Animator lvlEndAnimator;
    EnergyCounting energyCounting;

    PlayerWallet playerWallet = new PlayerWallet();

    EnergyCountingElement energyCountingElement;
    WinMenuElement winMenuElement;
    DeadMenuElement deadMenuElement;

    public float CurrentTimeEdited { set; get; } = 0f;
    public int CurrentLvlNum { set; get; }

    // Use this for initialization
    void Awake()
    {
        CheckSingletonExist();

        //Set framerate to 60
        Application.targetFrameRate = 60;

        InitializeBounceExplosionList();
        InitializeHitPlayerExplosionList();
        InitializeMineExplosionList();

        tvSnowTransition.SetActive(false);
        lvlEndTransition.SetActive(false);
        winMenu.SetActive(false);
        deadMenu.SetActive(false);
        adsPopout.SetActive(false);

        CurrentLvlNum = SceneManager.GetActiveScene().buildIndex;
        lvlEndAnimator = lvlEndTransition.GetComponent<Animator>();
        energyCounting = gameObject.GetComponent<EnergyCounting>();

        // Initializing self-objects
        energyCounting.InitializeCountingValues(energyLoadInMin);
        playerWallet.InitializeStartsValues(lvlScenesIndex);

        //Load Objects
        if(debug == false)
        {
            playerWallet.LoadPlayerWallet();
            energyCounting.LoadEnergyCountingTime();
        }

        //create wallet obserwers
        winMenuElement = new WinMenuElement(winMenu.GetComponent<WinmenuMenager>());
        energyCountingElement = new EnergyCountingElement(energyCounting);
        deadMenuElement = new DeadMenuElement(deadMenu.GetComponent<DeadMenuMenager>());

        //Add obserwers to wallet
        playerWallet.AddObserwer(winMenuElement);
        playerWallet.AddObserwer(energyCountingElement);
        playerWallet.AddObserwer(deadMenuElement);

        //initialize methods
        playerWallet.InitializeAllObserwers();
    }

    // to fix
    private void OnDestroy()
    {
        playerWallet.SavePlayerWallet();
        energyCounting.SaveEnergyCountingTime();
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            playerWallet.SavePlayerWallet();
            energyCounting.SaveEnergyCountingTime();
            // go to menu 
            LoadLvlNumber(0);
        }
        else
        {
            if(debug == false)
            {
                playerWallet.LoadPlayerWallet();
                energyCounting.LoadEnergyCountingTime();
            }
        }
    }

    private void CheckSingletonExist()
    {
        var numOfInstances = FindObjectsOfType<GameMenager>().Length;
        if (numOfInstances > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
            DontDestroyOnLoad(gameObject);
    }

    private void InitializeMineExplosionList()
    {
        for (int i = 0; i < 5; i++)
        {
            var tmp = Instantiate(explosionMine);
            tmp.transform.SetParent(transform);
            tmp.SetActive(false);
            explosionsMine.Add(tmp);
        }
    }

    private void InitializeHitPlayerExplosionList()
    {
        for (int i = 0; i < 3; i++)
        {
            var tmp = Instantiate(explosionHitPlayer);
            tmp.transform.SetParent(transform);
            tmp.SetActive(false);
            explosionsHitPlayer.Add(tmp);
        }
    }

    private void InitializeBounceExplosionList()
    {
        for(int i = 0; i < 10; i++)
        {
            var tmp = Instantiate(explosionBouncePrefab);
            tmp.transform.SetParent(transform);
            tmp.SetActive(false);
            explosionBouncePrefabs.Add(tmp);
        }
    }

    private void SetStartedParameters()
    {
        CurrentTimeEdited = 0f;
    }

    //Popout//
    public void CreateADPopout()
    {
        InGameEvents.CallUIButtonPress();
        adsPopout.SetActive(true);
    }

    //Wallet API//
    public void  AddObserwer(WalletObserwer walletObserwer)
    {
        playerWallet.AddObserwer(walletObserwer);
    }
    
    public void RemoveObserwer(WalletObserwer walletObserwer)
    {
        playerWallet.RemoveObserwer(walletObserwer);
    }

    public List<bool> GetUnlockedLvlsList()
    {
        return playerWallet.GetUnlockedLvlsList();
    }

    public void AddEnergy(int value)
    {
        playerWallet.AddEnergy(value);
    }

    public void SubstractEnergy(int value)
    {
        playerWallet.SubstractEnergy(value);
    }

    public bool IsEnoughEnergyForPlay()
    {
        if (playerWallet.IsEnoughEnergy())
            return true;
        else
        {
            // zrobic pop out z reklama filmu
            return false;
        }
    }

    public void AddTimeToLvl(float time)
    {
        playerWallet.AddNewEndTimeToLvl(SceneManager.GetActiveScene().buildIndex, CurrentTimeEdited);
    }

    public float BestTimeForCurrentLvl()
    {
        var tmp = playerWallet.GetBestTimeBylvlId(SceneManager.GetActiveScene().buildIndex);
        return tmp;
    }

    //Time Counter API//
    public float GetActualEnergyTime()
    {
        return energyCounting.GetActualEnergyTime();
    }

    // Animations API
    public void CreateExplosionBounceAnimAtPoint(Vector3 position)
    {
        bool isInactivePrefab = false;

        foreach(var element in explosionBouncePrefabs)
        {
            if(!isInactivePrefab &&!element.activeSelf)
            {
                element.gameObject.transform.position = position;
                element.gameObject.SetActive(true);
                isInactivePrefab = true;
            }
        }

        if(!isInactivePrefab)
        {
            var tmp = Instantiate(explosionBouncePrefab, position, Quaternion.identity) as GameObject;
            tmp.transform.SetParent(transform);
            explosionBouncePrefabs.Add(tmp);
        }
    }

    public void CreateExplosionHitPlayerAnimAtPoint(Vector3 position)
    {
        bool isInactivePrefab = false;

        foreach (var element in explosionsHitPlayer)
        {
            if (!isInactivePrefab && !element.activeSelf)
            {
                element.gameObject.transform.position = position;
                element.gameObject.SetActive(true);
                isInactivePrefab = true;
            }
        }

        if (!isInactivePrefab)
        {
            var tmp = Instantiate(explosionHitPlayer, position, Quaternion.identity) as GameObject;
            tmp.transform.SetParent(transform);
            explosionsHitPlayer.Add(tmp);
        }
    }

    public void CreateExplosionMineAnimAtPoint(Transform transformParam)
    {
        bool isInactivePrefab = false;

        foreach (var element in explosionsMine)
        {
            if (!isInactivePrefab && !element.activeSelf)
            {
                element.gameObject.transform.position = transformParam.position;
                element.gameObject.transform.rotation = transformParam.rotation;
                element.gameObject.SetActive(true);
                isInactivePrefab = true;
            }
        }

        if (!isInactivePrefab)
        {
            var tmp = Instantiate(explosionMine, transformParam.position, transformParam.rotation) as GameObject;
            tmp.transform.SetParent(transform);
            explosionsMine.Add(tmp);
        }
    }

    // Win/Dead/Main Menu's API //
    public void LoadLvlNumber(int lvlIndex)
    {
        CreateLvlTransition();
        CurrentLvlNum = lvlIndex;
        StartCoroutine(LvlTransitionQueue(lvlIndex));
    }
    
    public void LoadNextLvl()
    {
        if(lvlScenesIndex.Exists(x => x == (CurrentLvlNum+1)))
        {
            CreateLvlTransition();
            CurrentLvlNum++;
            StartCoroutine(LvlTransitionQueue(CurrentLvlNum));
        }
    }

    public void LoadCurrentLvl()
    {
        CreateLvlTransition();
        StartCoroutine(LvlTransitionQueue(CurrentLvlNum));
    }

    // Scenes Transition and loads 
    private void CreateLvlTransition()
    {
        lvlEndTransition.SetActive(true);
    }

    private void LoadLvl(int lvlNumber)
    {
        SceneManager.LoadScene(lvlNumber);
        SetStartedParameters();
    }

    private IEnumerator LvlTransitionQueue(int lvlNumber)
    {
        while (lvlEndAnimator.GetCurrentAnimatorStateInfo(0).IsName("LvlTransition"))
        {
            yield return null;
        }
        LoadLvl(lvlNumber);
    }

    private IEnumerator LvlTransitionWaiter(float time)
    {
        yield return new WaitForSeconds(time);
        winMenu.SetActive(false);
        deadMenu.SetActive(false);
        lvlEndAnimator.SetBool("isExit", true);
    }

    // Lvl loaded success
    public void LvlLoadedSuccess()
    {
        StartCoroutine(LvlTransitionWaiter(waitTime));
    }

    // Clear stage and loose events
    private void CreateMenuTransition(GameObject gameObject)
    {
        tvSnowTransition.SetActive(true);
        StartCoroutine(EnableMenuAtTime(gameObject));
    }

    private IEnumerator EnableMenuAtTime(GameObject gameObject)
    {
        var tmp = tvSnowTransition.GetComponent<Animator>();
        while (tmp.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.5f)
            yield return null;
        gameObject.SetActive(true);
    }

    public void StageClearBehaviour()
    {
        CreateMenuTransition(winMenu);
    }

    public void DeadPlayerBehaviour()
    {
        CreateMenuTransition(deadMenu);
        playerWallet.SubstractEnergy(1);
    }
}