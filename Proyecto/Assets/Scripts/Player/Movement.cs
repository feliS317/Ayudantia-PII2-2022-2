using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Componentes")]
    private Rigidbody2D rb;

    [Header("Movimiento")]
    public float characterSpeed;

    [Header("Salto")]
    private bool canDoubleJump = true;
    public float jumpForce;

    [Header("Grounded")]
    private bool isGrounded;
    public Transform groundCheckpoint;
    public LayerMask ground;

    [Header("Animacion")]
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(characterSpeed * Input.GetAxisRaw("Horizontal"), rb.velocity.y);
        anim.SetFloat("Xaxis", Mathf.Abs(rb.velocity.x));
        if(rb.velocity.y < -0.2f)
        {
            anim.SetBool("isFalling", true);
            anim.SetBool("isJumping", false);
        }
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
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                if(!isGrounded && canDoubleJump)
                {
                    canDoubleJump = false;
                }
            }    
        }
    }
}
