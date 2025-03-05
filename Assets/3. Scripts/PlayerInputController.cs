using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    [SerializeField] private Pistol pistol;
    [SerializeField] private Shotgun shotgun;

    private void OnShoot(InputValue value)
    {
        if (value.isPressed)
        {
            pistol.OnShoot();
            shotgun.OnShoot();
        }
    }
}