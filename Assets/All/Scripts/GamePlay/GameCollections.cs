using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultState
{ }
    

public class GameCollections : MonoBehaviour
{
    public int gameStage { get; set; }
    public string gameMode { get; set; }
    public ResultState resultState { get; set; }
    public double playTime { get; set; }
    public string resource { get; set; }
    public string percentComplete { get; set; }
    public int enemyKill { get; set; }
    public int reviveCount { get; set; }
    public string characters { get; set; }
    public int hpCharacter { get; set; }
    public int damageEnemy { get; set; }

    private double timeSystem = 0;

    private void Update()
    {
        timeSystem += Time.deltaTime;
    }

    public void SendGameCollectionsInfo()
    {
        gameStage = PlayerPrefs.GetInt("CurrentProgress") - 1;
        gameMode = (gameStage % 5 == 0) ? "Boss" : "Normal";
        //resultState = (ResultState)GameplayController.Instance.resultState;
        resource = PlayerPrefs.GetInt("CashWon", 0).ToString();
        
        enemyKill = GameplayController.Instance.baseEnemies - GameplayController.Instance.numberOfEnemy;
        percentComplete = ((enemyKill * 100f) / GameplayController.Instance.baseEnemies).ToString("0.00");
        reviveCount = PlayerPrefs.GetInt("ReviveCount");
        playTime = timeSystem;

    }

    /*
     * 
     * public static void LogStageEnd(int stage, string mode, ResultState result,
            double playTime, string resource, string percentComplete, int enemyKill, int reviveCount, string characters,
            int hpCharacter, int damEnemy)
        {
     * 
     */
}
