using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ArmorGrid : MonoBehaviour, IDropHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
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
            transform.SetParent(transform.parent.parent);
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
        if(eventData.pointerDrag.gameObject.GetComponent<ItemDrag>().currentItem.isArmor){
            if(eventData.pointerDrag.gameObject.GetComponent<ItemDrag>().currentItem.Armortag.Contains("Armor")){
                armorimage.sprite = eventData.pointerDrag.gameObject.GetComponent<ItemDrag>().currentItem.icon;
                armorimage.color = Color.white;
                currentEquip = eventData.pointerDrag.gameObject.GetComponent<ItemDrag>().currentItem;
            }
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
            BackToInit();
            backpack.itemList[eventData.pointerCurrentRaycast.gameObject.GetComponent<Slot>().slotID] = currentEquip;
            InventoryManager.RefreshItem();
            currentEquip = null;
            GetComponent<CanvasGroup>().blocksRaycasts = true;
            return;
        }
        if(eventData.pointerCurrentRaycast.gameObject.name == "Backpack")
        {
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
    void BackToInit()
    {
        armorimage.sprite = initsprite;
        armorimage.color = initColor;
        transform.SetParent(initparent);
        transform.position = initparent.position;
    }
}
