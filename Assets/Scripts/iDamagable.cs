using UnityEngine;

public interface iDamageable{
    void hitTaken(float damage, RaycastHit hit);
    void damageTaken(float damage);
}

