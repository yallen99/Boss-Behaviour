using Health_Scripts;
using UnityEngine;

namespace Enemies
{
    public class JumpDamage : MonoBehaviour
    {
        public int damage;

        private void OnTriggerEnter2D(Collider2D other)
        {
            HealthSystem player = other.GetComponent<HealthSystem>();
            if (player)
            {
                player.Damage(damage);
            }
        }
    }
}
