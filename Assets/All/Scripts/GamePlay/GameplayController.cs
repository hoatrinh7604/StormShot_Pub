using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

using JsonClass;
public class GameplayController : MonoBehaviour
{
    [SerializeField] int bulletQuantity;

    [SerializeField] BulletsRemaining bulletsRemaining;
    [SerializeField] float timeDelayShoot;
    [SerializeField] PlayerController playerController;
    [SerializeField] Transform crossHair;

    [SerializeField] UIController uIController;
    
    public int numberOfEnemy { get; private set; }
    public int baseEnemies { get; private set; }

    private int numberOfHostage = 1;
    public bool isWinGame = false;
    private int userCash = 0;
    private float timing;
    private int currentLevel = 1;
    private bool allyDeath = false;

    [SerializeField] GameDataController gameDataController;
    [SerializeField] MissionsController missionsController;
    [SerializeField] PauseController pauseController;

    private bool isTurnBaseMode = false;
    [SerializeField] TurnBaseMode turnBaseMode;

    

    [SerializeField] float checkingStateDelay = 3;
    private float timeChecking;
    private int bulletInScene = 0;

    public int resultState { get; private set; }
    public int gameState {get; private set;}

    // Create singleton
    public static GameplayController Instance { get; private set; }

    private void Awake()
    {
        uIController = GameElement.Instance.uIController;
        playerController = GameElement.Instance.playerController;
        missionsController = GameElement.Instance.missionsController;
        pauseController = GameElement.Instance.pauseController;
        crossHair = GameElement.Instance.crosshairMovement.transform;

        turnBaseMode = GameElement.Instance.turnBaseMode;
        if(turnBaseMode)
        {
            isTurnBaseMode = true;
        }

        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        SetSceneData();
        SetMissionData();
    }

    // Update is called once per frame
    void Update()
    {
        timing -= Time.deltaTime;
        timeChecking -= Time.deltaTime;

        if(timeChecking < 0 && bulletInScene <= 0)
        {
            CheckState();
            timeChecking = 100;
        }

        if (Input.GetMouseButtonUp(0))
        {
            Shoot();
        }

    }

    public void Shoot()
    {
        if (timing < 0 && bulletQuantity > 0)
        {
            if (uIController.IsPointerOverUIElement()) return;
            if (gameState == (int)GameState.PAUSE) return;

            playerController.Shooting(Random.Range(0, 2), crossHair);
            bulletQuantity--;
            bulletsRemaining.ReduceQuantity();
            timing = timeDelayShoot;
            bulletInScene++;
            if (turnBaseMode)
            {
                GameElement.Instance.enemyShootController.IncreaseRatioKill(10);
            }
        }
    }

    public void BulletOnDestroy()
    {
        timeChecking = checkingStateDelay;
        bulletInScene--;
    }

    public void EnemyDeath()
    {
        numberOfEnemy--;
        timeChecking = 3;
    }

    public void HostageDeath()
    {
        numberOfHostage--;
        allyDeath = true;
        timeChecking = checkingStateDelay;
        //StartCoroutine(GoToGameOver(false));
    }

    public void PlayerDeath()
    {
        allyDeath = true;
        timeChecking = checkingStateDelay;
        gameState = (int)GameState.PAUSE;
    }

    public void CheckState(bool isBulletEffect = false)
    {
        CheckStateAfterTime(isBulletEffect);
    }

    public void CheckStateAfterTime(bool isBulletEffect = false)
    {
        
            if (bulletQuantity <= 0 && numberOfEnemy > 0)
            {
                StartCoroutine(GoToGameOver((int)TypeOfEnd.OUT_OF_BULLET));
            }
            else if (allyDeath)
            {
                StartCoroutine(GoToGameOver((int)TypeOfEnd.YOU_DEAD));
            }
            else if (numberOfEnemy <= 0)
            {
                StartCoroutine(GoToGameOver((int)TypeOfEnd.WIN));
            }
    }

    public IEnumerator GoToGameOver(int typeOfEnd)
    {
        gameState = (int)GameState.PAUSE;
        if (isTurnBaseMode) GameElement.Instance.enemyShootController.HideShotLine();
        yield return null;
        GameOver(typeOfEnd);
    }

