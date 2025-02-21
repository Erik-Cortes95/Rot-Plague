using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public float health = 100f;  // Salud inicial del jugador
    public float damagePerHit = 5f;  // Daño que el jugador recibe por cada golpe
    public float damageInterval = 1f;  // Intervalo de tiempo entre cada golpe

    public void TakeDamage(float damage)
    {
        health -= damage;

        Debug.Log("¡Te han hecho " + damage + " de daño!");
        Debug.Log("Salud restante: " + health);

        if (health <= 0)
        {
            Die(); 
        }
    }
    private void Die()
    {
        Debug.Log("¡Has ha muerto!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.CompareTag("arma"))
        {
            print("Daño");
        }
    }
}