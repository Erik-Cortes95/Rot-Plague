using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public void RestartGame()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadSceneAsync("Mapa");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Te han matado");
    }
}