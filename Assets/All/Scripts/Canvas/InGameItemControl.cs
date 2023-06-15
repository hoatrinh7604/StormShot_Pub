using System.Collections;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using JsonClass;
public class InGameItemControl : MonoBehaviour
{
    [SerializeField] GameDataController gameDataController;
    [SerializeField] ItemController itemController;
    [SerializeField] SceneController sceneController;
    [SerializeField] UIController uIController;

    private GameData gameData = new GameData();

    [SerializeField] Button goButton;
    [SerializeField] Button noThanks;

    [SerializeField] Button freeItemButton;
    [SerializeField] GameObject failADsPopUp;
    [SerializeField] GameObject itemRewardPopUp;

    private void Awake()
    {
        gameDataController = GameElement.Instance.gameDataController;
        uIController = GameElement.Instance.uIController;
        GetGameData();

        DisplayInfo();

        goButton.onClick.AddListener(delegate { GoGamePlay(); });
        //freeItemButton.onClick.AddListener(delegate { WatchADsToGetMoreItems(); });
        noThanks.onClick.AddListener(delegate { NoThanks(); });
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
        GameElement.Instance.AdControlInGamePlay.WatchADsToGetMoreItems();
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
        uIController.SendGameCollectionsInfo();
        SceneManager.LoadSceneAsync("Level " + PlayerPrefs.GetInt("ChoosenLevel", 1));
    }

    public void NoThanks()
    {
        uIController.SendGameCollectionsInfo();
        SceneManager.LoadSceneAsync("Level " + PlayerPrefs.GetInt("ChoosenLevel", 1));
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

            uIController.ReduceCash(-price);
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
