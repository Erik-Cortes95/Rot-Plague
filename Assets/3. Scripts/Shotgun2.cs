using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun2 : MonoBehaviour
{
    [SerializeField] private float damage = 40f;
    [SerializeField] private float fireRate = 1f;
    [SerializeField] private int pelletCount = 6;
    [SerializeField] private float spreadAngle = 10f;
    [SerializeField] private ParticleSystem bloodEffect;

    private bool canShoot = true;

    public void OnShoot()
    {
        Debug.Log("Disparo detectado"); // Ver si OnShoot se llama

        if (canShoot)
        {
            Shoot();
            canShoot = false;
            StopAllCoroutines();
            StartCoroutine(ResetShoot());
        }
    }

    private void Shoot()
    {
        Debug.Log("Disparo realizado"); // Ver si Shoot() se llama cada vez

        for (int i = 0; i < pelletCount; i++)
        {
            Vector3 spread = new Vector3(
                Random.Range(-spreadAngle, spreadAngle),
                Random.Range(-spreadAngle, spreadAngle),
                0
            );

            Quaternion rotation = Quaternion.Euler(spread) * transform.rotation;

            RaycastHit hit;
            if (Physics.Raycast(transform.parent.position, transform.parent.forward, out hit) && !hit.collider.CompareTag("Escenario"))
            {
                Destroy(hit.transform.gameObject, 0.5f);
                Instantiate(bloodEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Debug.Log("Escopetazo en la boca"); // Ver si impacta el disparo
            }
        }
    }

    private IEnumerator ResetShoot()
    {
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
        Debug.Log("Escopeta lista para disparar"); // Ver si se vuelve a habilitar
    }

    public float GetDamage()
    {
        return damage;
    }
}
