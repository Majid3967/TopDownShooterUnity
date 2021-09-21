﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour,iDamageable
{
    public float startingHealth;
    protected float health;
    protected bool dead;

    protected virtual void Start()
    {
        health = startingHealth;
    }

    public void damageTaken(float damage,RaycastHit hit)
    {
        health -= damage;
        if(!dead && health <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        dead = true;
        GameObject.Destroy(gameObject);
    }
}