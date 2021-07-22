using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Transform originalParent;
    public Inventory backpack; // get the refernce of the backpack
    public int currentID;// 當前物品的slot id
    public Transform playerTransform;
    public GameObject itemdroped;
    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;// item 的上級是 slot
        currentID = originalParent.GetComponent<Slot>().slotID; //取得先前id
        transform.SetParent(transform.parent.parent);//點擊時把層級上移到slotgrid避免被擋住
        transform.position = eventData.position; 
        GetComponent<CanvasGroup>().blocksRaycasts = false; // 使他不會擋住raycast
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position; // 拖曳期間
        //Debug.Log(eventData.pointerCurrentRaycast.gameObject.name);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(eventData.pointerCurrentRaycast.gameObject != null)
        {
            if(eventData.pointerCurrentRaycast.gameObject.tag == "ItemImage")
            {
                transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform.parent.parent); // Image 的上層是Item 再上層是 Slot
                transform.position = eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.position;
                eventData.pointerCurrentRaycast.gameObject.transform.parent.SetParent(originalParent); // Image 的上層 Item 設slot為parent
                eventData.pointerCurrentRaycast.gameObject.transform.parent.position = originalParent.transform.position;
                // 兩物品id的調換
                var temp = backpack.itemList[currentID];
                backpack.itemList[currentID] = backpack.itemList[eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<Slot>().slotID];
                backpack.itemList[eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<Slot>().slotID] = temp;
                GetComponent<CanvasGroup>().blocksRaycasts = true;
                return;
            }
            else if(eventData.pointerCurrentRaycast.gameObject.tag == "Slot")
            {
                transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform); // 直接setparent
                transform.position = eventData.pointerCurrentRaycast.gameObject.transform.position;
                //把物品調換到空格
                backpack.itemList[eventData.pointerCurrentRaycast.gameObject.GetComponent<Slot>().slotID] = backpack.itemList[currentID];
                // 要判斷是否放在自己的位置
                if(currentID != eventData.pointerCurrentRaycast.gameObject.GetComponent<Slot>().slotID)
                    backpack.itemList[currentID] = null;
                GetComponent<CanvasGroup>().blocksRaycasts = true;
                return;
            }
        }
        if(eventData.pointerCurrentRaycast.gameObject == null)
        {
            DropItemtoWorld();
        } 
    }

    public void DropItemtoWorld()
    {
        Vector3 dropPosistion = playerTransform.position + new Vector3(5,0,0);
        itemdroped = Instantiate(backpack.itemList[currentID].itemPrefab);
        itemdroped.transform.position = dropPosistion;
        backpack.itemList[currentID] = null;
        InventoryManager.RefreshItem();
    }
        
}
