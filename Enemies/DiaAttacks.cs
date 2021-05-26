using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerScripts;
using Health_Scripts;

public class DiaAttacks : MonoBehaviour
{
    [Header("Skee Attack")]
    [SerializeField] private GameObject skeePos;
    [SerializeField] private GameObject skeeProjectile;
    [Space]

    [Header("Sticky Smoke Attack")]
    [SerializeField] private GameObject smokePos;
    [SerializeField] private GameObject smokeProjectile;
    [Space]

    private PlayerController _player;
    private HealthSystem _healthSystem;
    private HealthSystem _playerHealth;

    private void Start()
    {
        _playerHealth = FindObjectOfType<PlayerController>().GetComponent<HealthSystem>();
        _healthSystem = GetComponent<HealthSystem>();
        _player = FindObjectOfType<PlayerController>();
    }

    public void ShootSkee()
    {
        Shoot(skeeProjectile, skeePos);
    }

    public void StickySmoke()
    {
        Shoot(smokeProjectile, smokePos);
    }

    private void Shoot(GameObject gameObject, GameObject objPosition)
    {
        Instantiate(gameObject, objPosition.transform.position, Quaternion.identity);
    }
}
