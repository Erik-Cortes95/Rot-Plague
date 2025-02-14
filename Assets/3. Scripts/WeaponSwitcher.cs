using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponSwitcher : MonoBehaviour
{
    [SerializeField] private Transform weaponHolder; // Padre de todas las armas
    private int currentWeaponIndex = 0;
    private Transform[] weapons;

    private void Start()
    {
        // Obtener todas las armas como hijos del weaponHolder
        int weaponCount = weaponHolder.childCount;
        weapons = new Transform[weaponCount];
        for (int i = 0; i < weaponCount; i++)
        {
            weapons[i] = weaponHolder.GetChild(i);
        }
        SelectWeapon(currentWeaponIndex);
    }

    private void OnSwitchWeapon(InputValue value)
    {
        float scrollInput = value.Get<float>();
        if (scrollInput > 0)
        {
            currentWeaponIndex = (currentWeaponIndex + 1) % weapons.Length;
        }
        else if (scrollInput < 0)
        {
            currentWeaponIndex = (currentWeaponIndex - 1 + weapons.Length) % weapons.Length;
        }
        SelectWeapon(currentWeaponIndex);
    }

    private void SelectWeapon(int index)
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].gameObject.SetActive(i == index);
        }
    }
}

