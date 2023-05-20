using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupItemControl : MonoBehaviour
{
    [SerializeField] float timeToHide = 2;
    [SerializeField] Sprite[] spritesIcon;
    [SerializeField] Image icon;

    private void OnEnable()
    {
        StartCoroutine(Hide());
    }

    IEnumerator Hide()
    {
        yield return new WaitForSeconds(timeToHide);
        this.gameObject.SetActive(false);
    }

    public void ShowIcon(int type)
    {
        icon.sprite = spritesIcon[type];
    }
}
