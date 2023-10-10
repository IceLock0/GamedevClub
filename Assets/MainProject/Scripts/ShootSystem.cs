using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootSystem : MonoBehaviour
{
    private Transform _target;

    public void Fire()
    {
        GameObject weaponObject = WeaponChangerManager.CurrentWeapon;
        Weapon weapon = weaponObject.GetComponent<Weapon>();
        bool isCanShoot = weapon.Shoot();
        _target = TargetSystem._closestEnemy;
        if (_target != null)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, _target.position - transform.position);
            if (hit.collider != null)
            {             
                if (hit.transform.GetComponent<Enemy>())
                {
                    Enemy enemy = hit.transform.GetComponent<Enemy>();
                    enemy.ApplyDamage(isCanShoot ? weapon.Attack() : 0);
                }
            }
        }
    }
}
