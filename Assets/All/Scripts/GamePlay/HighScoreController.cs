using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreController : MonoBehaviour
{
    [SerializeField] Image[] imageStars;
    [SerializeField] float darkColor;

    [SerializeField] int stars;
    public void SetStars(int numOfStars)
    {
        for(int i = 0; i < imageStars.Length; i++)
        {
            SetDark(i);
            if (i <=  numOfStars-1)
            {
                SetLight(i);
            }
        }
    }

    private void SetDark(int index)
    {
        var color = imageStars[index].color;
        imageStars[index].color = new Color(color.r, color.g, color.b, darkColor);
    }

    private void SetLight(int index)
    {
        var color = imageStars[index].color;
        imageStars[index].color = new Color(color.r, color.g, color.b, 1);
    }
}
