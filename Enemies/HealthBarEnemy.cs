using UnityEngine;

namespace Health_Scripts
{
    public class HealthBarEnemy : MonoBehaviour
    {
        [SerializeField] private HealthSystem healthSystem;
        [SerializeField] private GameObject healthBar;

        public void Setup(HealthSystem health)
        {
            healthSystem = health;
            healthSystem.OnHealthChanged += HealthSystem_OnHealthChanged;
        }

        private void HealthSystem_OnHealthChanged(object sender, System.EventArgs e)
        {
            healthBar.transform.localScale = new Vector3(healthSystem.GetHealthPercent(), 1);
        }

        private void Update()
        {
            if (healthBar == null || healthSystem == null)
            {
                return;
            }
            else
            {
                healthBar.transform.localScale = new Vector3(healthSystem.GetHealthPercent(), 1, 0);

            }

        }
    }
}