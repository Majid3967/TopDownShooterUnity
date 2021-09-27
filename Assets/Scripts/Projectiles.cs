using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour
{
    public LayerMask collisionMask;
    public float speed = 10f;
    public float damage = 1;
    float lifeTime = 3;
    float skinWidth = .1f;
    void Start()
    {
        Destroy(gameObject, lifeTime);

        Collider[] intialCollisions = Physics.OverlapSphere(transform.position, .1f,collisionMask);
        if(intialCollisions.Length > 0)
        {
            OnHitObject(intialCollisions[0]);
        }
    }
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
        if(Physics.Raycast(ray,out hit, moveDistance+skinWidth, collisionMask, QueryTriggerInteraction.Collide))
        {
            OnHitObject(hit);
        }
    }
    void OnHitObject(RaycastHit hit)
    {
        iDamageable damageable = hit.collider.GetComponent<iDamageable>();
        if (damageable != null)
        {
            damageable.hitTaken(damage, hit);
        }
        GameObject.Destroy(gameObject);
    }
    void OnHitObject(Collider c)
    {
        iDamageable damageable = c.GetComponent<iDamageable>();
        if (damageable != null)
        {
            damageable.damageTaken(damage);
        }
        GameObject.Destroy(gameObject);
    }
}
