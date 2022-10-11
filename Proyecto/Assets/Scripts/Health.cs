using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    private GameObject hitbox;
    [SerializeField] private bool enemy;
    private Animator anim;
    private float health;
    private bool attacked = false;
    [SerializeField] private float maxHealth = 20f;
    // Start is called before the first frame update
    void Start()
    {
        hitbox = GameObject.FindWithTag("Player").transform.Find("Hitbox").gameObject;
        anim = GetComponent<Animator>();
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(enemy && attacked)
        {
            if(!hitbox.GetComponent<Attack>().attacking)
            {
                attacked = false;
            }
        }
    }

    public void UpdateHealth(float mod)
    {
        if(!attacked)
        {
            health += mod;
            if(mod < 0 && enemy)
            {
                attacked = true;
            }
            else if(health > maxHealth)
            {
                health = maxHealth;
            }
            if(health <= 0)
            {
                Die();
            }
        }
    }

    private void Die()
    {
        anim.SetTrigger("Dead");
    }
    public void Destroy()
    {
        Destroy(gameObject);
    }
}
