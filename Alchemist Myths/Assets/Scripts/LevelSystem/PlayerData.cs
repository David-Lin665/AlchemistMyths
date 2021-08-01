using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New PlayerData", menuName ="Player/PlayerData")]
public class PlayerData : ScriptableObject
{
    [Header("Stats")]
    public int currentHealth;
    public int maxHealth;
    public int mana;
    public int speed;
    public int attackDmg;
    public int armor;

    [Header("Level")]
    public int currentLevel;
    public int maxLevel;
    public float exptoLevelUP;
    public float currentExp;
    public float levelbuff;

    public void UpdateExp(int expgained)
    {
        currentExp += expgained;
        if(currentExp >= exptoLevelUP)
            LevelUP();
            currentExp -= exptoLevelUP;
    }

    public void LevelUP()
    {
        currentLevel = Mathf.Clamp(currentLevel+1,0,maxLevel);
        maxHealth = (int)(maxHealth*levelbuff);
        currentHealth = maxHealth;
        mana = (int)(mana*levelbuff);
        speed = (int)(speed*levelbuff);
        exptoLevelUP = exptoLevelUP + 
    }
}
