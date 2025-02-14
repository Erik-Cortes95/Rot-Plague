using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Zombie : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 4;
    [SerializeField] private float animationChangeSpeed = 8;

    private float movementInput;
    private float currentAnimationSpeed;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        transform.Translate(0, 0, movementInput * movementSpeed * Time.deltaTime);

        currentAnimationSpeed = Mathf.MoveTowards(currentAnimationSpeed, movementInput, animationChangeSpeed * Time.deltaTime);

        animator.SetFloat("Speed", movementInput);
    }

    private void OnMove(InputValue value)
    {
        movementInput = value.Get<float>();
       // animator.SetFloat("Speed", movementInput);
    }
}
