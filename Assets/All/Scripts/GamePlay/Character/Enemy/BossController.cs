using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : EnemyController
{
    [SerializeField] EnemyShootController enemyShootController;

    override public void Awake()
    {
        base.Awake();
        enemyShootController = GetComponent<EnemyShootController>();
    }

    override public void OnColliderWithBullet(Collision collision)
    {
        if (isDeath) return;
        if (collision.gameObject.name == GameContracts.EXPLOSION_NAME)
        {
            StartCoroutine(DisableAfterTime(0));
            BreakCharacter(collision);
        }
        else
        {
            OnColliderHandle(collision);
            Hitted();
        }
        
    }

    override public void OnColliderWithBarrier(Collision collision)
    {
        if (isDeath) return;

        // Check pos
        if (collision.gameObject.name == GameContracts.SAW_NAME)
        {
            StartCoroutine(DisableAfterTime(0));
            BrokenCharacter(collision);
        }
        else
        {
            if (transform.position.y >= collision.transform.position.y) return;
            if (collision.gameObject.name == GameContracts.TNT_NAME) return;
            OnColliderHandle(collision);
            Hitted();
        }
    }

    override public void PlayWakeSound()
    {
        if (SoundController.Instance == null) return;
        SoundController.Instance.PlayAudio(SoundController.Instance.bossSmile, PlayerPrefs.GetFloat("EffectSound", 1), false);
    }

    override public void Death(Collision collision)
    {
        if (isDeath) return;
        base.Death(collision);

        if (SoundController.Instance == null) return;
        SoundController.Instance.StopAudio(SoundController.Instance.bossSmile);
    }

    public void Hitted()
    {
        //GameElement.Instance.turnBaseMode.BlockBoss(1);
        enemyShootController.ReworkBone();
    }

    public void Idle()
    {
        if(!GameplayController.Instance.isWinGame)
            enemyShootController.ReturnIdle();
    }

    public void BossTurn()
    {
        enemyShootController.StartAiming();
    }

    override public void SetState(int state)
    {
        SetAnim(state);

        if(state == (int)AnimState.VICTORY)
            Rotate();
    }

    private void Rotate()
    {
        GetComponent<CharacterRotation>().Rotate();
    }
}
