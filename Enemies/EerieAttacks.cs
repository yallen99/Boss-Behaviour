using Health_Scripts;
using PlayerScripts;
using UnityEngine;
using UnityEngine.VFX;

namespace Enemies
{
    public class EerieAttacks : MonoBehaviour
    {
        [Header("Orb Attack")]
        [SerializeField] private GameObject orbPosition;
        [SerializeField] private GameObject orbPrefab;
        [SerializeField] private GameObject orbPrefab2;
        [Space]

        [Header("Tornado Attack")]
        [SerializeField] private GameObject rayPosition;
        [SerializeField] private GameObject rayPrefab;
        [Space]

        [Header("Dive Attack")]
        public Transform divePoint;
        public LayerMask playerLayers;
        public float attackRange = 0.5f; //range of attack (short - close combat)
        public int attackDamage = 40;

        [Space] [Header("Branches Link")] [SerializeField]
        private Animator[] branchesAnimators;
        
        
        
        private PlayerController _player;
        private HealthSystem _healthSystem;
        private bool shouldDamagePlayer = false;


        private void Start()
        {
            _healthSystem = GetComponent<HealthSystem>();
            _player = FindObjectOfType<PlayerController>();
        }

        public void ShootOrb()
        {
            Shoot(orbPrefab, orbPosition);
        }

        public void ShootOrb2()
        {
            Shoot(orbPrefab2, orbPosition);
        }


        public void ShootRay()
        {
            Shoot(rayPrefab, rayPosition);
        }

        private void Shoot(GameObject gameObject, GameObject objPosition)
        {
            Instantiate(gameObject, objPosition.transform.position, Quaternion.identity);
        }

        public void DiveAttack()
        {

            Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(divePoint.position, attackRange, playerLayers); //creates a circle radius from our game object and detects if any enemies collide

            foreach (Collider2D player in hitPlayers)
            {
                player.GetComponent<HealthSystem>().Damage(attackDamage);

            }
        }

        //Animation event for IDLE & TELL states (exhaustion phase)
        public void Roses()
        {
            foreach (var branch in branchesAnimators)
            {
                branch.SetBool("exhausted", true);
            }
        }
        
        //Animation Event for ATTACKS

        public void Thorns()
        {
            foreach (var branch in branchesAnimators)
            {
                branch.SetBool("exhausted", false);
            }
        }


        private int branchValue1;
        private int branchValue2;
        
        //Certain Branches only!
        public void Wiggle()
        {
            branchValue1 = Random.Range(0, 5);
            branchValue2 = Random.Range(0, 5);
            Animator branch1 = (Animator) branchesAnimators.GetValue(branchValue1);
            branch1.SetTrigger("wiggle");
            
            Animator branch2 = (Animator) branchesAnimators.GetValue(branchValue2);
            branch2.SetTrigger("wiggle");
        }
        public void BranchAtk()
        {
            Animator branch1 = (Animator) branchesAnimators.GetValue(branchValue2);
            branch1.SetTrigger("attack");
            
            Animator branch2 = (Animator) branchesAnimators.GetValue(branchValue1);
            branch2.SetTrigger("attack");
        }
        
    }
}
