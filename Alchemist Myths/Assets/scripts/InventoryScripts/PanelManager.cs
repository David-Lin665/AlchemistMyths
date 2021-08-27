using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    public GameObject inventoryPanel;
    public GameObject playerStatusPanel;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P)) // if i press Tab
        {
            inventoryPanel.SetActive(!inventoryPanel.gameObject.activeSelf); // set active or inactive
            playerStatusPanel.SetActive(!playerStatusPanel.gameObject.activeSelf);
        }
    }
}
