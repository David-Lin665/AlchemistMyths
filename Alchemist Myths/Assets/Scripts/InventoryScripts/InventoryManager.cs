using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    static InventoryManager instance;
    public Inventory backpack;
    public GameObject itemGrid;
    public Slot slotprefab;
    public Text description;
    void Awake()
    {
        if(instance != null)
        {
            Destroy(this);
            instance = this;
        }
    }
    public static void CreateNewItem(Item item)
    {
        Slot newItem = Instantiate(instance.slotprefab,instance.itemGrid.transform.position,Quaternion.identity);
        newItem.gameObject.transform.SetParent(instance.itemGrid.transform);
        newItem.slotItem = item;
        newItem.slotImage.sprite = item.icon;
        newItem.slotnum.text = item.itemheld.ToString();
    }
}
