using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControllerEndgame : MonoBehaviour
{

    public void LoseGame()
    {
        GameElement.Instance.playerController.SetState((int)AnimState.DEFEAT);
        GameElement.Instance.playerController.DefeatState();

        //Enemy win
        var enemy = GameElement.Instance.enemyControllers;

        foreach (var e in enemy)
        {
            var movingControl = e.GetComponent<CharacterMoving>();
            if(movingControl)
            {
                movingControl.Idle();
            }
            e.GetComponent<EnemyController>().SetState((int)AnimState.VICTORY);
        }
    }

    public void WinGame()
    {
        GameElement.Instance.playerController.SetState((int)AnimState.VICTORY);
        GameElement.Instance.playerController.WinningState();

        var hostage = GameElement.Instance.hostageControllers;
        foreach (var h in hostage)
        {
            h.GetComponent<HostageController>().SetState((int)AnimState.VICTORY);
            h.GetComponent<HostageController>().DisbaleChatBox();
        }
    }
}
