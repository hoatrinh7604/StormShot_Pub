using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnBaseMode : MonoBehaviour
{
    [SerializeField] PlayerController playerController;

    [SerializeField] BossController bossController;
    [SerializeField] EnemyShootController enemyShootController;
    [SerializeField] bool isPlayerTurn;

    [SerializeField] float bossDelayTime = 2;
    private float time;
    private bool bossInAttacking = false;
    private bool isBlockedBoss = false;
    private void Awake()
    {
        playerController = GameElement.Instance.playerController;
        bossController = FindObjectOfType<BossController>();
        enemyShootController = GameElement.Instance.enemyShootController;
    }

    private void Start()
    {
        //StartGame();
        BossWakeUp();
    }

    private void Update()
    {
        BossAutoAttack();
    }

    public void BossAutoAttack()
    {
        if (GameplayController.Instance.IsPauseGame()) return;
        if (bossInAttacking) return;

        time -= Time.deltaTime;
        if (time < 0 && !isBlockedBoss)
        {
            bossDelayTime = Random.Range(3, 6);
            time = bossDelayTime;
            BossTurn();
        }
    }

    public void PlayerTurn()
    {
        //BlockBoss();
        enemyShootController.IncreaseRatioKill(10);

        //isPlayerTurn = true;
        playerController.PlayerTurn();
    }

    public void BlockPlayer()
    {
        playerController.Idle();
    }

    public void BossTurn()
    {
        //BlockPlayer();

        //isPlayerTurn = false;
        bossInAttacking = true;
        bossController.BossTurn();
    }

    public void BlockBoss(float time = 0)
    {
        isBlockedBoss = true;
        StartCoroutine(BlockBossAfterTime(time));
    }

    IEnumerator BlockBossAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        isBlockedBoss = false;
        bossController.Idle();
        bossInAttacking = false;

    }

    public void ChangeTurn()
    {
        StartCoroutine(Change());
    }

    IEnumerator Change()
    {
        BlockPlayer();
        BlockBoss();
        yield return new WaitForSeconds(1.5f);
        isPlayerTurn = !isPlayerTurn;

        if (isPlayerTurn)
        {
            PlayerTurn();
        }
        else
        {
            BossTurn();
        }
    }

    public void StartGame()
    {
        //if(isEnemyFirst)
        //{
        //    BossTurn();
        //}

        PlayerTurn();
    }

    public void BossWakeUp()
    {
        bossDelayTime = Random.Range(3, 6);
        time = bossDelayTime;
        bossController.Idle();
        bossInAttacking = false;
    }

    public bool IsPlayerTurn()
    {
        return isPlayerTurn;
    }
}
