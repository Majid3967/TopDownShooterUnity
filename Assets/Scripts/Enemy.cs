using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : LivingEntity
{
    enum State {idle,chasing,attacking};
    State currentState;
    NavMeshAgent pathFinder;
    Transform target;
    Material skinMaterial;
    Color originalColor;

    float attackDistanceThreshold = 1.5f;
    float timeBetweenAttack = 1;
    float nextAttackTime;

    float myCollisionRadius;
    float targetCollisonRadius;
    protected override void Start()
    {
        base.Start();
        currentState = State.chasing;
        pathFinder = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        myCollisionRadius = GetComponent<CapsuleCollider>().radius;
        targetCollisonRadius = target.GetComponent<CapsuleCollider>().radius;
        skinMaterial = GetComponent<Renderer>().material;
        originalColor = skinMaterial.color;
        StartCoroutine(UpdatePath());
    }
    void Update()
    {
        if(Time.time > nextAttackTime)
        {
            float sqrDstToTarget = (target.position - transform.position).sqrMagnitude;
            if(sqrDstToTarget < Mathf.Pow(attackDistanceThreshold+myCollisionRadius+targetCollisonRadius, 2))
            {
                nextAttackTime = Time.time + timeBetweenAttack;
                StartCoroutine(Attack());
            }

        }
    }
    IEnumerator Attack()
    {
        currentState = State.attacking;
        pathFinder.enabled = false;
        skinMaterial.color = Color.red;

        Vector3 originalPostion = transform.position;
        Vector3 dirToTarget = (target.position - transform.position).normalized; 
        Vector3 attackPosition = target.position -dirToTarget*(myCollisionRadius);

        float attackSpeed = 3;
        float percent = 0;
        while(percent <= 1)
        {
            percent += Time.deltaTime * attackSpeed;
            float interpolation = (-Mathf.Pow(percent, 2) + percent) * 4;
            transform.position = Vector3.Lerp(originalPostion, attackPosition, interpolation);
            yield return null;
        }
        currentState = State.chasing;
        pathFinder.enabled = true;
        skinMaterial.color = originalColor;
    }
    IEnumerator UpdatePath()
    {
        float refreshRate = .25f;
        while(target != null)
        {
            if (currentState == State.chasing)
            {
                Vector3 dirToTarget = (target.position - transform.position).normalized;
                Vector3 targetPosition = target.position - dirToTarget * (myCollisionRadius+targetCollisonRadius+attackDistanceThreshold/2);
                if (!dead)
                {
                    pathFinder.SetDestination(targetPosition);
                }
            }
            yield return new WaitForSeconds(refreshRate);
        }
    }
}
