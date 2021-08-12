using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_behaviour_stand_ver : MonoBehaviour
{
    #region Public Variables
    public float attackDistance; //Minimum distance for attack
    public Transform leftLimit;
    public Transform rightLimit;
    public float intTimer;
    [HideInInspector] public Transform target;
    [HideInInspector] public bool inRange; //Check if Player is in range
    public GameObject hotZone;
    public GameObject triggerArea;
    #endregion

    #region Private Variables
    private Animator anim;
    private float distance; //Store the distance b/w enemy and player
    private bool attackMode;
    private bool cooling; //Check if Enemy is cooling after attack
    #endregion

    void Awake()
    {
        anim = GetComponent<Animator>(); //use animator
    }

    void Start()
    {
        StartCoroutine(Cool());
    }

    void Update()
    {
        if (!attackMode)
        {
            sleeping();
        }

        if (inRange)
        {
            EnemyLogic();
        }
    }


    void EnemyLogic() //�ϼĤH�P�_�Ӱ�����Ӱʧ@��
    {
        distance = Vector2.Distance(transform.position, target.position);

        if (distance > attackDistance)
        {
            StopAttack();
        }
        else if (attackDistance >= distance && cooling == false)
        {

            Attack();
        }
    }

    void Attack() //�s�ĤH������
    {
        attackMode = true; //To checl if Enemy can still attack or not

        anim.SetBool("canWalk", false);
        anim.SetBool("Attack", true);
    }

    void StopAttack() //���U�������ܼƳ]�w
    {
        if (cooling)
        {
            attackMode = true;
            return;
        }
        attackMode = false;
        anim.SetBool("Attack", false);
    }


    public void TriggerCooling() //�b�g�L�������᪺�N�o
    {
        cooling = true;
    }

    void sleeping()
    {
        anim.SetBool("Attack",false);
    }
    
    IEnumerator Cool()
    {
        while (true)
        {
            yield return new WaitUntil(()=>cooling && inRange);
            anim.SetBool("Attack", false);
            yield return new WaitForSeconds(intTimer);
            cooling = false;
        }
    }
}
