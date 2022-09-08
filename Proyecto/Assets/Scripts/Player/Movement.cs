using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float characterSpeed = 3f;
    private Rigidbody2D rb;
    [Header("Salto")]
    public float jumpForce;
    private bool isGrounded;
    [SerializeField] private Transform groundCheckpoint;
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

        if(Input.GetKeyDown(KeyCode.T) && isGrounded){
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }
}
