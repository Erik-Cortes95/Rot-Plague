using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    [SerializeField] private Pistol pistol;
    [SerializeField] private AssaultRifle assaultRifle;
    [SerializeField] private Shotgun shotgun;

    private void OnShoot(InputValue value)
    {
        if (value.isPressed)
        {
            pistol.OnShoot();
            assaultRifle.OnShoot(value);
            shotgun.OnShoot();
        }
    }
}