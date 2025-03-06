using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    [SerializeField] private float moveVel = 5f;
    [SerializeField] private float jumpHeight = 20f;
    [SerializeField] private Vector2 aimSens;
    [SerializeField] private ParticleSystem bloodEffect;
    [SerializeField] private TextMeshProUGUI limites;

    private Rigidbody rb;

    private Vector2 moveInput;
    private Vector2 aimInput;

    private float xAngle;
    private bool canJump = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("InvisibleWall")) 
        {
            limites.text ="Cuando alguien quiere bajar de un edificio, normalmente lo hace por las escaleras";
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("InvisibleWall"))
        {
            limites.text = "Llega al aparcamiento y acaba con el líder zombi";
        }
    }
    void Update()
    {
        // Moverse
        Vector3 newVelocity = new Vector3(moveInput.x * moveVel, rb.velocity.y, moveInput.y * moveVel);
        rb.velocity = transform.rotation * newVelocity;

        //Apuntar
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

    private void OnJump()
    {
        if (canJump)
        {
            rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
            canJump = false;
            Invoke(nameof(ResetJump), 2f);
        }
    }

    private void ResetJump()
    {
        canJump = true;
    }
}