    int starLevel = 0;
    public void GameOver(int typeOfEnd)
    {
        gameState = (int)GameState.PAUSE;
        //CompletedLevel(isWin);
        if (typeOfEnd == (int)TypeOfEnd.WIN)
        {
            starLevel = GetStars();

            if (SoundController.Instance != null)
            {
                SoundController.Instance.PlayAudio(SoundController.Instance.victory, 1, false);
            }
        }
        else
        {
            starLevel = 0;
            
            if (SoundController.Instance != null)
            {
                SoundController.Instance.PlayAudio(SoundController.Instance.lose, 1, false);
            }
        }

        if (typeOfEnd == (int)TypeOfEnd.WIN)
        {
            // Win state
            WinGame();
            isWinGame = true;
            //resultState = (int)ResultState.win;
            //CompletedLevel();
        }
        else
        {
            // Lose state
            LoseGame();
            isWinGame = false;
            //resultState = (int)ResultState.lose;
            //CompletedLevel();
        }
        CompletedLevel();

        if (isWinGame) SetNextLevel();

        StartCoroutine(ShowGameOverPopUp(typeOfEnd));
    }

    IEnumerator ShowGameOverPopUp(int typeOfEnd)
    {
        yield return new WaitForSeconds(3);
        uIController.ShowGameOver(true, typeOfEnd, starLevel);
        uIController.ShowStars(starLevel);
    }

    public void ContinueGameAfterWatchADs()
    {
        if(isTurnBaseMode)
        {
            turnBaseMode.BossWakeUp();
        }

        // Hostage
        var hostage = GameElement.Instance.hostageControllers;
        foreach (var h in hostage)
        {
            var control = h.GetComponent<HostageController>();
            if (!control.isDeath)
            {
                control.SetState((int)AnimState.IDLE);
                control.DisbaleChatBox(true);
            }
        }

        // Enemy
        var enemy = GameElement.Instance.enemyControllers;
        foreach (var h in enemy)
        {
            var control = h.GetComponent<EnemyController>();
            if (!control.isDeath)
            {
                control.SetState((int)AnimState.IDLE);
            }
        }

        // Player
        GameElement.Instance.playerController.SetState((int)AnimState.AIMING);

        bulletQuantity += 5;
        bulletsRemaining.SpawBulletFollowQuantity(bulletQuantity);
        uIController.HideGameOver();
        PauseControl(false);

        // Increase revive
        PlayerPrefs.SetInt("ReviveCount", PlayerPrefs.GetInt("ReviveCount") + 1);
    }

    public void CompletedLevel(int scaleRewards = 1)
    {
        int cashReward = GetCashReward(isWinGame);
        cashReward *= scaleRewards;
        userCash += cashReward;
        uIController.UpdateCash(cashReward);
        uIController.AddedCash(cashReward);

        gameDataController.CompletedLevel(currentLevel, starLevel, cashReward);

        PlayerPrefs.SetInt("CashWon", PlayerPrefs.GetInt("CashWon") + cashReward);

        //if(isWinGame)
        //    SetNextLevel();
    }



    public void CompletedLevelForReplay(int scaleRewards = 1)
    {
        int cashReward = GetCashReward(isWinGame);
        cashReward *= scaleRewards;
        userCash += cashReward;
        uIController.UpdateCash(cashReward);
        uIController.AddedCash(cashReward);

        gameDataController.CompletedLevel(currentLevel, starLevel, cashReward);

        PlayerPrefs.SetInt("CashWon", PlayerPrefs.GetInt("CashWon") + cashReward);
    }

    public void SkipLevel()
    {
        gameDataController.CompletedLevel(currentLevel, 0, 0);
        SetNextLevel();
    }

    public void StayThisLevel()
    {
        PlayerPrefs.SetInt("CurrentProgress", PlayerPrefs.GetInt("CurrentProgress") - 1);

        PlayerPrefs.SetInt("ChoosenLevel", currentLevel);
    }

    public int GetCashReward(bool isWin)
    {

        return GetMissionCashReward(isWin);
    }

    public void LoseGame()
    {
        //Enemy win
        var enemy = GameElement.Instance.enemyControllers;

        foreach (var e in enemy)
        {
            e.GetComponent<EnemyController>().SetState((int)AnimState.VICTORY);
        }

        playerController.SetState((int)AnimState.DEFEAT);
        playerController.DefeatState();

        var hostage = GameElement.Instance.hostageControllers;
        foreach (var h in hostage)
        {
            h.GetComponent<HostageController>().SetState((int)AnimState.DEFEAT);
            h.GetComponent<HostageController>().DisbaleChatBox();
        }
    }

