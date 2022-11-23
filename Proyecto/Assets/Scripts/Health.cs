using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private bool enemy;
    [SerializeField] private float maxHealth = 20f;
    
    private GameObject player;
    private GameObject hitbox;
    private Animator anim;
    public float health;
    private bool attacked = false;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
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
        if(!attacked || (!enemy && !player.GetComponent<MovementTopDown>().isDashing))
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
        if(enemy)
        {
            Doors.cantEnemy--;
        }
        Destroy(gameObject);
    }

    public void SavePlayer(){
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer(){
        SavedData data = SaveSystem.LoadPlayer();

        health = data.health;

        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        transform.position = position;
    }
}
