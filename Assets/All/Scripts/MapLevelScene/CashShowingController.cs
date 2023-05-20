using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CashShowingController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI cashText;
    [SerializeField] Text cashAddedText;
    int cash;

    public void SetCash(int cash)
    {
        this.cash = cash;
        cashText.text = cash.ToString();
    }

    public void UpdateCash(int amount)
    {
        StartCoroutine(UpdateCashByTime(amount));
    }

    public void ReduceCash(int amount)
    {
        StartCoroutine(ReduceCashByTime(amount));
    }

    IEnumerator UpdateCashByTime(int amount)
    {
        while(amount > 0)
        {
            yield return new WaitForSeconds(0.01f);
            cash++;
            amount--;
            cashText.text = cash.ToString();
        }
    }

    IEnumerator ReduceCashByTime(int amount)
    {
        while (amount < 0)
        {
            yield return new WaitForSeconds(0.001f);
            cash--;
            amount++;
            cashText.text = cash.ToString();
        }
    }

    public void AddedCash(int amount)
    {
        cashAddedText.gameObject.SetActive(true);
        cashAddedText.text = "+" + amount;
    }
}
