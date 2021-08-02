using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_behaviour : MonoBehaviour
{
    /*Unity�����]�w
    Animation Bool: Attack, canWalk
    Enemy��ƪ��ɮת���: collider, sprites, hitbox, raycast, triggerArea
    leftLimit, rightLimit (�ĤH�i�H���ʪ����k����)*/
    
    #region Public Variables
    public float moveSpeed;
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
        SelectTarget(); 

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
            Move();
        }

        if (!InsideofLimits() && !inRange && !anim.GetCurrentAnimatorStateInfo(0).IsName("Enemy_attack")) //���k�������B�b�ĤH���u���B���O�B�b"�������ʵe"���Y�i��w
        {
            SelectTarget();
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

    void Move() //�ĤH�ʰ_��
    {
        anim.SetBool("canWalk", true);

        if(!anim.GetCurrentAnimatorStateInfo(0).IsName("Enemy_attack"))
        {
            Vector2 targetPosition = new Vector2(target.position.x, transform.position.y);

            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
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

    private bool InsideofLimits() //���ʷ����d�򪺳]�w
    {
        return transform.position.x > leftLimit.position.x && transform.position.x < rightLimit.position.x;
    }

    public void SelectTarget() //�ĤH���k���ʪ��]�w
    {
        float distanceToLeft = Vector2.Distance(transform.position, leftLimit.position);
        float distanceToRight = Vector2.Distance(transform.position, rightLimit.position);

        if (distanceToLeft > distanceToRight)
        {
            target = leftLimit;
        }
        else
        {
            target = rightLimit;
        }

        Flip();
    }

    public void Flip() //�ĤH���k���ʮɪ�½��
    {
        Vector3 rotation = transform.eulerAngles;
        if (transform.position.x > target.position.x)
        {
            rotation.y = 180f;
        }
        else
        {
            rotation.y = 0f;
        }

        transform.eulerAngles = rotation;
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
