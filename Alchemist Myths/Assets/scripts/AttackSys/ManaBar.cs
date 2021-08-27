using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{
    public Text currentManaText;
    public Text maxManaText;
    public Slider slider;
    public void SetMaxMana(int Mana){
        slider.maxValue = Mana;
        maxManaText.text = Mana.ToString();
    }
    public void SetMana(int Mana)
    {
        slider.value = Mana;
        currentManaText.text = Mana.ToString();
    }

}

