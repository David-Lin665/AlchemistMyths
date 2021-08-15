using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemDrop : MonoBehaviour, IDropHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public Image armorimage;
    private Sprite initsprite;
    private Color initColor;
    public Inventory backpack;
    public Item currentEquip;
    private Transform initparent;

    public void OnBeginDrag(PointerEventData eventData)
    {
        initparent = transform.parent;
        if(armorimage.sprite != initsprite)
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
        if(eventData.pointerDrag.gameObject.GetComponent<ItemDrag>().currentItem.itemName.Contains("護甲"))
        {
            currentEquip = eventData.pointerDrag.gameObject.GetComponent<ItemDrag>().currentItem;
            armorimage.sprite = eventData.pointerDrag.gameObject.GetComponent<ItemDrag>().currentItem.icon;
            armorimage.color = Color.white;
        }
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
        armorimage = GetComponent<Image>();
        initsprite = armorimage.sprite;
        currentEquip = null;
        initColor = armorimage.color;
    }
    void controlZ()
    {
        armorimage.sprite = initsprite;
        armorimage.color = initColor;
        transform.SetParent(initparent);
        transform.position = initparent.position;
    }
}
