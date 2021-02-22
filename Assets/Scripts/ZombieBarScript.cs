using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZombieBarScript : MonoBehaviour
{
    public Slider slider;

    public void SetZCount(int zCount)
    {
        slider.value = zCount;
    }

    public void SetMaxZCount(int zCount)
    {
        slider.maxValue = zCount;
        slider.value = zCount;
    }

    public void SetMinZCount(int zCount)
    {
        slider.minValue = zCount;
        slider.value = zCount;
    }
}
