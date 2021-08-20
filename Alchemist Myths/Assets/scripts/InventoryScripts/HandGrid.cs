using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HandGrid : MonoBehaviour, IDropHandler
{
    public Image itemImage;
    private Sprite initsprite;
    private Color initColor;
    public Inventory backpack;
    public Item currentEquip;
    private Transform initparent;
    public GameObject connectToUI;

    public void OnBeginDrag(PointerEventData eventData)
    {
        initparent = transform.parent;
        if(itemImage.sprite != initsprite)
        {
            transform.SetParent(transform.parent.parent.parent);
            transform.position = eventData.position;
            GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnDrop(PointerEventData eventData)
    {
        currentEquip = eventData.pointerDrag.gameObject.GetComponent<ItemDrag>().currentItem;
        itemImage.sprite = eventData.pointerDrag.gameObject.GetComponent<ItemDrag>().currentItem.icon;
        itemImage.color = Color.white;

        connectToUI.GetComponent<Image>().sprite = eventData.pointerDrag.gameObject.GetComponent<ItemDrag>().currentItem.icon;
        connectToUI.GetComponent<Image>().color = Color.white;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(eventData.pointerCurrentRaycast.gameObject == null)
        {
            transform.SetParent(initparent);
            transform.position = initparent.position;
            GetComponent<CanvasGroup>().blocksRaycasts = true;
            return;
        }
        if(eventData.pointerCurrentRaycast.gameObject.CompareTag("Slot"))
        {
            controlZ();
            backpack.itemList[eventData.pointerCurrentRaycast.gameObject.GetComponent<Slot>().slotID] = currentEquip;
            InventoryManager.RefreshItem();
            currentEquip = null;
            GetComponent<CanvasGroup>().blocksRaycasts = true;
            return;
        }
        if(eventData.pointerCurrentRaycast.gameObject.name == "Backpack")
        {
            controlZ();
            for(int i = 0; i<backpack.itemList.Count; i++)
            {
                if(backpack.itemList[i]==null)
                {
                    backpack.itemList[i] = currentEquip;
                    break;
                }
            }
            InventoryManager.RefreshItem();
            GetComponent<CanvasGroup>().blocksRaycasts = true;
            return;
        }
        transform.SetParent(initparent);
        transform.position = initparent.position;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
    void Awake()
    {
        itemImage = GetComponent<Image>();
        initsprite = itemImage.sprite;
        currentEquip = null;
        initColor = itemImage.color;
    }
    void controlZ()
    {
        itemImage.sprite = initsprite;
        itemImage.color = initColor;
        transform.SetParent(initparent);
        transform.position = initparent.position;
    }

}