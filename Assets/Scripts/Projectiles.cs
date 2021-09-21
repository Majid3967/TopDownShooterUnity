using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour
{
    public LayerMask collisionMask;
    public float speed = 10f;
    public float damage = 1;
    public void setSpeed(float newSpeed)
    {
        speed = newSpeed;
    }
    void Update()
    {
        float moveDistance = Time.deltaTime * speed;
        CheckCollision(moveDistance);
        transform.Translate(Vector3.forward * moveDistance);
    }
    void CheckCollision(float moveDistance)
    {
        Ray ray = new Ray(transform.position, Vector3.forward);
        RaycastHit hit;
        if(Physics.Raycast(ray,out hit, moveDistance, collisionMask, QueryTriggerInteraction.Collide))
        {
            OnHitObject(hit);
        }
    }
    void OnHitObject(RaycastHit hit)
    {
        iDamageable damageable = hit.collider.GetComponent<iDamageable>();
        if (damageable != null)
        {
            damageable.damageTaken(damage, hit);
        }
        GameObject.Destroy(gameObject);
    }
}
