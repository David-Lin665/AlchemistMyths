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
        }
    }
    public void AddNewItem()
    {
        if(!playerInventory.itemList.Contains(thisitem))
        {
            playerInventory.itemList.Add(thisitem);
        }else
        {
            thisitem.itemheld++;
        }
    }
}
