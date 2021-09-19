using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform muzzle;
    public Projectiles projectiles;
    public float msBetweenShots;
    public float muzzleVelocity;

    float nextShotTime;

    public void Shoot()
    {
        if (Time.time > nextShotTime)
        {
            nextShotTime = Time.time + msBetweenShots/1000;
            Projectiles newProjectiles = Instantiate(projectiles, muzzle.position, muzzle.rotation) as Projectiles;
            newProjectiles.setSpeed(muzzleVelocity);
        }
    }
}
