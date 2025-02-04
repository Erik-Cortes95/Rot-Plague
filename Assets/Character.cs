using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Character : MonoBehaviour
{
    [SerializeField] private float moveVel;
    [SerializeField] private Vector2 aimSens;

    private Rigidbody rb;

    private Vector2 moveInput;
    private Vector2 aimInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        Vector3 newVelocity = new Vector3(moveInput.x * moveVel, rb.velocity.y, moveInput.y * moveVel);
        rb.velocity = newVelocity;

        transform.Rotate(0, aimInput.x * aimSens.x * Time.deltaTime, 0);
        transform.GetChild(0).Rotate(-aimInput.y * aimSens.y * Time.deltaTime, 0, 0);
    }

    private void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    private void OnAim(InputValue value)
    {
        aimInput = value.Get<Vector2>();
    }
}
