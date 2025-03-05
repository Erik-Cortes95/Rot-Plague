using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour
{
    public GameObject ObjetoMenuPausa;
    public bool Pausa = false;

    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            if(Pausa == false)
            {
                ObjetoMenuPausa.SetActive(true);
                Pausa = true;

                Time.timeScale = 0;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;

                AudioSource[] sonidos = FindObjectsOfType<AudioSource>();

                for(int i = 0; i < sonidos.Length; i++)
                {
                    sonidos[i].Pause();
                }
            }
            else if(Pausa == true)
            {
                Resumir();
            }
        }
    }

    public void Resumir()
    {
        ObjetoMenuPausa.SetActive(false);
        Pausa = false;

        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        AudioSource[] sonidos = FindObjectsOfType<AudioSource>();

        for (int i = 0; i < sonidos.Length; i++)
        {
            sonidos[i].UnPause();
        }
    }
    public void MainMenu(string NombreMenu)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(NombreMenu);
    }

    public void Exit()
    {
        Application.Quit();
    }
}

