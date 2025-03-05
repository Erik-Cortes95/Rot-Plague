using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public float health = 100f;  // Salud inicial del jugador
    public GameObject gameOverMenu;  // Referencia al Canvas del men� de Game Over

    public void TakeDamage(float damage)
    {
        health -= damage;

        Debug.Log("�Te han hecho " + damage + " de da�o!");
        Debug.Log("Salud restante: " + health);

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("�Has muerto!");

        // Pausar el tiempo (detener el juego)
        Time.timeScale = 0f;

        // Cargar la escena de Game Over
        SceneManager.LoadScene("GameOver");

        // Hacer visible el cursor y permitir que interact�e con los botones
        Cursor.visible = true;  // Asegura que el cursor sea visible
        Cursor.lockState = CursorLockMode.None;  // Desbloquea el cursor (no estar� bloqueado al centro)
    }
}