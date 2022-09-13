using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float characterSpeed = 3f;
    private Rigidbody2D rb;
    [Header("Salto")]
    private bool canDoubleJump = true;
    public float jumpForce;

    [Header("Grounded")]
    private bool isGrounded;
    public Transform groundCheckpoint;
    public LayerMask ground;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(characterSpeed * Input.GetAxisRaw("Horizontal"), rb.velocity.y);

        isGrounded = Physics2D.OverlapCircle(groundCheckpoint.position, 0.2f, ground);
        if(isGrounded)
        {
            canDoubleJump = true; 
        }
        
        if(Input.GetKeyDown(KeyCode.T)){
            if(isGrounded || canDoubleJump)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                if(!isGrounded && canDoubleJump)
                {
                    canDoubleJump = false;
                }
            }
            
        }
    }
}
