using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using JsonClass;
public class MapLevelSceneController : MonoBehaviour
{
    [SerializeField] CashShowingController cashShowingController;
    [SerializeField] GameDataController gameDataController;
    [SerializeField] ItemController itemController;
    [SerializeField] SceneController sceneController;

    [SerializeField] LevelInfoController levelInfoController;

    private GameData gameData = new GameData();
    private int userCash;

    [SerializeField] Button goButton;
    [SerializeField] Button startNowButton;

    [SerializeField] Button freeItemButton;
    [SerializeField] GameObject failADsPopUp;
    [SerializeField] GameObject itemRewardPopUp;

    private void Awake()
    {
        cashShowingController = FindObjectOfType<CashShowingController>();
        levelInfoController = FindObjectOfType<LevelInfoController>();
        GetGameData();

        DisplayInfo();

        goButton.onClick.AddListener(delegate { GoGamePlay(); });
        startNowButton.onClick.AddListener(delegate { StartNow(); });
        freeItemButton.onClick.AddListener(delegate { WatchADsToGetMoreItems(); });
    }

    public void UpdateCashShowing()
    {
        cashShowingController.UpdateCash(userCash);
    }

    public void GetGameData()
    {
        gameData = gameDataController.GetGameData();

        userCash = gameData.Cash;
    }

    public void DisplayInfo()
    {
        UpdateCashShowing();
        UpdateAllLevel();

        itemController.SetListItem(gameData.SupportItems);
        itemController.UpdateUserItems();
    }

    public void UpdateAllLevel()
    {
        levelInfoController.SetAllLevelInfo(gameData.Levels);
    }

    public void AddMoreCash(int amount)
    {
        gameDataController.AddMoreCash(amount);
        userCash += amount;

        UpdateCashShowing();
    }

    public void ShowADs()
    {
        //AdsController.Instance.ShowInterstitial(() => {
            // Xử lý sau khi đóng inter thành công
        //}, InterstitialPositionType.Close);
    }

    public void WatchADsToGetMoreItems()
    {
        
    }

    public void GetRewardItem()
    {
        int randomType = UnityEngine.Random.Range(0, Enum.GetValues(typeof(ItemType)).Length) + 1;
        itemRewardPopUp.SetActive(true);
        itemRewardPopUp.GetComponent<PopupItemControl>().ShowIcon(randomType);

        gameDataController.AddMoreItemByType(randomType, 1);
        ReloadItemData();
    }

    public void ReloadItemData()
    {
        itemController.ReloadData(gameDataController.GetGameData().SupportItems);
    }

    public void GoGamePlay()
    {
        
        itemController.GoGamePlay();

        int playLevel = PlayerPrefs.GetInt("ChoosenLevel", 1);
        sceneController.LoadLevelScene(playLevel);
    }

    public void StartNow()
    {
        int continueLevel = CountLevelCompleted();
        PlayerPrefs.SetInt("ChoosenLevel", continueLevel);
        sceneController.LoadLevelScene(continueLevel);
    }

    private int CountLevelCompleted()
    {
        int number = 0;
        foreach(var item in gameData.Levels)
        {
            if (item.isUnlock) number++;
        }

        return number;
    }

    public void EquipItem(int type, int isEquip)
    {
        gameDataController.UpdateEquipmentState(type, isEquip);
    }

    public void AddItem(System.Action callback, int type, int price)
    {
        StartCoroutine(AddMoreItem(callback, type, price));
    }
    IEnumerator AddMoreItem(System.Action callback, int type, int price)
    {
        if (CanPurchase(price))
        {

            // Add item to data
            gameDataController.AddMoreItemByType(type, 1);
            gameDataController.AddMoreCash(-price);

            userCash -= price;
            UpdateCashShowing();

            callback();
        }

        yield return new WaitForSeconds(0);
    }

    private bool CanPurchase(int price)
    {
        return (userCash >= price);
    }
}
