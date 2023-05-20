using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverCOntroller : MonoBehaviour
{
    [SerializeField] Button XReward;
    [SerializeField] Button continueButton;
    [SerializeField] Button tryAgainButton;
    [SerializeField] Button reloadButton;
    [SerializeField] Button nextLevelButton;
    [SerializeField] Button mainButton;
    [SerializeField] Button replayButton;
    [SerializeField] Button skipButton;
    [SerializeField] GameObject sliderScale;
    [SerializeField] GameObject starGroup;
    [SerializeField] TextMeshProUGUI replayText;
    [SerializeField] TextMeshProUGUI title;
    [SerializeField] TextMeshProUGUI description;

    [SerializeField] GameObject failADsPopUp;
    private bool isWin;

    private void Awake()
    {
        continueButton.onClick.AddListener(Continue);
        tryAgainButton.onClick.AddListener(TryAgain);
        replayButton.onClick.AddListener(Replay);
        mainButton.onClick.AddListener(MainMenu);
        XReward.onClick.AddListener(WatchADs);
        reloadButton.onClick.AddListener(Reload);
        nextLevelButton.onClick.AddListener(NextLevel);
        skipButton.onClick.AddListener(NextLevel);
    }
    public void SetInfo(int typeOfEnd, int star = 0)
    {
        EndGame(typeOfEnd, star);
    }

    public void WinGame(int star)
    {
        WinGameHandle(star);
        GameElement.Instance.AdControlInGamePlay.ShowInterstitialByRatio();

    }



    public void WinGameHandle(int star)
    {
        title.text = "VICTORY";
        switch (star)
        {
            case 1: description.text = "Still potential for growth"; break;
            case 2: description.text = "Just a little bit more"; break;
            case 3: description.text = "Talented Marksmanship"; break;
        }

        HideAllButton();
        //sliderScale.SetActive(true);
        //sliderScale.GetComponent<CashADsRewardController>().Run();
        XReward.gameObject.SetActive(true);
        //replayButton.gameObject.SetActive(true);
        continueButton.gameObject.SetActive(true);
    }

    public void EndGame(int typeOfEnd, int star = 1)
    {
        switch(typeOfEnd)
        {
            case (int)TypeOfEnd.WIN: WinGame(star); break;
            case (int)TypeOfEnd.OUT_OF_BULLET: OutOfBullet(); break;
            case (int)TypeOfEnd.YOU_DEAD: PlayerDeath(); break;
        }
    }

    public void OutOfBullet()
    {
        GameElement.Instance.AdControlInGamePlay.ShowInterstitialByRatio(3);

        title.text = "Wasted !";
        description.text = "Out of Bullet";

        HideAllButton();
        reloadButton.gameObject.SetActive(true);
        tryAgainButton.gameObject.SetActive(true);
        skipButton.gameObject.SetActive(true);
    }

    public void PlayerDeath()
    {
        GameElement.Instance.AdControlInGamePlay.ShowInterstitialByRatio(3);

        title.text = "Wasted !";
        description.text = "You Dead";
        starGroup.SetActive(false);
        HideAllButton();
        tryAgainButton.gameObject.SetActive(true);
        skipButton.gameObject.SetActive(true);
    }

    public void HostageDeath()
    {
        title.text = "Wasted !";
        description.text = "Your Girl Dead";

        HideAllButton();
        tryAgainButton.gameObject.SetActive(true);
        skipButton.gameObject.SetActive(true);
    }

    private void HideAllButton()
    {
        sliderScale.SetActive(false);
        XReward.gameObject.SetActive(false);
        continueButton.gameObject.SetActive(false);
        tryAgainButton.gameObject.SetActive(false);
        replayButton.gameObject.SetActive(false);
        reloadButton.gameObject.SetActive(false);
        nextLevelButton.gameObject.SetActive(false);
        skipButton.gameObject.SetActive(false);

        mainButton.gameObject.SetActive(false);
    }

    public void TryAgain()
    {
        GameElement.Instance.uIController.ShowMenuItem();

        //AdsController.Instance.ShowInterstitial(() => {
        //    // Xử lý sau khi đóng inter thành công
        //    //SceneManager.LoadSceneAsync("Level " + PlayerPrefs.GetInt("ChoosenLevel", 1));
        //    GameElement.Instance.uIController.ShowMenuItem();
        //}, InterstitialPositionType.TryAgain);

        //AdsController.Instance.ShowBanner(true, BannerShowMode.BOT_ONLY, BannerPositionType.Gameplay);
    }

    public void Replay()
    {
        GameplayController.Instance.StayThisLevel();
        SendGameCollectionData();
        SceneManager.LoadSceneAsync("Level " + PlayerPrefs.GetInt("ChoosenLevel", 1));

    }

    public void Continue()
    {
        SendGameCollectionData();
        SceneManager.LoadSceneAsync("Level " + PlayerPrefs.GetInt("ChoosenLevel", 1));

    }

    public void NextLevel()
    {
        GameElement.Instance.AdControlInGamePlay.WatchADsToSkipLevel();
    }

    public void SkipLevel()
    {
        GameplayController.Instance.SkipLevel();
        SendGameCollectionData();
        SceneManager.LoadSceneAsync("Level " + PlayerPrefs.GetInt("ChoosenLevel", 1));
    }

    public void MainMenu()
    {
        SceneManager.LoadSceneAsync("MainMenuScene");

        //if (GameplayController.Instance.IsNoADs())
        //{
        //    SceneManager.LoadSceneAsync("MainMenuScene");
        //    return;
        //}

        //AdsController.Instance.ShowInterstitial(() => {
        //    // Xử lý sau khi đóng inter thành công
        //    //SceneManager.LoadSceneAsync("Level " + PlayerPrefs.GetInt("ChoosenLevel", 1));
        //    SceneManager.LoadSceneAsync("MainMenuScene");
        //}, InterstitialPositionType.Home);

        //AdsController.Instance.ShowBanner(true, BannerShowMode.BOT_ONLY, BannerPositionType.Gameplay);
    }

    public void WatchADs()
    {
        GameElement.Instance.AdControlInGamePlay.WatchADsToXReward();
    }

    public void ADsCompleted()
    {
        //sliderScale.GetComponent<CashADsRewardController>().PauseAndCalReward(GameplayController.Instance.GetCashReward(true));
        int scale = 2;
        GameplayController.Instance.CompletedLevel(scale);
        XReward.gameObject.SetActive(false);
    }

    public void WatchADsTrigger()
    {
        //GameplayController.Instance.PauseControl(true);
        //GameplayController.Instance.PauseControl(false);
    }

    public void Reload()
    {
        GameElement.Instance.AdControlInGamePlay.ShowInterstitialByRatio(3);
        GameplayController.Instance.ContinueGameAfterWatchADs();
        
    }

    public void SendGameCollectionData()
    {
        GetComponent<GameCollections>().SendGameCollectionsInfo();
    }
}
