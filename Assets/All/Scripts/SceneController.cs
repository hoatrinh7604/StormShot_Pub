using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("GamePlay");
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    
    public void LoadSettings()
    {
        SceneManager.LoadScene("Setting");
    }
    
    public void LoadHighscore()
    {
        SceneManager.LoadScene("Highscore");
    }

    public void LoadGamePlayScene()
    {
        SceneManager.LoadSceneAsync("GamePlayScene");
    }

    public void LoadLevelScene(int level)
    {
        SceneManager.LoadSceneAsync("Level " + level);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
