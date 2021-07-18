using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    public GameObject inventoryPanel;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab)) // if i press Tab
        {
            inventoryPanel.SetActive(!inventoryPanel.gameObject.activeSelf); // set active or inactive
        }
    }
}
