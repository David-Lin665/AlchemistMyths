using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public int slotID;
    public Item slotItem;
    public Image slotImage;
    public Text slotnum;
    public GameObject itemInSlot;
    public string slotInfo;

    public void SlotOnclick()
    {
        InventoryManager.UpdateItemInfo(slotInfo);
    }

    public void SetSlot(Item item)
    {
        if(item == null)
        {
            itemInSlot.SetActive(false);
            return;
        }

        slotImage.sprite = item.icon;
        slotnum.text = item.itemheld.ToString();
        slotInfo = item.description;
    }
}
