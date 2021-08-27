using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selectItem : MonoBehaviour
{
    [SerializeField] private int currentGrid =0;
    //[SerializeField] private GameObject currentHandling;
    [SerializeField] private GameObject[] slots = new GameObject[5];
    // Update is called once per frame
    void Update()
    {
        NowHolding();
    }

    private void NowHolding(){
        if(Input.GetAxis("Mouse ScrollWheel")< 0f){
            if(currentGrid ==4){
                currentGrid =0;
            }else{
                currentGrid++;
            }
            ShowCurrentSelect();
        }
        if(Input.GetAxis("Mouse ScrollWheel")> 0f){
            
            if(currentGrid ==0){
                currentGrid =4;
            }else{
                currentGrid--;
            }
            ShowCurrentSelect();
        }
    }

    private void ShowCurrentSelect()
    {
        int a = currentGrid;
        Debug.Log(slots[a].name);
    }
}
