using System;
using System.Collections;
using System.Collections.Generic;
using PlayerScripts;
using UnityEngine;

public class PillarDestroyer : MonoBehaviour
{
    private SpriteRenderer sprite;

    private int timer = 5;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        PlayerController player = other.gameObject.GetComponent<PlayerController>();
        PCTutorial tutorialPlayer = other.gameObject.GetComponent<PCTutorial>();
        if (player || tutorialPlayer)
        {
            StartCoroutine(DestroyCountdown());
        }
    }

    private IEnumerator DestroyCountdown()
    {
        yield return new WaitForSeconds(1);
        Debug.Log(timer);
        timer--;
        sprite.color = Color.Lerp(sprite.color, Color.red, Time.deltaTime*3);
        if (timer < 1)
        {
            sprite.sprite = Resources.Load<Sprite>("Broken Pillar");
            StopCoroutine(DestroyCountdown());
            StartCoroutine(BuildCountdown());
            GetComponent<Collider2D>().isTrigger = true;
        }
        else
        {
            StartCoroutine(DestroyCountdown());
        }

        
    }

    private IEnumerator BuildCountdown()
    {
        yield return  new WaitForSeconds(5);
        timer = 5;
        sprite.color = Color.white;
        sprite.sprite = Resources.Load<Sprite>("Pillar");
        GetComponent<Collider2D>().isTrigger = false;
    }
}
