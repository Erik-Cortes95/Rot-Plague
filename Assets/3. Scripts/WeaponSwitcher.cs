using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI; // Importante para usar Image

public class WeaponSwitcher : MonoBehaviour
{
    [SerializeField] private Transform weaponHolder;
    [SerializeField] private Image weaponIconUI; // Referencia a la imagen en el Canvas
    [SerializeField] private Sprite[] weaponIcons; // Array de sprites de armas

    private int currentWeaponIndex = 0;
    private Transform[] weapons;

    private void Start()
    {
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

        // Cambiar la imagen en el UI
        if (weaponIcons.Length > index)
        {
            weaponIconUI.sprite = weaponIcons[index];
        }
    }
}