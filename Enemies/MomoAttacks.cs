using System.Collections;
using UnityEngine;
using Health_Scripts;
using PlayerScripts;
using UnityEngine.VFX;

public class MomoAttacks : MonoBehaviour
{

    [Header("Mid Attack")]
    [SerializeField] private GameObject spitPosition;
    [SerializeField] private GameObject[] spits;
    [Space]

    [Header("High Attack")]
    [SerializeField] private GameObject tonguePosition;
    [SerializeField] private GameObject tonguePrefab;
    [Space]

    [Header("Melee Attack")]
    public Transform meleePoint;
    public LayerMask playerLayers;
    public float attackRange = 0.5f; //range of attack (short - close combat)
    public int attackDamage = 40;

    [Space] [Header("Bubble")] [SerializeField]
    private VisualEffect shield;
    [SerializeField] private int enableBubbleAfter;
    [SerializeField] private int disableBubbleAfter;
    
    
    private PlayerController _player;
    private HealthSystem _healthSystem;
    private bool shouldDamagePlayer = false;
    private bool isShieldOn;
    private bool bubbleActive;

    private void Start()
    {
        _healthSystem = GetComponent<HealthSystem>();
        _player = FindObjectOfType<PlayerController>();
        shield.SetFloat("speed", 0.5f);
        shield.SetFloat("bubble rate", 10);
    }



    public void ShootSpit()
    {
        GameObject randomObj = spits[(int)Random.Range(0, 3)];
        Shoot(randomObj, spitPosition);
    }
    

    public void ShootTongue()
    {
        Shoot(tonguePrefab, tonguePosition);
    }

    private void Shoot(GameObject gameObject, GameObject objPosition)
    {
        Instantiate(gameObject, objPosition.transform.position, Quaternion.identity);
    }

    public void MeleeAttack() 
    {
        
        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(meleePoint.position, attackRange, playerLayers); //creates a circle radius from our game object and detects if any enemies collide
        
        foreach (Collider2D player in hitPlayers) 
        {
            player.GetComponent<HealthSystem>().Damage(attackDamage);
          
        }
        
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
    
    private IEnumerator EnableBubble()
    {
        yield return new WaitForSeconds(enableBubbleAfter);
        shield.SetFloat("speed", 5);
        shield.SetFloat("bubble rate", 1000);

        isShieldOn = true;
    }
   
    IEnumerator DisableBubble()
    {
        yield return new WaitForSeconds(disableBubbleAfter);
      
        shield.SetFloat("speed", 0.5f);
        shield.SetFloat("bubble rate", 10);   

        isShieldOn = false;
        bubbleActive = false;
    }
   
    private bool IsPlayerInBubble()
    {
        var distance = Vector3.Distance(_player.transform.position, transform.position);
        return distance < 7;
    }
    
    

}