using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField] GameObject gameOverPopup;
    [SerializeField] HighScoreController highScoreController;
    [SerializeField] CashShowingController cashShowingController;

    [SerializeField] Button settingsButton;
    [SerializeField] GameObject settingsPopup;
    [SerializeField] Button closeSettingsPopup;

    [SerializeField] Button skipButton;

    [SerializeField] Button backMenuButton;

    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] GameOverCOntroller gameOverCOntroller;
    PauseController pauseController;

    [SerializeField] InGameItemControl inGameItemControl;

    [SerializeField] GameObject failADsPopUp;
    int UILayer;
    private void Awake()
    {
        cashShowingController = GameElement.Instance.cashShowingController;
        pauseController = GameElement.Instance.pauseController;
        gameOverCOntroller = GameElement.Instance.gameOverCOntroller;
        gameOverPopup.SetActive(false);

        settingsButton.onClick.AddListener(delegate { OpenSetting(true); });
        skipButton.onClick.AddListener(delegate { SkipLevel(); });
        closeSettingsPopup.onClick.AddListener(delegate { OpenSetting(false); });
    }

    private void Start()
    {
        
    }

    public void ShowGameOver(bool isShow, int typeOfEnd, int star = 1)
    {
        gameOverPopup.SetActive(isShow);
        gameOverCOntroller.SetInfo(typeOfEnd, star);
    }

    public void HideGameOver()
    {
        gameOverPopup.SetActive(false);
    }

    public void ShowStars(int stars)
    {
        highScoreController.SetStars(stars);
    }

    public void UpdateCash(int amount)
    {
        cashShowingController.UpdateCash(amount);
    }

    public void ReduceCash(int amount)
    {
        cashShowingController.ReduceCash(amount);
    }

    public void SetCash(int cash)
    {
        cashShowingController.SetCash(cash);
    }

    public void AddedCash(int amount)
    {
        cashShowingController.AddedCash(amount);
    }

    public void SkipLevel()
    {
        WatchADsToSkipLevel();
    }

    public void WatchADsToSkipLevel()
    {
        GameplayController.Instance.PauseControl(true);
        GameElement.Instance.gameOverCOntroller.SkipLevel();
    }

    public void SkipThisLevel()
    {
        GameplayController.Instance.SkipLevel();
        SendGameCollectionsInfo();
        SceneManager.LoadSceneAsync("Level " + PlayerPrefs.GetInt("ChoosenLevel", 1));
    }

    public void OpenSetting(bool isOpen)
    {
        if(isOpen)
        {
            settingsPopup.SetActive(true);
            pauseController.Pause(true);
        }
        else
        {
            settingsPopup.SetActive(false);
            pauseController.Pause(false);
        }
    }

    public void SetLevelText(int level)
    {
        levelText.text = level.ToString();
    }

    //Returns 'true' if we touched or hovering on Unity UI element.
    public bool IsPointerOverUIElement()
    {
        return IsPointerOverUIElement(GetEventSystemRaycastResults());
    }


    //Returns 'true' if we touched or hovering on Unity UI element.
    private bool IsPointerOverUIElement(List<RaycastResult> eventSystemRaysastResults)
    {
        for (int index = 0; index < eventSystemRaysastResults.Count; index++)
        {
            RaycastResult curRaysastResult = eventSystemRaysastResults[index];
            if (curRaysastResult.gameObject.layer == UILayer)
            {
                return true;
            }
        }
        return false;
    }


    //Gets all event system raycast results of current mouse or touch position.
    public static List<RaycastResult> GetEventSystemRaycastResults()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        List<RaycastResult> raysastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raysastResults);
        return raysastResults;
    }

    public void WatchADsTrigger()
    {
        GameplayController.Instance.PauseControl(true);
        //AdsController.Instance.ShowRewardedVideo(() =>
        //{
        //    // Cant show ADs
        //    //failADsPopUp.SetActive(true);
        //},
        //(gotRewarded) =>
        //{
        //    if (gotRewarded)
        //    {
        //        // Got reward
        //    }
        //    else
        //    {
        //        // can show ADs but the user dont have condition to get reward!
        //        //failADsPopUp.SetActive(true);
        //    }
        //}, RewardedPositionType.FreeItem);

        //AdsController.Instance.ShowBanner(true, BannerShowMode.BOT_ONLY, BannerPositionType.Menu);
        GameplayController.Instance.PauseControl(false);
    }

    public void ShowMenuItem()
    {
        if (GameElement.Instance.gameDataController.GetGameData().Cash >= 500)
        {
            inGameItemControl.gameObject.SetActive(true);
        }
        else
        {
            SceneManager.LoadSceneAsync("Level " + PlayerPrefs.GetInt("ChoosenLevel", 1));
        }
    }

    public void SendGameCollectionsInfo()
    {
        gameOverCOntroller.SendGameCollectionData();
    }

}
