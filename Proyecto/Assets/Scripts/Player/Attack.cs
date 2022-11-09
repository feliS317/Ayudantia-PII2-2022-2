using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [Header("Componentes")]
    [SerializeField] private BoxCollider2D hitbox;
    [SerializeField] private Animator animator;
    [Header("Proyectil")]
    [SerializeField] private Projectile ProjectilePrefab;
    [SerializeField]private Transform launchOffset;
    [Header("Values")]
    [SerializeField] private float damage = 10f;
    [SerializeField] private float atkCooldown = 0.8f;
    [SerializeField] private float fireCooldown = 1f;
    public bool attacking = false;
    private bool canAttack = true;
    private bool canFire = true;
    [SerializeField] AudioClip[] sfx;
    [SerializeField] AudioController controller;
    int randomSfx;
    
    // Start is called before the first frame update
    void Start()
    {
        controller = GameObject.Find("Audio").GetComponent<AudioController>();
        hitbox.enabled = false;
    }

    // Update is called once per frame
    public void Left()
    {
        hitbox.transform.localScale = new Vector3(-1f, 1f, 1f);
        launchOffset.transform.localScale = new Vector3(-1f, 1f, 1f);
    }

    public void Right()
    {
        hitbox.transform.localScale = new Vector3(1f, 1f, 1f);
        launchOffset.transform.localScale = new Vector3(-1f, 1f, 1f);
    }

    public void AttackAnimation()
    {
        if(canAttack)
        {
            randomSfx = Random.Range(0, 3);
            controller.PlaySfx(sfx[randomSfx]);
            attacking = true;
            hitbox.enabled = true;
            animator.SetBool("isAttacking", true);
        }
    }

    public void ProjectileAnimation()
    {
        if(canFire)
        {
            animator.SetBool("isFiring", true);
        }
        
    }

    public void ShootProjectile()
    {
        Instantiate(ProjectilePrefab, launchOffset.position, launchOffset.rotation);
        Instantiate(ProjectilePrefab, launchOffset.position, launchOffset.rotation * Quaternion.Euler(0f, 10f, 0f));
        Instantiate(ProjectilePrefab, launchOffset.position, launchOffset.rotation * Quaternion.Euler(0f, 350f, 0f));
    }

    public void EndAttack()
    {
        animator.SetBool("isAttacking", false);
        attacking = false;
        hitbox.enabled = false;
        StartCoroutine(AttackCooldown(atkCooldown));
    }

    public void EndFiring()
    {
        animator.SetBool("isFiring", false);
        StartCoroutine(FireCooldown(fireCooldown));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy" && attacking == true)
        {
            other.GetComponent<Health>().UpdateHealth(-damage);
        }
    }

    public IEnumerator AttackCooldown(float atkCooldown)
    {
        canAttack = false;
        yield return new WaitForSeconds(atkCooldown);
        canAttack = true;
    }
    public IEnumerator FireCooldown(float fireCooldown)
    {
        canFire = false;
        yield return new WaitForSeconds(fireCooldown);
        canFire = true;
    }
}