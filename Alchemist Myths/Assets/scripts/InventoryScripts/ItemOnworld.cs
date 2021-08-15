using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 這個是用來偵測誰碰到地圖上的道具，並做出反應的程式
public class ItemOnworld : MonoBehaviour
{
    public Item thisitem;
    public Inventory playerInventory;
    void OnTriggerStay2D(Collider2D other)// call when other collider enters the zone
    {
        // 判斷是不是主角碰到
        if(other.gameObject.CompareTag("Player"))
        {
            if(Input.GetKey(KeyCode.S))
            {
                Destroy(gameObject);
                AddNewItem();  
            }
        }
    }
    public void AddNewItem()
    {
        if(!playerInventory.itemList.Contains(thisitem))
        {
            //找空的格子
            for(int i = 0; i< playerInventory.itemList.Count; i++)
            {
                if(playerInventory.itemList[i] == null)
                {
                    playerInventory.itemList[i] = thisitem;
                    break;
                }
            }
            Debug.Log("New Item added"); 
        }else
        {
            //數量加一
            thisitem.itemheld+=1;
            Debug.Log("count+1");
        }
        // 刷新背包
        InventoryManager.RefreshItem();
    }
}
