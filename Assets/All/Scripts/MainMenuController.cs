using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class MainMenuController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI cash;
    [SerializeField] GameDataController gameDataController;

    [SerializeField] GameObject failNoADsPopUp;
    [SerializeField] GameObject failADsPopUp;
    [SerializeField] GameObject rewardADsPopUp;
    [SerializeField] Button moreCashBtn;
    [SerializeField] Button noADsBtn;
    [SerializeField] GameObject noADsTick;

    [SerializeField] GameObject itemSelection;

    [SerializeField] Button tapToPlayBtn;

    public ADcontrolInMain ADcontrolInMain;

    // Start is called before the first frame update
    void Start()
    {
        //noADsBtn.interactable = !SDKPlayerPrefs.GetBoolean(StringConstants.REMOVE_ADS, false);
        //noADsTick.SetActive(SDKPlayerPrefs.GetBoolean(StringConstants.REMOVE_ADS, false));

        cash.text = gameDataController.GetGameData().Cash.ToString();

        moreCashBtn.onClick.AddListener(WatchADs);

        tapToPlayBtn.onClick.AddListener(TapToPlay);
    }

    public void UpdateUserCash()
    {
        cash.text = gameDataController.GetGameData().Cash.ToString();
    }

    public void WatchADsToGetMoreCoins()
    {
        ADcontrolInMain.WatchADsToGet500Cash();
    }

    public void WatchADs()
    {
        WatchADsToGetMoreCoins();
    }

    public void NoADsComplete()
    {
        //SDKPlayerPrefs.SetBoolean(StringConstants.REMOVE_ADS, true);
        //noADsBtn.interactable = !SDKPlayerPrefs.GetBoolean(StringConstants.REMOVE_ADS, false);
        //noADsTick.SetActive(SDKPlayerPrefs.GetBoolean(StringConstants.REMOVE_ADS, false));
    }

    bool canPlayNow = false;
    public void TapToPlay()
    {
        if (canPlayNow)
        {
            SceneManager.LoadSceneAsync("Level " + PlayerPrefs.GetInt("ChoosenLevel", 1));
        }
        else if (gameDataController.GetGameData().Cash >= 500)
        {
            itemSelection.SetActive(true);
            StartCoroutine(CanPlay());
        }
        else
        {
            SceneManager.LoadSceneAsync("Level " + PlayerPrefs.GetInt("ChoosenLevel", 1));
        }
    }

    IEnumerator CanPlay()
    {
        canPlayNow = false;
        yield return new WaitForSeconds(2);
        canPlayNow = true;
    }


    public void FailNoADs()
    {
        failNoADsPopUp.SetActive(true);
    }

    public void GetReward()
    {
        rewardADsPopUp.SetActive(true);
        gameDataController.AddMoreCash(500);
        cash.text = gameDataController.GetGameData().Cash.ToString();
    }
}
