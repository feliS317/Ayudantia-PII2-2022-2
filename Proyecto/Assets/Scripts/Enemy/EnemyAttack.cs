using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public bool playerInRange;
    public bool canAttack;
    public bool attacking;
    public GameObject enemy;
    private GameObject player;
    [SerializeField] private float enemyCooldown = 2f;
    [SerializeField] private float damage = 5f;
    [SerializeField] private Animator animator;

    void Start()
    {
        canAttack = true;
        attacking = false;
        playerInRange = false;
        player = GameObject.FindWithTag("Player");
    }

    void OnTriggerEnter2D(Collider2D other) // Si entra al rango de ataque, indicar que el jugador esta en rango
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInRange = true;

        }
    }

    void OnTriggerExit2D(Collider2D other) // Si sale del rango de ataque, indicar que el jugador no esta en rango
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    public IEnumerator AttackCooldown(float cooldown) // Cooldown entre ataque
    {
        canAttack = false;
        yield return new WaitForSeconds(cooldown);
        canAttack = true;
    }

    public void AttackAnimation()
    {
        animator.SetBool("isAttacking", true);
        attacking = true;
    }

    public void EndAnimation()
    {
        animator.SetBool("isAttacking", false);
        StartCoroutine(AttackCooldown(enemyCooldown));
        attacking = false;
    }

    public void DealDamage()
    {
        if (playerInRange)
        {
            player.GetComponent<Health>().UpdateHealth(-damage);
        }
    }
}
