using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdControlInGamePlay : MonoBehaviour
{
    public GameObject ADsItemSuccessPopup;
    public GameObject ADsItemFailPopup;
    public GameObject ShowADsFailPopup;

    // --------------- Interstitial ADs ------------------ //
    public void ShowInterstitialByRatio(int ratio = 2)
    {
        bool isShowADs = Random.Range(0, 3) < ratio;
        if (isShowADs)
        {
            AdsInitializer.Instance.interstitialADs.ShowInterstitial();
        }
    }

    // --------------- Item ------------------ //
    public void WatchADsToGetMoreItems()
    {
        AdsInitializer.Instance.rewardedADs.ShowRewarded(WatchADsToGetMoreItemsSuccess, WatchADsToGetMoreItemsFail);
    }

    private void WatchADsToGetMoreItemsSuccess()
    {
        GameElement.Instance.inGameItemControl.GetRewardItem();
        ADsItemSuccessPopup.SetActive(true);
    }

    private void WatchADsToGetMoreItemsFail()
    {
        ADsItemFailPopup.SetActive(true);
    }

    // --------------- Skip Level ------------------ //
    public void WatchADsToSkipLevel()
    {
        AdsInitializer.Instance.rewardedADs.ShowRewarded(WatchADsToSkipLevelSuccess, WatchADsToSkipLevelFail);
    }

    private void WatchADsToSkipLevelSuccess()
    {
        GameElement.Instance.gameOverCOntroller.SkipLevel();
        GameplayController.Instance.PauseControl(false);
    }

    private void WatchADsToSkipLevelFail()
    {
        ShowADsFailPopup.SetActive(true);
        GameplayController.Instance.PauseControl(false);
    }

    // --------------- XReward ------------------ //
    public void WatchADsToXReward()
    {
        AdsInitializer.Instance.rewardedADs.ShowRewarded(WatchADsToXRewardSuccess, WatchADsToXRewardFail);
    }

    private void WatchADsToXRewardSuccess()
    {
        GameElement.Instance.gameOverCOntroller.ADsCompleted();
    }

    private void WatchADsToXRewardFail()
    {
        ShowADsFailPopup.SetActive(true);
    }
}
