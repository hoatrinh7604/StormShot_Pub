using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySliderController : MonoBehaviour
{
    [SerializeField] Slider healthBar;
    //[SerializeField] float speedHealthBar = 5f;

    private float maxValue;

    public void SetSlider(float maxValue)
    {
        healthBar.maxValue = maxValue;
        healthBar.value = maxValue;
    }

    public void UpdateSlider(float value)
    {
        //healthBar.value = value;
        StartCoroutine(ChangeValue(value));
    }

    IEnumerator ChangeValue(float value)
    {
        while(healthBar.value > value)
        {
            yield return new WaitForSeconds(0.001f);
            healthBar.value -= Time.deltaTime;
        }

        healthBar.value = value;
    }
}
