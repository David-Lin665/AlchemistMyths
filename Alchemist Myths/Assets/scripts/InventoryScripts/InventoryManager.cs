using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    static InventoryManager instance;
    public Inventory backpack;
    public Inventory playerEquipment;
    public GameObject ArmorSlot;
    public GameObject slotGrid;
    public GameObject emptyslot;
    public Text description;
    public List<GameObject> slots = new List<GameObject>();
    void Awake()
    {
        //生成singleton 這段可忽略
        if(instance != null)
            Destroy(this);
        instance = this;
    }
    private void OnEnable() 
    {
        // 當打開背包時初始化
        RefreshItem();
        instance.description.text = "";
    }
    // 用來傳輸物品資料
    public static void UpdateItemInfo(string iteminfo)
    {
        instance.description.text = iteminfo;
    }
    // 刷新背包
    public static void RefreshItem()
    {
        //先銷毀所有的格子
        for(int i = 0; i<instance.slotGrid.transform.childCount;i++)
        {
            Destroy(instance.slotGrid.transform.GetChild(i).gameObject);
            //也清除格子的List
            instance.slots.Clear();
        }
        //再根據背包的狀況生成新的格子
        for(int i = 0; i<instance.backpack.itemList.Count; i++)
        {
            //先生成空格並把他加到List裡
            instance.slots.Add(Instantiate(instance.emptyslot));
            //把空格的parent設成slotgrid使他們排好
            instance.slots[i].transform.SetParent(instance.slotGrid.transform);
            //幫空格編號
            instance.slots[i].GetComponent<Slot>().slotID = i;
            //將backpack的List裡的物品信息放到空格裡
            instance.slots[i].GetComponent<Slot>().SetSlot(instance.backpack.itemList[i]);
        }
    }
}
