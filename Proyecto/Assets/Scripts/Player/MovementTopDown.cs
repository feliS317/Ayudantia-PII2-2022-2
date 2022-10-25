using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementTopDown : MonoBehaviour
{
    [Header("Componentes")]
    [SerializeField] private GameObject player;
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sr;

    [Header("Movimiento")]
    private float currentSpeed;
    public float characterSpeed;

    [Header("Attack")]
    [SerializeField] private GameObject hitbox;

    [Header("Dash")]
    public bool isDashing = false;
    private bool canDash = true;
    public float dashSpeed = 5f;
    public float dashTime = 5f;
    public float dashCooldown = 5f;

    // Start is called before the first frame update
    void Start()
    {
        currentSpeed = characterSpeed;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isDashing)
        {
            Attack();
        }
        if(!hitbox.GetComponent<Attack>().attacking)
        {
            Dashing();
        }
        MovementUpdate();
    }

    void MovementUpdate()
    {
        rb.velocity = new Vector2(currentSpeed * Input.GetAxisRaw("Horizontal"), currentSpeed * Input.GetAxisRaw("Vertical"));

        if(Mathf.Abs(rb.velocity.y) > Mathf.Abs(rb.velocity.x))
        {
            sr.flipX = false;
            anim.SetBool("Side", false);
            if (rb.velocity.y > 0)
            {
                anim.SetBool("Down", false);
                anim.SetBool("Up", true);
                anim.SetInteger("Direction", 0);
            }
            else if (rb.velocity.y < 0)
            {
                anim.SetBool("Up", false);
                anim.SetBool("Down", true);
                anim.SetInteger("Direction", 2);
            }
        }
        else if(Mathf.Abs(rb.velocity.x) > Mathf.Abs(rb.velocity.y))
        {
            anim.SetInteger("Direction", 1);
            anim.SetBool("Up", false);
            anim.SetBool("Down", false);
            if (rb.velocity.x > 0)
            {
                anim.SetBool("Side", true);
                sr.flipX = false;
                hitbox.GetComponent<Attack>().Right();
            }
            if (rb.velocity.x < 0)
            {
                anim.SetBool("Side", true);
                sr.flipX = true;
                hitbox.GetComponent<Attack>().Left();
            }
        }
        if (rb.velocity.y == 0)
        {
            anim.SetBool("Up", false);
            anim.SetBool("Down", false);
        }
        if (rb.velocity.x == 0)
        {
            anim.SetBool("Side", false);
        }
    }

    void Attack()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            hitbox.GetComponent<Attack>().AttackAnimation();
        }
        if(Input.GetButtonDown("Fire2"))
        {
            hitbox.GetComponent<Attack>().ProjectileAnimation();
        }
    }

    void Dashing()
    {
        if(Input.GetButtonDown("Jump") && canDash)
        {
            StartCoroutine(Dash(dashTime));
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

    private IEnumerator Dash(float dash)
    {   
        canDash = false;
        isDashing = true;
        currentSpeed = dashSpeed;
        yield return new WaitForSeconds(dash);
        isDashing = false;
        currentSpeed = characterSpeed;
        StartCoroutine(DashCooldown(dashCooldown));
    }

    private IEnumerator DashCooldown(float dashCooldown)
    {      
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;

    }
}
