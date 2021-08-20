using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Transform originalParent;
    public Inventory backpack; // get the refernce of the backpack
    public int currentID;// 當前物品的slot id
    public Item currentItem;
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;// item 的上級是 slot
        currentID = originalParent.GetComponent<Slot>().slotID; //取得先前id
        transform.SetParent(transform.parent.parent);//點擊時把層級上移到slotgrid避免被擋住
        transform.position = eventData.position; 
        GetComponent<CanvasGroup>().blocksRaycasts = false; // 使他不會擋住raycast
        currentItem = backpack.itemList[currentID];
    }
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position; // 拖曳期間持續更新位置
        //Debug.Log(eventData.pointerCurrentRaycast.gameObject.name);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(eventData.pointerCurrentRaycast.gameObject != null)
        {
            //如果放在另一個物品上
            if(eventData.pointerCurrentRaycast.gameObject.tag == "ItemImage")
            {
                //調換兩物品位置
                transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform.parent.parent); // Image 的上層是Item 再上層是 Slot
                transform.position = eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.position;
                eventData.pointerCurrentRaycast.gameObject.transform.parent.SetParent(originalParent); // Image 的上層 Item 設slot為parent
                eventData.pointerCurrentRaycast.gameObject.transform.parent.position = originalParent.transform.position;
                // 兩物品調換在背包裡的順序
                var temp = backpack.itemList[currentID];
                backpack.itemList[currentID] = backpack.itemList[eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<Slot>().slotID];
                backpack.itemList[eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<Slot>().slotID] = temp;
                GetComponent<CanvasGroup>().blocksRaycasts = true;
                return;
            }
            //如果放在空格上
            if(eventData.pointerCurrentRaycast.gameObject.tag == "Slot")
            {
                //把它放進新空格
                transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform); // 直接setparent
                transform.position = eventData.pointerCurrentRaycast.gameObject.transform.position;
                //把空格跟當前物品在背包裡的順序調換
                backpack.itemList[eventData.pointerCurrentRaycast.gameObject.GetComponent<Slot>().slotID] = backpack.itemList[currentID];
                // 要判斷是否放在自己的位置
                if(currentID != eventData.pointerCurrentRaycast.gameObject.GetComponent<Slot>().slotID)
                    backpack.itemList[currentID] = null;
                GetComponent<CanvasGroup>().blocksRaycasts = true;
                return;
            }
            // 裝備armor
            if(eventData.pointerDrag.gameObject.GetComponent<ItemDrag>().currentItem.isArmor)
            {
                if(eventData.pointerDrag.gameObject.GetComponent<ItemDrag>().currentItem.Armortag.Contains("Armor"))
                {
                    backpack.itemList[currentID] = null;
                    Destroy(gameObject);
                    GetComponent<CanvasGroup>().blocksRaycasts = true;
                    return;
                }
                ReturnItem();
                return;
            }
            ReturnItem();
            return;
        }
        //如果放在背包外
        if(eventData.pointerCurrentRaycast.gameObject == null)
        {
            Destroy(gameObject);
            Instantiate(backpack.itemList[currentID].itemPrefab,PlayerTracker.playerPosition + new Vector3(5,0,0),Quaternion.identity);
            backpack.itemList[currentID] = null;
            InventoryManager.RefreshItem();
        } 
    }
    public void ReturnItem()
    {
        transform.SetParent(originalParent);
        transform.position = originalParent.position;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }        
}
