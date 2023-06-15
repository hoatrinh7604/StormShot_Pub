using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;
using JsonClass;

public class MainMenuItemController : MonoBehaviour
{
    [SerializeField] GameDataController gameDataController;
    [SerializeField] ItemController itemController;
    [SerializeField] SceneController sceneController;
    [SerializeField] MainMenuController mainMenuController;

    private GameData gameData = new GameData();

    [SerializeField] Button goButton;
    [SerializeField] Button noThanks;

    [SerializeField] Button freeItemButton;
    [SerializeField] GameObject failADsPopUp;
    [SerializeField] GameObject itemRewardPopUp;

    private void Awake()
    {
        mainMenuController = FindObjectOfType<MainMenuController>();
        GetGameData();

        DisplayInfo();

        goButton.onClick.AddListener(delegate { GoGamePlay(); });
        freeItemButton.onClick.AddListener(delegate { WatchADsToGetMoreItems(); });
        noThanks.onClick.AddListener(delegate { ShowADs(); });
    }

    public void GetGameData()
    {
        gameData = gameDataController.GetGameData();
    }

    public void DisplayInfo()
    {
        itemController.SetListItem(gameData.SupportItems);
        itemController.UpdateUserItems();
    }

    public void ShowADs()
    {
        //AdsController.Instance.ShowInterstitial(() => {
        //    // Success
        //}, InterstitialPositionType.Nothanks);
    }

    public void WatchADsToGetMoreItems()
    {
        mainMenuController.ADcontrolInMain.WatchADsToGetItem();
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
        foreach (var item in gameData.Levels)
        {
            if (item.isUnlock) number++;
        }

        return number;
    }

    public void EquipItem(int type, int isEquip)
    {
        gameDataController.UpdateEquipmentState(type, isEquip);
    }

    public void UnEquipWeaponItemExcept(int type)
    {
        itemController.UnEquipWeaponItemExcept(type);
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

            mainMenuController.UpdateUserCash();
            GetGameData();
            callback();
        }

        yield return new WaitForSeconds(0);
    }

    private bool CanPurchase(int price)
    {
        return (gameDataController.GetGameData().Cash >= price);
    }
}
