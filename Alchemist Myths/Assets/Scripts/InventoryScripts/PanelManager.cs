using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    public GameObject inventoryPanel;
    bool isOpen;//default value is false
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab)) // if i press Tab
        {
            isOpen = !isOpen; // false to true, true to false
            inventoryPanel.SetActive(isOpen); // set active or inactive
        }
    }
}
