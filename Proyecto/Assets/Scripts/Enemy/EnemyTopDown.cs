using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyTopDown : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    private SpriteRenderer sr;

    [SerializeField] private GameObject enemy;
    private GameObject player;
    [SerializeField] private GameObject hitbox;


    private NavMeshAgent agent;

    private Vector2 distance;
    [SerializeField] private float distanceDetection;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!hitbox.GetComponent<EnemyAttack>().attacking)
        {
            Attack();
        }
        Animation();
        InRange();
    }

    private void Animation()
    {
        if(Mathf.Abs(distance.y) > Mathf.Abs(distance.x))
        {
            sr.flipX = false;
            anim.SetBool("Side", false);
            if (distance.y > 0)
            {
                anim.SetBool("Down", false);
                anim.SetBool("Up", true);
                anim.SetInteger("Direction", 1);
            }
            else if (distance.y < 0)
            {
                anim.SetBool("Up", false);
                anim.SetBool("Down", true);
                anim.SetInteger("Direction", 3);
            }
        }
        else if(Mathf.Abs(distance.x) > Mathf.Abs(distance.y))
        {
            anim.SetBool("Up", false);
            anim.SetBool("Down", false);
            anim.SetBool("Side", true);
            anim.SetInteger("Direction", 2);
            if (distance.x > 0)
            {
                sr.flipX = false;
                Right();
            }
            if (distance.x < 0)
            {
                sr.flipX = true;
                Left();
            }
        }
        if (distance.y == 0)
        {
            anim.SetBool("Up", false);
            anim.SetBool("Down", false);
        }
        if (distance.x == 0)
        {
            anim.SetBool("Side", false);
        }
    }

    private void InRange()
    {
        distance = player.transform.position - enemy.transform.position;
        if(distance.magnitude < distanceDetection)
        {
            agent.SetDestination(player.transform.position);
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
