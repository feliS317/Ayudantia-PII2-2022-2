using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
     [SerializeField] public Animator animator;
     [SerializeField] private GameObject collider;
     [SerializeField] private GameObject spawner;
     [SerializeField] private GameObject items;
     [SerializeField] Collider2D area;
     [SerializeField] private bool spawn;
     public bool roomFinished = false;
     [Header("BossFight")]
     [SerializeField] private bool bossFight;
     [SerializeField] private AudioClip bossMusic;
     public static int cantEnemy;
     [Header("Players")]
     public static Collider2D player1;



    void Start()
    {
          cantEnemy = 0;
          if(items != null)
          {
               items.SetActive(false);
          } 
    }

    void Update()
    {
          if(cantEnemy == 0)
          {
              animator.SetBool("Open", true);
              collider.GetComponent<BoxCollider2D>().enabled = false;
          }
          else
          {
               animator.SetBool("Open", false);
               collider.GetComponent<BoxCollider2D>().enabled = true;
          }
          if(spawn)
          {
               if(spawner.activeSelf && cantEnemy == 0)
               {
                    roomFinished = true;
               }
               if(!roomFinished && cantEnemy == 0)
               {
                    OnePlayerDoors();
               }
               if(roomFinished)
               {
                    if(items != null)
                    {
                         items.SetActive(true);
                    }    
               }
          }
    }
    private void OnePlayerDoors()
    {
          if(area.IsTouching(player1))
          {
               spawner.SetActive(true);
               if(bossFight)
               {
                    /*
                    AudioSource audio = GameObject.Find("BackgroundMusic").GetComponent<AudioSource>();
                    audio.clip = bossMusic;
                    audio.Play();
                    */
               }
          }
    }
}