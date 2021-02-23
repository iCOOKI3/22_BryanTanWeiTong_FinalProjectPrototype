using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerBarScript : MonoBehaviour
{

    public Slider slider;

    public void SetTimer(int time)
    {
        slider.value = time;
    }

    public void SetMaxTime(int time)
    {
        slider.maxValue = time;
        slider.value = time;
    }

    void Update()
    {

    }
}
