using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public int slotID;
    public Item slotItem;
    [SerializeField] private Image slotImage;
    [SerializeField] private GameObject count;
    [SerializeField] private Text slotnum;
    [SerializeField] private GameObject itemInSlot;
    [SerializeField] private string slotInfo;
    [SerializeField] private bool isEquip;
    public void SlotOnclick()
    {
        InventoryManager.UpdateItemInfo(slotInfo);
    }

    public void SetSlot(Item item)
    {
        if(isEquip)
        {
            count.SetActive(false);
        }
        // 如果是空格
        if(item == null)
        {
            //把圖片隱藏
            itemInSlot.SetActive(false);
            return;
        }
        //如果不是空格，就把訊息更新
        slotItem = item;
        slotImage.sprite = item.icon;
        slotnum.text = item.itemheld.ToString();
        slotInfo = item.description;
    }
}
