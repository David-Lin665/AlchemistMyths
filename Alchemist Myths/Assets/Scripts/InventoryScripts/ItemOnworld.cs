using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemOnworld : MonoBehaviour
{
    public Item thisitem;
    public Inventory playerInventory;
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            AddNewItem();
        }
    }
    public void AddNewItem()
    {
        if(!playerInventory.itemList.Contains(thisitem))
        {
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
            thisitem.itemheld+=1;
            Debug.Log("count+1");
        }
        InventoryManager.RefreshItem();
    }
}
