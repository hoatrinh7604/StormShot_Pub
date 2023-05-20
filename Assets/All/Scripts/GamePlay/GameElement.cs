using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameElement : MonoBehaviour
{
    public static GameElement Instance { get; private set; }

    private void Awake()
    {
        SetElements();
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public PlayerController playerController;
    public AdControlInGamePlay AdControlInGamePlay;
    public InGameItemControl inGameItemControl;

    public UIController uIController;
    public GameOverCOntroller gameOverCOntroller;
    public CashShowingController cashShowingController;
    public HighScoreController highScoreController;

    public CrosshairMovement crosshairMovement;

    public GameDataController gameDataController;
    public MissionsController missionsController;
    public PauseController pauseController;

    public TurnBaseMode turnBaseMode;
    public EnemyShootController enemyShootController;
    public EnemyCrosshairController enemyCrosshairController;

    public Shooting shooting;

    public EnemyController[] enemyControllers;
    public HostageController[] hostageControllers;

    public void SetElements()
    {
        enemyControllers = FindObjectsOfType<EnemyController>();
        hostageControllers = FindObjectsOfType<HostageController>();
    }
}
