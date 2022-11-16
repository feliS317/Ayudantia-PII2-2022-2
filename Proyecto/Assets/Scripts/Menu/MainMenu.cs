using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private string level;

    public void Play()
    {
        SceneManager.LoadScene(level, LoadSceneMode.Single);
    }

    public void Quit() 
    {
        Application.Quit();
    }
}
