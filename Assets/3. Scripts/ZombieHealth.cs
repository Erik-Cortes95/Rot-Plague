using UnityEngine;

public class ZombieHealth : MonoBehaviour
{
    [SerializeField] private float vida = 100f;

    public void RecibirDaņo(float cantidad)
    {
        vida -= cantidad;
        if (vida <= 0) Destroy(gameObject);
    }
}

