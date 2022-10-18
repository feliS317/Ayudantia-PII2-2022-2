using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed;

    public Transform leftPoint, rightPoint;

    private bool movingRight;

    private Animator anim;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    [SerializeField] private GameObject hitbox;

    public float moveTime, waitTime;
    private float moveCount, waitCount;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        leftPoint.parent = null;
        rightPoint.parent = null;

        movingRight = true;

        moveCount = moveTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(!hitbox.GetComponent<EnemyAttack>().attacking)
        {
            Attack();
        }
        AnimationAndMovement();
    }

    private void AnimationAndMovement()
    {
        anim.SetFloat("moveSpeed", Mathf.Abs(rb.velocity.x));
        if(moveCount > 0)
        {
            moveCount -= Time.deltaTime;

            if(movingRight)
            {
                rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
                sr.flipX = false;
                Right();
                if(transform.position.x > rightPoint.position.x)
                {
                    movingRight = false;
                }
            }
            else
            {
                rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
                sr.flipX = true;
                Left();
                if(transform.position.x < leftPoint.position.x)
                {
                    movingRight = true;
                }
            }

            if(moveCount <= 0)
            {
                waitCount = Random.Range(waitTime * .75f, waitTime * 1.25f);
            }
        }
        else if(waitCount > 0)
        {
            waitCount -= Time.deltaTime;
            rb.velocity = new Vector2(0f, rb.velocity.y);

            if(waitCount <= 0)
            {
                moveCount = Random.Range(moveCount * 1.75f, waitTime * 3.25f);
            }
        }
    }

    private void Attack()
    {
        if (hitbox.GetComponent<EnemyAttack>().playerInRange && hitbox.GetComponent<EnemyAttack>().canAttack)
        {
            hitbox.GetComponent<EnemyAttack>().AttackAnimation();
        }
    }
    
    public void EndAnimation()
    {
        hitbox.GetComponent<EnemyAttack>().EndAnimation();
    }

    public void DealDamage()
    {
        hitbox.GetComponent<EnemyAttack>().DealDamage();
    }

    public void Left()
    {
        hitbox.transform.localScale = new Vector3(-1f, 1f, 1f);
    }

    public void Right()
    {
        hitbox.transform.localScale = new Vector3(1f, 1f, 1f);
    }
}
