using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class statusPanel : MonoBehaviour
{
    public GameObject status;
    public GameObject outlook;
    public GameObject player;


    [SerializeField]
    private float maxHealth;
    [SerializeField]
    private float maxMana;
    [SerializeField]
    private float armor;
    [SerializeField]
    private float speed;

    private int a=1;

    public void Open(){

        if(a==1){//status
            a=0;
            outlook.SetActive(false);
            status.SetActive(true);
            
        }
        else{//outlook
            a=1;
            outlook.SetActive(true);
            status.SetActive(false);
            
        }
    }

    void UpdateStatus(string entry,float value){
        if(entry == "maxHealth"){
            //player.getChild();
            maxHealth = maxHealth*value;
        }
    }
    void UpdateOutlook(){

    }

}