    public void WinGame()
    {
        playerController.SetState((int)AnimState.VICTORY);
        playerController.WinningState();

        var hostage = GameElement.Instance.hostageControllers;
        foreach (var h in hostage)
        {
            h.GetComponent<HostageController>().SetState((int)AnimState.VICTORY);
            h.GetComponent<HostageController>().DisbaleChatBox();
        }
    }

    public void PauseControl(bool isPause)
    {
        if (isPause == false)
            StartCoroutine(Resume(isPause));
        else
        {
            gameState = (int)(isPause ? GameState.RUNNING : GameState.PAUSE);
        }
    }

    IEnumerator Resume(bool state)
    {
        yield return new WaitForSeconds(0.2f);
        gameState = (int)(state?GameState.RUNNING: GameState.PAUSE);
    }

    public bool IsPlayerCanShoot()
    {
        return playerController.canShoot;
    }

    public bool IsPauseGame()
    {
        return gameState != (int)GameState.RUNNING;
    }

    public int bulletRemaining()
    {
        return bulletQuantity;
    }

    public void Reset()
    {
        gameState = (int)GameState.RUNNING;
    }

    public void SetSceneData()
    {
        gameState = (int)GameState.RUNNING;
        allyDeath = false;
        timeChecking = 100;

        var data = gameDataController.GetGameData();
        currentLevel = PlayerPrefs.GetInt("ChoosenLevel", 1);

        // Set bullet
        var level = GetLevelInfo(data, currentLevel);
        bulletQuantity = level.bullet;
        bulletsRemaining.SpawBulletFollowQuantity(bulletQuantity);

        starLevel = 0;

        // Item Effect
        ItemEffectControl(data);

        // Cash
        userCash = data.Cash;
        uIController.SetCash(userCash);
        //uIController.UpdateCash(userCash);

        // Level
        uIController.SetLevelText(PlayerPrefs.GetInt("CurrentProgress"));

        numberOfEnemy = GameElement.Instance.enemyControllers.Length;
        numberOfHostage = GameElement.Instance.hostageControllers.Length;
        baseEnemies = numberOfEnemy;

        Time.timeScale = 1;

        // Revive
        PlayerPrefs.SetInt("ReviveCount", 0);

        // Cash
        PlayerPrefs.SetInt("CashWon", 0);

        // FirseBase
        int stageLog = PlayerPrefs.GetInt("CurrentProgress");
        //MonsterSDK.LogSDK.Scripts.GameAnalyticsV2.LogStageStart(stageLog, (stageLog % 5 == 0) ? "Boss" : "Normal");

        if(AdsInitializer.Instance)
            AdsInitializer.Instance.banner.LoadAd();
    }

    public void ItemEffectControl(GameData data)
    {
        playerController.EquipWeapon(1); // Equip Gun base
        foreach (var i in data.SupportItems)
        {
            if (i.type == (int)ItemType.AMMO)
            {
                if (i.isEquip == 1 && i.quantity >= 1)
                {
                    //bulletQuantity = 7;
                    bulletQuantity += 2;
                    bulletsRemaining.SpawBulletFollowQuantity(bulletQuantity);
                    gameDataController.AddMoreItemByType(i.type, -1);
                }
                else
                {
                    gameDataController.UpdateEquipmentState(i.type, 0);
                }
            }
            else if (i.type == (int)ItemType.RIFLE)
            {
                if (i.isEquip == 1 && i.quantity >= 1)
                {
                    playerController.EquipWeapon((int)ItemType.RIFLE);
                    gameDataController.AddMoreItemByType(i.type, -1);
                }
                else
                {
                    gameDataController.UpdateEquipmentState(i.type, 0);
                }
            }
        }
    }

    #region Handle Load Level
    public void SetNextLevel()
    {
        int continueLevel = CountAllLevel();
        PlayerPrefs.SetInt("CurrentProgress", PlayerPrefs.GetInt("CurrentProgress") + 1);

        if((PlayerPrefs.GetInt("CurrentProgress") >= 51))
        {
            if (PlayerPrefs.GetInt("CurrentProgress") == 51)
                ResetLoaded();
            else if((PlayerPrefs.GetInt("CurrentProgress") - 71) % 20 == 0)
            {
                ResetLoaded();
            }
        }

        if (PlayerPrefs.GetInt("CurrentProgress") <= continueLevel)
        {
            PlayerPrefs.SetInt("ChoosenLevel", PlayerPrefs.GetInt("CurrentProgress"));
        }
        else
        {
            HandleLoadExtraLevel();
        }
        
    }

