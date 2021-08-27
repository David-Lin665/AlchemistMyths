using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Text currentHealthText;
    public Text maxHealthText;
    public Slider slider;
    public void SetMaxHealth(int Health){
        slider.maxValue = Health;
        maxHealthText.text = Health.ToString();
    }
    public void SetHealth(int Health)
    {
        slider.value = Health;
        currentHealthText.text = Health.ToString();
    }
}
