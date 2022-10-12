using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTopDown : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    private SpriteRenderer sr;

    [SerializeField] private GameObject enemy;
    private GameObject player;

    private UnityEngine.AI.NavMeshAgent agent;

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
            }
            else if (distance.y < 0)
            {
                anim.SetBool("Up", false);
                anim.SetBool("Down", true);
            }
        }
        else if(Mathf.Abs(distance.x) > Mathf.Abs(distance.y))
        {
            anim.SetBool("Up", false);
            anim.SetBool("Down", false);
            if (distance.x > 0)
            {
                anim.SetBool("Side", true);
                sr.flipX = false;
            }
            if (distance.x < 0)
            {
                anim.SetBool("Side", true);
                sr.flipX = true;
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
        distance = enemy.transform.position - player.transform.position;
        if(distance.magnitude < distanceDetection)
        {
            agent.SetDestination(player.transform.position);
        }   
    }
}
