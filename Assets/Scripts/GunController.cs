using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public Transform weaponHold;
    public Gun startingWeapon;
    Gun eqiuppedGun;
    void Start()
    {
        if(startingWeapon != null)
        {
            equipWeapon(startingWeapon);
        }
    }
    public void equipWeapon(Gun gunToEquip)
    {
        if(eqiuppedGun != null)
        {
            Destroy(eqiuppedGun.gameObject);
        }
        eqiuppedGun = Instantiate(gunToEquip, weaponHold.position, weaponHold.rotation) as Gun;
        eqiuppedGun.transform.parent = weaponHold;
    }
    public void Shoot()
    {
        if(eqiuppedGun != null)
        {
            eqiuppedGun.Shoot();
        }
    }
}
