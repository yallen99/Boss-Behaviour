using System.Collections;
using Health_Scripts;
using PlayerScripts;
using UnityEngine;

namespace Enemies
{
  public class BubbleWrap : MonoBehaviour
  {
    private HealthSystem playerHealth;
    private Coroutine damaging;
    private bool shouldDamage;

    [SerializeField] private float bubbleDamage = 1f;
 
    private void Start()
    {
      playerHealth = FindObjectOfType<PlayerController>().GetComponent<HealthSystem>();
    }

    private void Update()
    {
      if (gameObject.GetComponent<CircleCollider2D>().bounds.Contains(playerHealth.transform.position))
      {
        shouldDamage = true;
      }
      else
      {
        shouldDamage = false;
      }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
      var player = other.GetComponent<PlayerController>();
      if (player)
      {
 //       Debug.Log("damaging");
        damaging = StartCoroutine(DamageTheIntruder());
      }
    }

    IEnumerator DamageTheIntruder()
    {
    
      playerHealth.Damage(bubbleDamage);
      yield return new WaitForSeconds(0.2f);
      if (shouldDamage)
      {
        StartCoroutine(DamageTheIntruder());
      }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
      var player = other.GetComponent<PlayerController>();
      if (player)
      {
        StopCoroutine(damaging);
      }
    }
  }
}

