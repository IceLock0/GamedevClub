using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class WeaponChangerManager : MonoBehaviour
{
    private Transform _parentTransform;
    private List<GameObject> _weaponList = new List<GameObject>();

    public static GameObject CurrentWeapon { get; private set; }

    private string _nameOfPistol = "Makarov";
    private string _nameOfAutomatic = "AK-74";

    private void Start()
    {
        _parentTransform = GetComponent<Transform>();
        GetChildObjects(_parentTransform);
        WeaponInitialState(_weaponList, ParentChecker(_parentTransform));
    }
    private void Update()
    {
        GetCurrentWeapon();
    }
    public void ChangeWeapon()
    {
        foreach (GameObject weapon in _weaponList)
        {
            weapon.SetActive(!weapon.activeSelf);
        }
    }

    private void GetChildObjects(Transform parentTransform)
    {
        foreach (Transform child in parentTransform)
            _weaponList.Add(child.gameObject);
    }

    private void WeaponInitialState(List<GameObject> weaponList, string nameOfWeaponToDisable)
    {
        foreach (GameObject weapon in weaponList)
        {
            if (weapon.name == nameOfWeaponToDisable)
                weapon.SetActive(false);
        }
    }

    private string ParentChecker(Transform parentTransform)
    {
        string buttonName = "WeaponButtonChanger";
        if (parentTransform.name == buttonName) return _nameOfPistol;
        return _nameOfAutomatic;
    }

    private void GetCurrentWeapon()
    {
        foreach (GameObject weapon in _weaponList)
        {
            if (weapon.TryGetComponent<Weapon>(out Weapon weaponComponent) && weapon.activeSelf)
            {
                CurrentWeapon = weapon;
            }
        }
    }
}
