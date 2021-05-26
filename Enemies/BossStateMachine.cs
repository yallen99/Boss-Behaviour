using System;
using System.Collections;
using System.Collections.Generic;
using Health_Scripts;
using PlayerScripts;
using UnityEngine;
using UnityEngine.SceneManagement;
using UniversalScripts;

namespace Enemies
{
    public class BossStateMachine : MonoBehaviour
    {
        [Header("Attack Blocks")]
        [Tooltip("increase the number of blocks then attach the sequence object here to add to the possible performed actions.")]
        [SerializeField] private List<Block> blocks;
        

        private bool sequenceBegan;

        [HideInInspector] public HealthSystem healthSystem;
        
        [Space]
        [SerializeField] private HealthBarEnemy _healthBarEnemy;
        [SerializeField] private int maxHealth;
        
        private PlayerController player;
        private Randomizer blockRandomizer;
        private int attackValue;
        
        private Animator _animator;
        //animation keys
        private static readonly int dodge = Animator.StringToHash("dodge");
        private static readonly int attack_1 = Animator.StringToHash("attack 1");
        private static readonly int attack_2 = Animator.StringToHash("attack 2");
        private static readonly int isDead = Animator.StringToHash("isDead");

        private bool isDodging;
        private bool gameWon;
        
        [Space]
        [Header("UI")]
        [SerializeField] GameObject healthUI;

        [SerializeField] private string nextScene;
        private GameManager GM;
        private SceneLoader _sceneLoader;

        [Space] [Header("Rewards")] [SerializeField]
        private int shardRewards;

        [SerializeField] private string sceneToActivate;

        private bool gotRewarded;
        private void Start()
        {
            gameWon = false;
            _sceneLoader = FindObjectOfType<SceneLoader>();
            GM = FindObjectOfType<GameManager>();
            player = FindObjectOfType<PlayerController>();
            _animator = GetComponent<Animator>();
           
            //init health
            healthSystem = GetComponent<HealthSystem>();
            healthSystem.Initialize(maxHealth);
            _healthBarEnemy.Setup(healthSystem);
            
            blockRandomizer = new Randomizer(1, blocks.Count+1, 0);

            sequenceBegan = false;
            StartCoroutine(StartAttacks());

            gotRewarded = false;

        }

        private void Update()
        {
            if (IsDead() && !gameWon)
            {
                GM.hubAreas[sceneToActivate] = true;
                StartCoroutine(WinScreen());
                gameWon = true;
            }
        }
        
        #region IDLE State

        private IEnumerator StartAttacks()
        {
            //2 idle loops between the attacks
            //JUST IN THE BEGINNING
           yield return new WaitForSeconds(3);
           StartCoroutine(Attack());
        }

        /// <summary>
        /// Recursive coroutine. Is read to activate attacks based on the blocks order
        /// </summary>
        /// <returns></returns>
        private IEnumerator Attack()
        {
            attackValue = blockRandomizer.ExtractNumber();
            yield return StartCoroutine(ReadBlock(blocks[attackValue-1]));
            StartCoroutine(Attack());
        }

     
        #endregion
        
        #region DODGE State

        /*private IEnumerator Invulnerability()
        {            
            _animator.SetTrigger(dodge);
            yield return new WaitForSeconds(0.5f);
            invincible = true;
            yield return new WaitForSeconds(1.5f);
            invincible = false;
            activeState = State.IDLE;
        }*/
        
        #endregion

        #region ATTACKS

    
        
        private IEnumerator ReadBlock(Block block)
        {
            foreach (string action in block.Actions)
                {
                    _animator.SetTrigger(action);
//                    Debug.Log(action);
                    yield return new WaitForSeconds(1.5f); // EACH Attack anim has to be 1.5s
                    _animator.ResetTrigger(action);
                }
        }

        /*private IEnumerator IdleLoop(int idleTime)
        {
            Debug.Log("begin Idle state...");
            yield return new WaitForSeconds(idleTime);
            StartCoroutine(Attack());
        }*/

        #endregion
        
        public void ResetIsDodging()
        {
            isDodging = false;
        }

        private bool IsDead()
        {
            return healthSystem.IsDead();
        }

        private IEnumerator WinScreen()
        {
            if (!gotRewarded)
            {
                GM.Currency += shardRewards;
                gotRewarded = true;
            }

            _animator.SetBool(isDead, true);
            yield return new WaitForSeconds(3f);
            GM.Autosave();
            _sceneLoader.LongLoadScene(nextScene);
        }
       
    }
}