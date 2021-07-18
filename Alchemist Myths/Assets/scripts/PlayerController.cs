using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour 
{
    public float speed;
    public float jumpforce;
    private Rigidbody2D rb;
    private bool facingRight = false;
    private float moveInput;
    private bool isGrounded;
    public Transform groundcheck;
    public float checkRadius;
    public LayerMask whatisGround;
    public int extrajumps;
    int jumps;

    private CapsuleCollider2D cc;
    private Vector2 colliderSize;
    private float slopeDownAngle;
    private Vector2 slopeNormalPerp;
    private bool isOnSlope;
    private float slopeDownAngleOld;
    private Vector2 newVelocity;
    private float slopeSideAngle;
    [SerializeField] private PhysicsMaterial2D noFriction;
    [SerializeField] private PhysicsMaterial2D fullFriction;
    private bool isjumping;
    //[SerializeField] private float 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cc= GetComponent<CapsuleCollider2D>();
        colliderSize = cc.size;
    }
    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundcheck.position,checkRadius,whatisGround);
        //Debug.Log(isGrounded);
        
        SlopeCheck();//檢查斜坡
        PlayerMovement();//玩家移動
        
        if(facingRight==true&& moveInput<0){//翻面
            flip();
        }
        if(facingRight!=true&& moveInput>0){
            flip();
        }
    }

    void Update()//跳躍
    {
        if(isGrounded==true){//若玩家接觸地板
            jumps = extrajumps;
            isjumping = false;
            //Debug.Log("isjumping=false");
        }
        if(Input.GetKeyDown(KeyCode.Space)&&jumps>0){//多段跳
            isjumping = true;
            rb.velocity = Vector2.up*jumpforce;
            jumps--;
            //Debug.Log("isjumping=true");
        }
    }
    void OnDrawGizmosSelected(){//groundCheck範圍顯示
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(groundcheck.position,checkRadius);
    }
    void flip()//翻轉
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x*=-1;
        transform.localScale = Scaler;
    }

    private void SlopeCheck(){//檢查斜坡
        Vector2 checkPos = transform.position - new Vector3(0.0f,colliderSize.y / 2);
        SlopeCheckHoirzontal(checkPos);
        SlopeCheckVertical(checkPos);
    }

    private void SlopeCheckHoirzontal(Vector2 checkPos){//斜坡X軸
        RaycastHit2D slopeHitFront = Physics2D.Raycast(checkPos,transform.right,checkRadius,whatisGround);
        RaycastHit2D slopeHitBack = Physics2D.Raycast(checkPos,-transform.right,checkRadius,whatisGround);
        if(slopeHitFront){//前方斜坡
            slopeSideAngle = Vector2.Angle(slopeHitFront.normal,Vector2.up);
            if(slopeSideAngle ==90f){//平地
                isOnSlope = false;
            }else{
                isOnSlope = true;
            }
        }
        else if(slopeHitBack){//後方斜坡
            slopeSideAngle = Vector2.Angle(slopeHitBack.normal,Vector2.up);
            if(slopeSideAngle ==90f){//平地
                isOnSlope = false;
            }else{
                isOnSlope = true;
            }
        }else{
            slopeSideAngle = 0.0f;
            isOnSlope = false;
        }
    }
    
    private void SlopeCheckVertical(Vector2 checkPos){//斜坡Y軸
        RaycastHit2D hit = Physics2D.Raycast(checkPos,Vector2.down,checkRadius,whatisGround);

        if(hit){
            slopeNormalPerp = Vector2.Perpendicular(hit.normal).normalized;
            slopeDownAngle = Vector2.Angle(hit.normal,Vector2.up);

            if(slopeDownAngle != slopeDownAngleOld){
                isOnSlope = true;
            }
            slopeDownAngleOld = slopeDownAngle;

            //Debug.DrawRay(hit.point,slopeNormalPerp,Color.red);
            //Debug.DrawRay(hit.point,hit.normal,Color.green);
        }

        if(isOnSlope && moveInput == 0.0f){
            rb.sharedMaterial = fullFriction;
        }else{
            rb.sharedMaterial = noFriction;
        }
    }

    private void PlayerMovement(){//移動
        moveInput = Input.GetAxis("Horizontal");
        
        newVelocity.Set(speed*moveInput,rb.velocity.y);
        rb.velocity = newVelocity;

        if(isGrounded && !isOnSlope && !isjumping){//平地
            newVelocity.Set(moveInput*speed,0.0f);
            rb.velocity = newVelocity;
            //Debug.Log("flat");
        }
        else if(isGrounded && isOnSlope && !isjumping){//斜坡
            newVelocity.Set(speed * slopeNormalPerp.x * -moveInput , speed*slopeNormalPerp.y*-moveInput);
            rb.velocity = newVelocity;
            //Debug.Log("slope");
        }
        else if(!isGrounded){//空中
            rb.velocity = new Vector2(moveInput*speed, rb.velocity.y);
            rb.velocity = newVelocity;
            //Debug.Log("air");
        }
    }
}