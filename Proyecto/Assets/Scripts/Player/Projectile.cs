using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float damage = 15f;
    public float speed = 7f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.right * Time.deltaTime * speed * Mathf.Cos(transform.rotation.x) + transform.up * Time.deltaTime * speed * Mathf.Sin(transform.rotation.x);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy")
        {
            other.GetComponent<Health>().UpdateHealth(-damage);
            Destroy(gameObject);
        }
        else if(other.tag == "Map")
        {
            Destroy(gameObject);
        }
    }
}
