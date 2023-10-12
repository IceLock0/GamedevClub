using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Weapon : MonoBehaviour, IAttackerable
{
    [SerializeField] private int _ammo = 20;
    [SerializeField] private float _damage = 10.0f;
    [SerializeField] private float _fireRate = 0.1f;
    [SerializeField] private float _nextFireTime = 0.0f;

    public float Attack() => _damage;

    public bool Shoot()
    {
        if (_ammo > 0 && Time.time > _nextFireTime)
        {
            _nextFireTime = Time.time + _fireRate;
            _ammo--;
            return true;
        }
        else return false;
    }
}
