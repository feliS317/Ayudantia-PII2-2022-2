using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;

    public GameObject pauseScreen;
    private Animator animator;
    private float prevSpeed;

    void Start()
    {
        animator = GameObject.FindWithTag("Player").GetComponent<Animator>();
    }
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(isPaused){
                Resume();
            }
            else{
                Pause();
            }
        }
    }

    public void Resume(){
        pauseScreen.SetActive(false);
        isPaused = false;
        Time.timeScale = 1f;
        animator.speed = prevSpeed;
    }

    public void Pause(){
        prevSpeed = animator.speed;
        pauseScreen.SetActive(true);
        isPaused = true;
        Time.timeScale = 0f;
        animator.speed = 0;
    }

    public void Save()
    {
        SaveSystem.SavePlayer(GameObject.FindWithTag("Player").GetComponent<Health>());
    }
}
