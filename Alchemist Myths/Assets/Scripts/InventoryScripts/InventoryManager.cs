using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    static InventoryManager instance;
    public Inventory backpack;
    public GameObject slotGrid;
    public GameObject emptyslot;
    public Text description;
    public List<GameObject> slots = new List<GameObject>();
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
    /*public static void CreateNewItem(Item item)
    {
        Slot newItem = Instantiate(instance.slotprefab,instance.itemGrid.transform.position,Quaternion.identity);
        newItem.gameObject.transform.SetParent(instance.itemGrid.transform);
        newItem.slotItem = item;
        newItem.slotImage.sprite = item.icon;
        newItem.slotnum.text = item.itemheld.ToString();
    }*/
    public static void RefreshItem()
    {
        for(int i = 0; i<instance.slotGrid.transform.childCount;i++)
        {
            Destroy(instance.slotGrid.transform.GetChild(i).gameObject);
            instance.slots.Clear();
        }
        for(int i = 0; i<instance.backpack.itemList.Count; i++)
        {
            instance.slots.Add(Instantiate(instance.emptyslot));
            instance.slots[i].transform.SetParent(instance.slotGrid.transform);
            instance.slots[i].GetComponent<Slot>().slotID = i;
            instance.slots[i].GetComponent<Slot>().SetSlot(instance.backpack.itemList[i]);
        }
    }
}
