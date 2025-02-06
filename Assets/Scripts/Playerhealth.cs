using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float health = 100f;  // Salud inicial del jugador
    public float damagePerHit = 5f;  // Daño que el jugador recibe por cada golpe
    public float damageInterval = 1f;  // Intervalo de tiempo entre cada golpe

    private bool isTakingDamage = false;  // Para controlar si el jugador está siendo dañado

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
    }

    public void StartTakingDamage()
    {
        if (!isTakingDamage)
        {
            StartCoroutine(DamageOverTime()); 
        }
    }

    private System.Collections.IEnumerator DamageOverTime()
    {
        isTakingDamage = true;

        while (health > 0)
        {
            TakeDamage(damagePerHit);  
            yield return new WaitForSeconds(damageInterval); 
        }

        isTakingDamage = false;
    }
}