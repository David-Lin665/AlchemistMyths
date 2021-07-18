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
            Destroy(this);
        instance = this;
    }
    private void OnEnable() 
    {
        RefreshItem();
        instance.description.text = "";
    }
    public static void UpdateItemInfo(string iteminfo)
    {
        instance.description.text = iteminfo;
    }
    public static void CreateNewItem(Item item)
    {
        Slot newItem = Instantiate(instance.slotprefab,instance.itemGrid.transform.position,Quaternion.identity);
        newItem.gameObject.transform.SetParent(instance.itemGrid.transform);
        newItem.slotItem = item;
        newItem.slotImage.sprite = item.icon;
        newItem.slotnum.text = item.itemheld.ToString();
    }
    public static void RefreshItem()
    {
        for(int i = 0; i<instance.itemGrid.transform.childCount;i++)
        {
            if(instance.itemGrid.transform.childCount == 0)
                break;
            Destroy(instance.itemGrid.transform.GetChild(i).gameObject);
        }
        for(int i = 0; i<instance.backpack.itemList.Count; i++)
        {
            CreateNewItem(instance.backpack.itemList[i]);
        }
    }
}
