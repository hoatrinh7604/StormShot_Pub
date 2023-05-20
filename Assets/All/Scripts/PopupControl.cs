using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupControl : MonoBehaviour
{
    [SerializeField] float timeToHide = 2;

    private void OnEnable()
    {
        StartCoroutine(Hide());
    }

    IEnumerator Hide()
    {
        yield return new WaitForSeconds(timeToHide);
        this.gameObject.SetActive(false);
    }
}
