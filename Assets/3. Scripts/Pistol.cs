using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Pistol : MonoBehaviour
{
    [SerializeField] private float damage = 25f;
    [SerializeField] private float fireRate = 0.2f; // Tiempo entre disparos (alta cadencia)
    [SerializeField] private ParticleSystem bloodEffect;

    private bool canShoot = true;

    public void OnShoot()
    {
        if (canShoot)
        {
            Shoot();
            canShoot = false;
            StartCoroutine(ResetShoot());
        }
    }

    private void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit) && hit.collider.CompareTag("Disparable"))
        {
            Destroy(hit.transform.gameObject, 0.5f);
            Instantiate(bloodEffect, hit.point, Quaternion.LookRotation(hit.normal));
            print("Disparo de pistola");
        }
    }

    private IEnumerator ResetShoot()
    {
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }

    public float GetDamage()
    {
        return damage;
    }
}