using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    public void SetMaxPoint(int point)
    {
        slider.maxValue = point;
        slider.value = point;
         fill.color= gradient.Evaluate(1f);
    }
    public void SetPoint(int point)
    {
        slider.value = point;
        fill.color = gradient.Evaluate(slider.normalizedValue);

    }
}
