using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerScripts;
using Health_Scripts;

public class DiaProjectile : MonoBehaviour
{
  
    [SerializeField] private int damage;

    private void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.GetComponent<PlayerController>();
        if (player)
        {
            HealthSystem health = player.GetComponent<HealthSystem>();
            health.Damage(damage);
        }

    }
}
