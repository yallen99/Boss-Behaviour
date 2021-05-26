using System;
using System.Collections;
using System.Collections.Generic;
using Health_Scripts;
using PlayerScripts;
using UnityEngine;
using UnityEngine.VFX;

public class ManiAttacks : MonoBehaviour
{
   [Header("Up Attack")]
   [SerializeField] private GameObject UpProjectilePos;
   [SerializeField] private GameObject UpProjectile;
   [Space]

   [Header("Mid Attack")]
   [SerializeField] private GameObject midProjectilePos;
   [SerializeField] private GameObject midProjectile;
   [Space]
   
   [Header("Low Attack")]
   [SerializeField] private GameObject wavePosition;
   [SerializeField] private GameObject wavePrefab;
   [Space]
   
   [Header("Bubble Shield")]
   [SerializeField] private VisualEffect shield;

   [Tooltip("Enable the bubble after THIS time of being in the boss range")]
   [SerializeField] private float enableBubbleAfter;
   [Tooltip("Disable the bubble after THIS time of being outside of the boss range")]
   [SerializeField] private float disableBubbleAfter;
  
   private PlayerController _player;
   private HealthSystem _healthSystem;
   private HealthSystem _playerHealth;
   private bool isShieldOn;
   private bool bubbleActive = false;
   private bool shouldDamagePlayer = false;
   
   private void Start()
   {
      shield.SetFloat("field rate", 0);
      shield.SetFloat("field aura count", 0);
      _playerHealth = FindObjectOfType<PlayerController>().GetComponent<HealthSystem>();
      _healthSystem = GetComponent<HealthSystem>();
      _player = FindObjectOfType<PlayerController>();
      isShieldOn = false;
   }

   private void Update()
   {
      _healthSystem.isShielded = isShieldOn;
      
      if (IsPlayerInBubble() && !bubbleActive)
      {
         bubbleActive = true;
         StartCoroutine(EnableBubble());
      }
      
      if (!IsPlayerInBubble() && bubbleActive )
      {
         StartCoroutine(DisableBubble());
         
      }
      
   }

   public void ShootStaffUp()
   {
      Shoot(UpProjectile, UpProjectilePos);  
   }
   public void ShootWave()
   {
    Shoot(wavePrefab, wavePosition);  
   }
   public void ShootStaffMid()
   {
      Shoot(midProjectile, midProjectilePos);
   }
   private void Shoot(GameObject gameObject, GameObject objPosition)
   {
      Instantiate(gameObject, objPosition.transform.position, Quaternion.identity);
   }

   private IEnumerator EnableBubble()
   {
      yield return new WaitForSeconds(enableBubbleAfter);
       shield.SetFloat("field rate", 10000);
       shield.SetFloat("field aura count", 1);
       isShieldOn = true;
   }
   
   IEnumerator DisableBubble()
   {
      yield return new WaitForSeconds(disableBubbleAfter);
      
      shield.SetFloat("field rate", 0);
      shield.SetFloat("field aura count", 0);

      isShieldOn = false;
      bubbleActive = false;
   }
   
   private bool IsPlayerInBubble()
   {
      var distance = Vector3.Distance(_player.transform.position, transform.position);
      return distance < 7;
   }
   
}
