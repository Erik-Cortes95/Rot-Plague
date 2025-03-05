using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Pistol : MonoBehaviour
{
    [SerializeField] private float damage = 25f;
    [SerializeField] private float fireRate = 0.2f;
    [SerializeField] private ParticleSystem bloodEffect;
    [SerializeField] private ParticleSystem impactEffect;
    private AudioSource shootSound;
    [SerializeField] private ParticleSystem disparoFX;
    [SerializeField] private Transform disparoPoint; // Punto donde aparece el efecto de disparo

    private bool canShoot = true;

    private void Awake()
    {
        shootSound = GetComponent<AudioSource>();
    }
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
        shootSound.Play();
        if (disparoFX != null && disparoPoint != null)
        {
            Instantiate(disparoFX, disparoPoint.position, disparoPoint.rotation); // Genera el efecto en la boca del cañón
        }

        RaycastHit hit;
        if (Physics.Raycast(transform.parent.position, transform.parent.forward, out hit) && !hit.collider.CompareTag("Escenario"))
        {
            Destroy(hit.transform.gameObject, 0.5f);
            Instantiate(bloodEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));

            Debug.Log("Disparo de pistola");
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
