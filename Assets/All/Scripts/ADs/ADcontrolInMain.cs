using System.Collections;
using System.Collections.Generic;
using Unity.Services.Mediation.Samples;
using UnityEngine;
using TMPro;

public class ADcontrolInMain : MonoBehaviour
{
    public GameObject ADs500CashSuccessPopup;
    public GameObject ADs500CashFailPopup;

    public GameObject ADsItemSuccessPopup;
    public GameObject ADsItemFailPopup;

    public MainMenuItemController mainMenuItemController;
    public MainMenuController mainMenuController;

    public void NoADs()
    {

    }

    // --------------- 500 cash ------------------ //
    public void WatchADsToGet500Cash()
    {
        AdsInitializer.Instance.rewardedADs.ShowRewarded(WatchADsToGet500CashSuccess, WatchADsToGet500CashFail);
    }

    private void WatchADsToGet500CashSuccess()
    {
        mainMenuController.GetReward();
        ADs500CashSuccessPopup.SetActive(true);
    }

    private void WatchADsToGet500CashFail()
    {
        ADs500CashFailPopup.SetActive(true);
    }

    // --------------- Item ------------------ //
    public void WatchADsToGetItem()
    {
        AdsInitializer.Instance.rewardedADs.ShowRewarded(WatchADsToGetItemSuccess, WatchADsToGetItemFail);
    }

    private void WatchADsToGetItemSuccess()
    {
        mainMenuItemController.GetRewardItem();
        ADsItemSuccessPopup.SetActive(true);
    }

    private void WatchADsToGetItemFail()
    {
        ADsItemFailPopup.SetActive(true);
    }
}
