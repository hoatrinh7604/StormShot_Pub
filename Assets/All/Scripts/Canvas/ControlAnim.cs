using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlAnim : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] string animName;
    [SerializeField] string animDefault;

    GameDataController gameDataController;
    private void Awake()
    {
        gameDataController = FindObjectOfType<GameDataController>();
    }
    private void Update()
    {
        if(gameDataController.GetGameData().Cash >= 500)
        {
            PlayAnim(animName);
        }
        else
        {
            PlayAnim(animDefault);
        }
    }

    public void PlayAnim(string animName)
    {
        anim.Play(animName);
    }
}
