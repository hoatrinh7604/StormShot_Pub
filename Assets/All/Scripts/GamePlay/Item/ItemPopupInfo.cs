using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPopupInfo : MonoBehaviour
{
    [SerializeField] float timeToHide = 2;

    public void ShowInfo()
    {
        this.gameObject.SetActive(true);
        StartCoroutine(Hide());
    }

    IEnumerator Hide()
    {
        yield return new WaitForSeconds(timeToHide);
        this.gameObject.SetActive(false);
    }
}
