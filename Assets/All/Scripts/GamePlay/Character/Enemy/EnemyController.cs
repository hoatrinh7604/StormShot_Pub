using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : CharacterController
{
    override public void PlayWakeSound()
    {
        if (SoundController.Instance == null) return;
        SoundController.Instance.PlayAudio(SoundController.Instance.enemySmile, PlayerPrefs.GetFloat("EffectSound", 1), false);
    }

    override public void Death(Collision collision)
    {
        if (isDeath) return;
        base.Death(collision);

        GameplayController.Instance.EnemyDeath();

        if (SoundController.Instance == null) return;
        SoundController.Instance.PlayAudio(SoundController.Instance.enemyDeath, PlayerPrefs.GetFloat("EffectSound", 1), false);
        SoundController.Instance.StopAudio(SoundController.Instance.enemySmile);

        if(GetComponent<WeaponEnemy>())
        {
            GetComponent<WeaponEnemy>().Unequip();
        }
    }

    override public void Death(Vector3 position)
    {
        if (isDeath) return;
        base.Death(position);

        GameplayController.Instance.EnemyDeath();

        if (SoundController.Instance == null) return;
        SoundController.Instance.PlayAudio(SoundController.Instance.enemyDeath, PlayerPrefs.GetFloat("EffectSound", 1), false);
        SoundController.Instance.StopAudio(SoundController.Instance.enemySmile);

        if (GetComponent<WeaponEnemy>())
        {
            GetComponent<WeaponEnemy>().Unequip();
        }
    }
}
