using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;         
public class statusPanel : MonoBehaviour//接收格子物品傳遞的BUFF、改動數值，傳給pc
{
    public GameObject status;
    public GameObject outlook;
    public GameObject player;
    public Text currentHealthText;
    public Text maxHealthText;
    public Text currentManaText;
    public Text maxManaText;
    public Text damageText;
    public Text damageBuffText;
    public Text speedText;
    public Text armorText;

    private int a=1;

    void Start(){// 初始化
        setMaxHealth(100);
        setHealth(100);
        setMaxMana(100);
        setMana(100);
        setArmor(0);
        setSpeed(6f);
    }

    public void Open(){//切換外觀面板/數據面板

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

    public void setHealth(int Health){//更改血量 (Health正值或是負值)
        int mh = int.Parse(maxHealthText.text);
        int ch = int.Parse(currentHealthText.text);
        ch += Health;
        if (ch<=0){//死亡
            ch = 0;
            //Debug.Log("X_X");
        }
        if(ch>mh){//超過最大血量
            ch = mh;
            //Debug.Log("maxHealth!");
        }
        player.GetComponentInChildren<HealthBar>().SetHealth(ch);
        currentHealthText.text = ch.ToString();
    }

    public void setMana(int Mana){//更改藍量 (Mana正值或是負值)
        int cm = int.Parse(currentManaText.text);
        int mm = int.Parse(maxManaText.text);
        cm += Mana;
        if (cm<=0){//藍亮不夠
            cm -= Mana;
            //Debug.Log("cant use");
        }
        if(cm>mm){//超過最大藍量
            cm = mm;
            //Debug.Log("maxMana!");
        }
        player.GetComponentInChildren<ManaBar>().SetMana(cm);
        currentManaText.text = cm.ToString();
    }

    public void setMaxHealth(int Health){//更改最大血量 (Health正值或是負值)
        int mh = int.Parse(maxHealthText.text);  
        int ch = int.Parse(currentHealthText.text);
        mh += Health;//mh = 110
        if(mh<ch){//目前血量不超過最大血量
            setHealth(mh);
            Debug.Log("in mh<ch");
        }
        player.GetComponentInChildren<HealthBar>().SetMaxHealth(mh);
        maxHealthText.text = mh.ToString();
    }

    public void setMaxMana(int Mana){//更改最大藍量 (Mana正值或是負值)
        int cm = int.Parse(currentManaText.text);
        int mm = int.Parse(maxManaText.text);
        mm += Mana;
        if(mm<cm){//目前藍量不超過最大藍量
            setMana(mm);
        }
        player.GetComponentInChildren<ManaBar>().SetMaxMana(mm);
        maxManaText.text = mm.ToString();
    }

    public void setArmor(float Armor){//更改人物護甲 (Armor正值或是負值)
        float ar = float.Parse(armorText.text);
        ar += Armor;
        armorText.text = ar.ToString();
    }

    public void setSpeed(float Speed){//更改人物移動速度 (Speed正值或是負值)
        float sp = float.Parse(speedText.text);
        sp += Speed;
        player.GetComponent<PlayerController>().speed = sp;
        speedText.text = sp.ToString();
    }

    public void setDamageBuff(int Damage){//攻擊力 (Damage正值或是負值)

        int dmgBuff = int.Parse(damageBuffText.text);
        dmgBuff += Damage;

        damageBuffText.text = dmgBuff.ToString();
    }

    public void setJumpforce(float Jumpforce){//更改人物跳躍高度 (Jumpforce正值或是負值)
        //float jf = float.Parse(jumpforceText.text);
        //jf += Jumpforce;
        //player.GetComponent<PlayerController>().jumpforce = jf;
        //jumpforceText.text = jf.ToString();
    }

    public void setExtraJumps(int extrajumps){//更改人物連跳次數 (extrajumps正值或是負值)
        // int ej = int.Parse(extraJumpText.text);
        // ej += extrajumps;
        // player.GetComponent<PlayerController>().extrajumps = ej;
        //extraJumpText.text = ej.ToString();
    }

}
