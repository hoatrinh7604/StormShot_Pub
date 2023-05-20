using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CashADsRewardController : MonoBehaviour
{
    [SerializeField] float speedBar = 5;
    [SerializeField] Slider slider;
    [SerializeField] Text xReward;

    [SerializeField] Vector2 x5;
    [SerializeField] Vector2 x2L;
    [SerializeField] Vector2 x2R;
    bool leftToRight = true;
    bool pause = true;
    // Start is called before the first frame update
    void Start()
    {
        slider.value = 0;   
        slider.maxValue = 1;   
    }

    // Update is called once per frame
    void Update()
    {
        RunSlider();
    }

    public void RunSlider()
    {
        if (pause) return;
        if (leftToRight)
        {
            slider.value += speedBar * Time.deltaTime;
            if(slider.value >= slider.maxValue)
            {
                leftToRight = false;
            }
        }
        else
        {
            slider.value -= speedBar * Time.deltaTime;
            if (slider.value <= 0)
            {
                leftToRight = true;
            }
        }
    }

    public void Run()
    {
        pause = false;
    }

    public int GetX()
    {
        if(slider.value >= x5.x && slider.value <= x5.y)
        {
            return 5;
        }
        else if(slider.value < x2L.x || slider.value > x2R.x)
        {
            return 2;
        }
        return 3;
    }

    public void PauseAndCalReward(float cash)
    {
        pause = true;
        xReward.text = "+" + (cash * (GetX() - 1)).ToString();
    }
}
