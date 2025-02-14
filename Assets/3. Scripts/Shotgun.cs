using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shotgun : MonoBehaviour
{
    [SerializeField] private float damage = 40f;
    [SerializeField] private float fireRate = 1f; // Cadencia baja
    [SerializeField] private int pelletCount = 6; // Número de perdigones
    [SerializeField] private float spreadAngle = 10f; // Dispersión de la escopeta
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
        for (int i = 0; i < pelletCount; i++)
        {
            Vector3 spread = new Vector3(
                Random.Range(-spreadAngle, spreadAngle),
                Random.Range(-spreadAngle, spreadAngle),
                0
            );

            Quaternion rotation = Quaternion.Euler(spread) * transform.rotation;

            RaycastHit hit;
            if (Physics.Raycast(transform.position, rotation * Vector3.forward, out hit) && hit.collider.CompareTag("Disparable"))
            {
                Destroy(hit.transform.gameObject, 0.5f);
                Instantiate(bloodEffect, hit.point, Quaternion.LookRotation(hit.normal));
                print("TOMA ZORRA, escopetazo en la puta boca");
            }
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