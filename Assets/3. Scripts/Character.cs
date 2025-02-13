using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Character : MonoBehaviour
{
    [SerializeField] private float moveVel;
    [SerializeField] private Vector2 aimSens;
    [SerializeField] private ParticleSystem bloodEffect;

    private Rigidbody rb;

    private Vector2 moveInput;
    private Vector2 aimInput;

    private float xAngle;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        Vector3 newVelocity = new Vector3(moveInput.x * moveVel, rb.velocity.y, moveInput.y * moveVel);
        rb.velocity = transform.rotation * newVelocity;

        transform.Rotate(0, aimInput.x * aimSens.x * Time.deltaTime, 0);

        xAngle = Mathf.Clamp(xAngle - aimInput.y * aimSens.y * Time.deltaTime, -90, 90);
        transform.GetChild(0).localEulerAngles = new Vector3(xAngle, 0, 0);
    }

    private void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    private void OnAim(InputValue value)
    {
        aimInput = value.Get<Vector2>();
    }

    private void OnShoot() 
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.GetChild(0).position, transform.GetChild(0).forward, out hit)) 
        {
            Destroy(hit.transform.gameObject, 0.5f);
            Instantiate(bloodEffect, hit.point, Quaternion.LookRotation(hit.normal));
        }
        else
        {
            print("TE HAS SALIDO");
        }
    }
}
