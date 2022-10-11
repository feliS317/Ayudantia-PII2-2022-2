using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Componentes")]
    [SerializeField] private GameObject player;
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sr;

    [Header("Movimiento")]
    public float characterSpeed;

    [Header("Salto")]
    private bool canDoubleJump = true;
    public float jumpForce;

    [Header("Grounded")]
    private bool isGrounded;
    public Transform groundCheckpoint;
    public LayerMask ground;

    [Header("Attack")]
    [SerializeField] private GameObject hitbox;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
        MovementUpdate();
        JumpingAndFalling();
    }

    void MovementUpdate()
    {
        rb.velocity = new Vector2(characterSpeed * Input.GetAxisRaw("Horizontal"), rb.velocity.y);
        anim.SetFloat("moveSpeed", Mathf.Abs(rb.velocity.x));
        if(rb.velocity.x < 0)
        {
            sr.flipX = true;
            hitbox.GetComponent<Attack>().Left();
        }
        else if(rb.velocity.x > 0)
        {
            sr.flipX = false;
            hitbox.GetComponent<Attack>().Right();
        }
    }

    void JumpingAndFalling()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheckpoint.position, 0.1f, ground);
        if(isGrounded)
        {
            canDoubleJump = true;
            anim.SetBool("isFalling", false);
        }
        if(Input.GetButtonDown("Jump")){
            if(isGrounded || canDoubleJump)
            {
                anim.SetBool("isJumping", true);
                anim.SetBool("isFalling", false);
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                if(!isGrounded && canDoubleJump)
                {
                    canDoubleJump = false;
                }
            }    
        }
        if(rb.velocity.y < -1f)
        {
            anim.SetBool("isFalling", true);
            anim.SetBool("isJumping", false);
        }
    }

    void Attack()
    {
        if(Input.GetButtonDown("Fire1") && isGrounded)
        {
            hitbox.GetComponent<Attack>().AttackAnimation();
        }
        if(Input.GetButtonDown("Fire2") && isGrounded)
        {
            hitbox.GetComponent<Attack>().ProjectileAnimation();
        }
    }

    public void ShootProjectile()
    {
        hitbox.GetComponent<Attack>().ShootProjectile();
    }

    public void EndAttack()
    {
        hitbox.GetComponent<Attack>().EndAttack();
    }

    public void EndFiring()
    {
        hitbox.GetComponent<Attack>().EndFiring();
    }
}
