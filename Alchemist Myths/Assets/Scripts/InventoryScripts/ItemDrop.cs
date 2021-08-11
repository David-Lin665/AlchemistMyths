using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemDrop : MonoBehaviour, IDropHandler
{
    public Image armorimage;

    public void OnDrop(PointerEventData eventData)
    {
        
        if(eventData.pointerDrag.gameObject.GetComponent<ItemDrag>().currentItem.itemName.Contains("甲"))
            armorimage.sprite = eventData.pointerDrag.gameObject.GetComponent<ItemDrag>().currentItem.icon;
            armorimage.color = Color.white;
            Debug.Log("sprite changed");
    }

    void Awake()
    {
        armorimage = GetComponent<Image>();
    }

    
}
