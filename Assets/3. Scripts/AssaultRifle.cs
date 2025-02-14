using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class AssaultRifle : MonoBehaviour
{
    [SerializeField] private float damage = 15f;
    [SerializeField] private float fireRate = 0.1f; // Disparo rápido
    [SerializeField] private ParticleSystem bloodEffect;

    private bool isShooting = false;
    private Coroutine shootingCoroutine;

    public void OnShoot(InputValue value)
    {
        if (value.isPressed)
        {
            if (shootingCoroutine == null)
            {
                shootingCoroutine = StartCoroutine(ShootContinuously());
            }
        }
        else
        {
            if (shootingCoroutine != null)
            {
                StopCoroutine(shootingCoroutine);
                shootingCoroutine = null;
            }
        }
    }

    private IEnumerator ShootContinuously()
    {
        while (true)
        {
            Shoot();
            yield return new WaitForSeconds(fireRate);
        }
    }

    private void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit) && hit.collider.CompareTag("Disparable"))
        {
            Destroy(hit.transform.gameObject, 0.5f);
            Instantiate(bloodEffect, hit.point, Quaternion.LookRotation(hit.normal));
            print("Ráfaga de fusil");
        }
    }

    public float GetDamage()
    {
        return damage;
    }
}