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
    public int WeaponEffect;
    public float attackSpeed;
    public string Weapontag;
    
    [Header("Armor status")]
    public bool isArmor;
    public float def;
    public int ArmorEffect;
    public string Armortag;
}
