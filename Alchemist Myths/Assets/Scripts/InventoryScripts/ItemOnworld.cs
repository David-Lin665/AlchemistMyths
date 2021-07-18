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
            AddNewItem();
            Destroy(gameObject);
        }
    }
    public void AddNewItem()
    {
        if(!playerInventory.itemList.Contains(thisitem))
        {
            playerInventory.itemList.Add(thisitem);
            Debug.Log("New Item added"); 
        }else
        {
            thisitem.itemheld++;
            Debug.Log("count+1");
        }
        InventoryManager.RefreshItem();
    }
}