    string baseNormalLevel = "11,12,13,14,16,17,18,19,21,22,23,24,26,27,28,29,31,32,33,34,36,37,38,39,41,42,43,44,46,47,48,49";
    string baseBossLevel = "15,20,25,30,35,40,45,50";
    private void HandleLoadExtraLevel()
    {
        int currentProgress = PlayerPrefs.GetInt("CurrentProgress", 1);
        string randomLevel = "11";
        if (currentProgress % 5 == 0)
        {
            // Load boss level
            randomLevel = "15";
            string loadedBossLevel = PlayerPrefs.GetString("LoadedBossLevel");
            if (loadedBossLevel == "")
            {
                PlayerPrefs.SetString("LoadedBossLevel", baseBossLevel);
                loadedBossLevel = baseBossLevel;
            }

            string[] levels = loadedBossLevel.Split(',');
            if (levels != null && levels.Length != 0)
            {
                List<string> list = new List<string>();
                foreach (var item in levels)
                {
                    list.Add(item);
                }

                int index = Random.Range(0, list.Count);
                randomLevel = "" + list[index];
                list.RemoveAt(index);
                PlayerPrefs.SetString("LoadedBossLevel", MakeString(list));
            }
        }
        else
        {
            randomLevel = "11";
            string loadedNormalLevel = PlayerPrefs.GetString("LoadedNormalLevel");
            if (loadedNormalLevel == "")
            {
                PlayerPrefs.SetString("LoadedNormalLevel", baseNormalLevel);
                loadedNormalLevel = baseNormalLevel;
            }
            
            string[] levels = loadedNormalLevel.Split(',');
            if (levels != null && levels.Length != 0)
            {
                List<string> list = new List<string>();
                foreach (var item in levels)
                {
                    list.Add(item);
                }

                int index = Random.Range(0, list.Count);
                randomLevel = "" + list[index];
                list.RemoveAt(index);
                PlayerPrefs.SetString("LoadedNormalLevel", MakeString(list));
            }
        }

        //string loadedNormalLevel = PlayerPrefs.GetString("LoadedNormalLevel");
        //string loadedBossLevel = PlayerPrefs.GetString("LoadedBossLevel");
        //PlayerPrefs.SetString("LoadedLevel", loadedLevel + "," + randomLevel);

        PlayerPrefs.SetInt("ChoosenLevel", int.Parse(randomLevel));
    }

    private void ResetLoaded()
    {
        PlayerPrefs.SetString("LoadedNormalLevel", baseNormalLevel);
        PlayerPrefs.SetString("LoadedBossLevel", baseBossLevel);
    }

    private string MakeString(List<string> chain)
    {
        string result = "";
        for(int i = 0; i < chain.Count; i++)
        {
            result += chain[i];
            if (i != chain.Count - 1) result += ",";
        }

        return result;
    }
    #endregion

    private int CountLevelCompleted()
    {
        int number = 0;
        foreach (var item in gameDataController.GetGameData().Levels)
        {
            if (item.isUnlock) number++;
        }

        return number;
    }

    private int CountAllLevel()
    {
        int number = 0;
        foreach (var item in gameDataController.GetGameData().Levels)
        {
            number++;
        }

        return number;
    }

    public Level GetLevelInfo(GameData data, int level)
    {
        foreach (var iter in data.Levels)
        {
            if(iter.level == level)
            {
                return iter;
            }
        }

        return null;
    }

    public int GetCurrentLevel()
    {
        return currentLevel;
    }

    public void SetNoADs(bool isNoADs)
    {
        PlayerPrefs.SetInt("NoADs", isNoADs ? 1 : 0);
    }

    public bool IsNoADs()
    {
        return PlayerPrefs.GetInt("NoADs", 0) == 1;
    }

    public void SetMissionData()
    {
        missionsController.SetMissionInfo(currentLevel);
    }

    public int GetMissionCashReward(bool isWin = false)
    {
        return missionsController.GetReward(numberOfEnemy, bulletQuantity, numberOfHostage, isWin);
    }

    public int GetStars()
    {
        //int star = missionsController.GetStarsResult(numberOfEnemy, bulletQuantity, numberOfHostage);
        int star = 1;
        if(bulletQuantity >= 5 - baseEnemies)
        {
            star = 3;
        }
        else if (bulletQuantity >= 4 - baseEnemies)
        {
            star = 2;
        }
        else
        {
            star = 1;
        }

        return star;
    }
}
