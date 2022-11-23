using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;

    public GameObject pauseScreen;

    
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

    void Resume(){
        pauseScreen.SetActive(false);
        isPaused = false;
        Time.timeScale = 1f;
    }

    void Pause(){
        pauseScreen.SetActive(true);
        isPaused = true;
        Time.timeScale = 0f;
    }
}
