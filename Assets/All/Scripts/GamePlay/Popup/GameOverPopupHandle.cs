using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverPopupHandle : MonoBehaviour
{
    Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        anim.Play("GameOverEnable");
    }

    private void OnDisable()
    {
        anim.Play("GameOverDisable");
    }
}
