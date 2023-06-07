using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostageController : CharacterController
{
    [SerializeField] GameObject chatBox;

    override public void PlayWakeSound()
    {
        if (SoundController.Instance == null) return;
        SoundController.Instance.PlayAudio(SoundController.Instance.hostageSmile, PlayerPrefs.GetFloat("EffectSound", 1), false);
    }

    public void DisbaleChatBox(bool isShow = false)
    {
        chatBox.SetActive(isShow);
    }

    override public void Death(Collision collision)
    {
        if (isDeath) return;
        base.Death(collision);
        chatBox.SetActive(false);

        GameplayController.Instance.HostageDeath();

        if (SoundController.Instance == null) return;
        SoundController.Instance.PlayAudio(SoundController.Instance.hostageDeath, PlayerPrefs.GetFloat("EffectSound", 1), false);
    }

    override public void Death(Vector3 position)
    {
        if (isDeath) return;
        base.Death(position);
        chatBox.SetActive(false);

        GameplayController.Instance.HostageDeath();

        if (SoundController.Instance == null) return;
        SoundController.Instance.PlayAudio(SoundController.Instance.hostageDeath, PlayerPrefs.GetFloat("EffectSound", 1), false);
    }
}
