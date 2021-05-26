using System;
using Health_Scripts;
using PlayerScripts;
using UnityEngine;

namespace Enemies
{
    public class Projectile: MonoBehaviour
    {
        /// <summary>
        /// Damage value
        /// </summary>
        [SerializeField] private int damage;
        [SerializeField] private float persistentDamage = 0;

        
        private void OnTriggerEnter2D(Collider2D other)
        {
            var player = other.GetComponent<PlayerController>();
            var playerTutorial = other.GetComponent<PCTutorial>();
            if (player)
            {
                HealthSystem health = player.GetComponent<HealthSystem>();
                health.Damage(damage);
                if (!CompareTag("Persistent Projectile"))
                {
                    Destroy(gameObject);
                }
            }
            else if (playerTutorial)
            {
                if (CompareTag("Inofensive projectile"))
                {
                    Destroy(gameObject);
                }
            }
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            var player = other.GetComponent<PlayerController>();
           
                if (player)
                {
                    if (CompareTag("Persistent Projectile")) 
                    {
                    HealthSystem health = player.GetComponent<HealthSystem>();
                    health.Damage(persistentDamage); 
                    }
            }
        }
        private void OnCollisionStay2D(Collision2D other)
        {
            if (CompareTag("Persistent Projectile"))
            {
                var player = other.gameObject.GetComponent<PlayerController>();
                if (player)
                {
                    HealthSystem health = player.GetComponent<HealthSystem>();
                    health.Damage(persistentDamage);
                }
            }
        }
    }
}