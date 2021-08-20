using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selectItem : MonoBehaviour
{
    [SerializeField] private float currentHandling = 0;
    [SerializeField] private GameObject[] slots = new GameObject[5];
    public GameObject hand0;
    public GameObject hand1;
    public GameObject hand2;
    public GameObject hand3;
    public GameObject hand4;
    
    void Start(){
        slots[0] = hand0;
        slots[1] = hand1;
        slots[2] = hand2;
        slots[3] = hand3;
        slots[4] = hand4;
    }
    // Update is called once per frame
    void Update()
    {
        //refreshSlots();
        NowHolding();
    }

    private void NowHolding(){
        if(Input.GetAxis("Mouse ScrollWheel")> 0f){
            if(currentHandling ==4){
                currentHandling =0;
            }else{
                currentHandling++;
            }

        }
        if(Input.GetAxis("Mouse ScrollWheel")< 0f){
            
            if(currentHandling ==0){
                currentHandling =4;
            }else{
                currentHandling--;
            }
            
        }
        int a = (int)currentHandling;
        Debug.Log(slots[a].name);
    }

    // private void refreshSlots(){
    //     for(int i=0;i<5;i++){
    //         slots[i] =
    //     }
    // }
}
