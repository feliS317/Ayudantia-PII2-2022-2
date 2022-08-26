using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Vector2 movement = Vector2.zero;
    [SerializeField] private float characterSpeed = 3f;
    private Animator animator;
    private Rigidbody2D rb;
    [Header("Salto")]
    public float jumpForce;
    private bool isGrounded;
    public Transform groundCheckpoint;
    public LayerMask ground;

    // Start is called before the first frame update

    /*public void OnMove(InputAction.CallbackContext context) // Movimiento con InputSystem
    {
        movement = context.ReadValue<Vector2>();
    }*/

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(characterSpeed * Input.GetAxisRaw("Horizontal"), rb.velocity.y);

        isGrounded = Physics2D.OverlapCircle(groundCheckpoint.position, .2f, ground);

        if(Input.GetKeyDown(KeyCode.T) &&  isGrounded){
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    /*void FixedUpdate() {
        rb.MovePosition(rb.position + movement.normalized * characterSpeed * Time.fixedDeltaTime);
    }*/
}
