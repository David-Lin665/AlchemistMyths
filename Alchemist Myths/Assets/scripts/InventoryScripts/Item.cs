using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Item", menuName ="Inventory/New Item")]
public class Item : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    [TextArea]
    public string description;
    public int itemheld;
    public GameObject itemPrefab;

    [Header("Weapon status")]
    public bool isWeapon;
    public int atk;
    public int weaponEffect;
    public float attackSpeed;
    
    [Header("Armor status")]
    public bool isArmor;
    public bool isBoots;
    public float def;
    public int armorEffect;
    
}
