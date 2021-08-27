using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmorSlot : MonoBehaviour
{
    public int slotID;
    public Item slotItem;
    public Image slotImage;
    public GameObject count;
    public GameObject itemInSlot;
    public string slotInfo;
    public void SetArmorSlot(Item item)
    {
        // 如果是空格
        if(item == null)
        {
            itemInSlot.SetActive(false);
            return;
        }
        //如果不是空格，就把訊息更新
        slotItem = item;
        slotImage.sprite = item.icon;
        count.SetActive(false);
        slotInfo = item.description;
    }
    
}
