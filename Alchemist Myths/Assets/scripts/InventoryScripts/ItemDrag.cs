using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Transform originalParent;
    public Inventory backpack; // get the refernce of the backpack
    public Inventory equipment;
    public int currentID;// 當前物品的slot id
    [SerializeField] private Image itemImage;
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;// item 的上級是 slot
        currentID = originalParent.GetComponent<Slot>().slotID;//取得先前id 
        transform.SetParent(transform.parent.parent.parent.parent);//點擊時把層級上移到slotgrid避免被擋住
        transform.position = eventData.position; 
        GetComponent<CanvasGroup>().blocksRaycasts = false; // 使他不會擋住raycast
    }
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position; // 拖曳期間持續更新位置
        //Debug.Log(eventData.pointerCurrentRaycast.gameObject.name);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        
        if(eventData.pointerCurrentRaycast.gameObject == null)
        {
            Destroy(gameObject);
            Instantiate(backpack.itemList[currentID].itemPrefab,GameObject.FindWithTag("Player").transform.position,Quaternion.identity);
            backpack.itemList[currentID] = null;
            InventoryManager.RefreshItem();
            return;
        } 
        //如果放在另一個物品上
        if(eventData.pointerCurrentRaycast.gameObject.tag == "ItemImage")
        {   //如果是從背包裡拿
            if(currentID < InventoryManager.instance.offset)
            {   //如果不是放裝備格
                if(eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<Slot>().slotID < InventoryManager.instance.offset)
                {
                    // 交換兩個item的parent跟位置
                    transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform.parent.parent); 
                    transform.position = eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.position;
                    eventData.pointerCurrentRaycast.gameObject.transform.parent.SetParent(originalParent); 
                    eventData.pointerCurrentRaycast.gameObject.transform.parent.position = originalParent.transform.position;
                    //交換Item 在背包中的順序
                    var temp = backpack.itemList[currentID];
                    backpack.itemList[currentID] = backpack.itemList[eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<Slot>().slotID];
                    backpack.itemList[eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<Slot>().slotID] = temp;
                    InventoryManager.RefreshItem();
                    return;
                }
                //放在Armor格
                else if(eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<Slot>().slotID == 0 + InventoryManager.instance.offset)
                {
                    // 如果item是armor
                    if(backpack.itemList[currentID].isArmor)
                    {
                        //交換物品的位置
                        transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform.parent.parent); // Image 的上層是Item 再上層是 Slot
                        transform.position = eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.position;
                        eventData.pointerCurrentRaycast.gameObject.transform.parent.SetParent(originalParent); // Image 的上層 Item 設slot為parent
                        eventData.pointerCurrentRaycast.gameObject.transform.parent.position = originalParent.transform.position;
                        //互換inventory裡的位置
                        var temp = backpack.itemList[currentID];
                        backpack.itemList[currentID] = equipment.itemList[0];
                        equipment.itemList[0] = temp;
                        InventoryManager.RefreshItem();
                        InventoryManager.RefreshEquipment();
                        return;
                    }
                    //否則將item歸位
                    ReturnItem();
                    GetComponent<CanvasGroup>().blocksRaycasts = true;
                    return;
                }
                // 放在boot格
                else if(eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<Slot>().slotID == (1 + InventoryManager.instance.offset))
                {
                    if(backpack.itemList[currentID].isBoots)
                    {
                        //跟上面一樣
                        transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform.parent.parent); // Image 的上層是Item 再上層是 Slot
                        transform.position = eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.position;
                        eventData.pointerCurrentRaycast.gameObject.transform.parent.SetParent(originalParent); // Image 的上層 Item 設slot為parent
                        eventData.pointerCurrentRaycast.gameObject.transform.parent.position = originalParent.transform.position;
                        var temp = backpack.itemList[currentID];
                        backpack.itemList[currentID] = equipment.itemList[1];
                        equipment.itemList[1] = temp;
                        InventoryManager.RefreshItem();
                        InventoryManager.RefreshEquipment();
                        return;
                    }
                    ReturnItem();
                    GetComponent<CanvasGroup>().blocksRaycasts = true;
                    return;
                }
            }
        }
        //如果放在空格上
        if(eventData.pointerCurrentRaycast.gameObject.CompareTag("Slot"))
        {
            //把它放進新空格
            if(currentID < InventoryManager.instance.offset)
            {
                transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform); // 直接setparent
                transform.position = eventData.pointerCurrentRaycast.gameObject.transform.position;
                //把空格跟當前物品在背包裡的順序調換
                backpack.itemList[eventData.pointerCurrentRaycast.gameObject.GetComponent<Slot>().slotID] = backpack.itemList[currentID];
                // 要判斷是否放在自己的位置
                if(currentID != eventData.pointerCurrentRaycast.gameObject.GetComponent<Slot>().slotID)
                    backpack.itemList[currentID] = null;
                InventoryManager.RefreshItem();
                GetComponent<CanvasGroup>().blocksRaycasts = true;
                return;
            }
            transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform); // 直接setparent
            transform.position = eventData.pointerCurrentRaycast.gameObject.transform.position;
            backpack.itemList[eventData.pointerCurrentRaycast.gameObject.GetComponent<Slot>().slotID] = equipment.itemList[currentID-InventoryManager.instance.offset];
            equipment.itemList[currentID-InventoryManager.instance.offset] = null;
            InventoryManager.RefreshItem();
            GetComponent<CanvasGroup>().blocksRaycasts = true;
            return;
        }
        // 裝備armor
        if(eventData.pointerCurrentRaycast.gameObject.CompareTag("Armor"))
        {
            if(currentID < InventoryManager.instance.offset)
            {
                if(backpack.itemList[currentID].isArmor)
                {
                    transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform.parent);
                    transform.position = eventData.pointerCurrentRaycast.gameObject.transform.parent.position;
                    equipment.itemList[0] = backpack.itemList[currentID];
                    backpack.itemList[currentID] = null;
                    InventoryManager.RefreshEquipment();
                    InventoryManager.RefreshItem();
                    return;
                }
                ReturnItem();
                GetComponent<CanvasGroup>().blocksRaycasts = true;
                return;
            }
            ReturnItem();
            GetComponent<CanvasGroup>().blocksRaycasts = true;
            return;
        }
        if(eventData.pointerCurrentRaycast.gameObject.CompareTag("Boot"))
        {
            if(currentID < InventoryManager.instance.offset)
            {
                if(backpack.itemList[currentID].isBoots)
                {
                    transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform.parent);
                    transform.position = eventData.pointerCurrentRaycast.gameObject.transform.parent.position;
                    equipment.itemList[1] = backpack.itemList[currentID];
                    backpack.itemList[currentID] = null;
                    InventoryManager.RefreshEquipment();
                    InventoryManager.RefreshItem();
                    GetComponent<CanvasGroup>().blocksRaycasts = true;
                    return;
                }
                ReturnItem();
                GetComponent<CanvasGroup>().blocksRaycasts = true;
                return;
            }
            ReturnItem();
            GetComponent<CanvasGroup>().blocksRaycasts = true;
            return;
        }
        ReturnItem();
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        return;
        
    }
    void ReturnItem()
    {
        transform.SetParent(originalParent);
        transform.position = originalParent.position;
    }        
}
