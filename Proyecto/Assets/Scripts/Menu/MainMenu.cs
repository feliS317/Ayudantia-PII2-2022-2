using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public static void Play()
    {
        SceneManager.LoadScene("Nivel1", LoadSceneMode.Single);
    }

    public static void Quit() 
    {
        Application.Quit();
    }
}
