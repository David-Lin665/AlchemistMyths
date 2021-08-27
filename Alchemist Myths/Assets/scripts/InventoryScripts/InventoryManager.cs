using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;
    [Header("Inventories")]
    [SerializeField] private Inventory backpack;
    [SerializeField] private Inventory equipment;
    [Header("SlotGrid")]
    [SerializeField] private GameObject slotGrid;
    [Header("EquipmentSlots")]
    [SerializeField] private GameObject equipmentParent;
    [Header("Prefabs")]
    [SerializeField] private GameObject emptyItemSlot;
    [SerializeField] private List<GameObject> equipmentSlots = new List<GameObject>();
    [Header("Text Area")]
    [SerializeField] private Text description;
    [Header("EquipmentID offset")]
    public int offset;
    private List<GameObject> slots = new List<GameObject>();
    private List<GameObject> equipments = new List<GameObject>();
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
        RefreshEquipment();
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
            instance.slots.Add(Instantiate(instance.emptyItemSlot));
            //把空格的parent設成slotgrid使他們排好
            instance.slots[i].transform.SetParent(instance.slotGrid.transform);
            //幫空格編號
            instance.slots[i].GetComponent<Slot>().slotID = i;
            //將backpack的List裡的物品信息放到空格裡
            instance.slots[i].GetComponent<Slot>().SetSlot(instance.backpack.itemList[i]);
        }
    }
    public static void RefreshEquipment()
    {
        //先銷毀所有的格子
        for(int i = 0; i<instance.equipmentParent.transform.childCount;i++)
        {
            Destroy(instance.equipmentParent.transform.GetChild(i).gameObject);
            instance.equipments.Clear();
        }
        //再根據背包的狀況生成新的格子
        for(int i = 0; i<instance.equipment.itemList.Count; i++)
        {
            //先生成空格並把他加到List裡
            instance.equipments.Add(Instantiate(instance.equipmentSlots[i]));
            //把空格的parent設成slotgrid使他們排好
            instance.equipments[i].transform.SetParent(instance.equipmentParent.transform);
            //幫空格編號
            instance.equipments[i].GetComponent<Slot>().slotID = i+ instance.offset;
            //將backpack的List裡的物品信息放到空格裡
            instance.equipments[i].GetComponent<Slot>().SetSlot(instance.equipment.itemList[i]);
        }
    }
}
