﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basic_Titan : MonoBehaviour
{
    /********************************
     *      Basic Titan Script      *
     ********************************/

    // Stores player as GameObject for reference
    private GameObject player;
    // Animator
    public Animator anm;
    // RigidBody2D
    public Rigidbody2D rb;
    // Speed at which titan walks
    public float speed;
    // Titan's health
    public float health;
    // Range at which titan will approach player from
    public float engageDistance;
    // Bool to control whether or not titan is moving
    private bool chasing = true;
    // Stores direction titan is facing (-1 = left, +1 = right)
    private int chaseDirection = -1;
    // Time it takes for titan to turn around fully
    public float turnTime;
    // Attacking
    private bool isAttacking = false;

    private bool alive = true;
    public SpriteRenderer[] pieces;
    private float opacity = 1.0f;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if (player.transform.position.x < this.transform.position.x)
            chaseDirection = -1;
        else
            chaseDirection = 1;
    }

    private void FixedUpdate()
    {
        if (player != null)
        {
            float playerDirection = player.transform.position.x - this.transform.position.x;
            var distanceFromPlayer = Mathf.Abs(playerDirection);

            // Will approach player if not attacking & player is within engagement distance
            if (distanceFromPlayer < engageDistance && !isAttacking)
            {
                // Prevents titan from simply walking into player
                if (distanceFromPlayer > 5)
                {
                    // Player is in front of titan (left-facing)
                    if (player.transform.position.x < this.transform.position.x && chaseDirection == -1)
                    {
                        rb.velocity = new Vector2(-speed, rb.velocity.y);
                        anm.SetBool("isWalking", true);
                    }
                    // Player is in front of titan (right-facing)
                    else if (player.transform.position.x > this.transform.position.x && chaseDirection == 1)
                    {
                        rb.velocity = new Vector2(speed, rb.velocity.y);
                        anm.SetBool("isWalking", true);
                    }
                }
                else
                    anm.SetBool("isWalking", false);

                // Reverses direction if player is behind titan
                if (player.transform.position.x > this.transform.position.x && chaseDirection == -1)
                {
                    StopCoroutine(ReverseDirection());
                    StartCoroutine(ReverseDirection());
                }
                // Reverses direction if player is behind titan
                else if (player.transform.position.x < this.transform.position.x && chaseDirection == 1)
                {
                    StopCoroutine(ReverseDirection());
                    StartCoroutine(ReverseDirection());
                }
            }
            else
                anm.SetBool("isWalking", false); // Will stop animator if player is not within range
        }
        else
            anm.SetBool("isWalking", false); // Will stop animator if player is dead
    }

    private void OnTriggerStay2D(Collider2D trigger)
    {
        // Will attack player if attack is not already occurring
        if (!isAttacking)
        {
            Attack();
        }
    }
    
    // Function called by animator when attack ends to reset animation state and clear isAttacking boolean
    public void EndAttack()
    {
        isAttacking = false;
        anm.SetBool("attackingHigh", false);
        anm.SetBool("attackingLow", false);
        anm.SetBool("attackingFeet", false);
    }

    // Triggered when player enters titan's attack zone
    private void Attack()
    {
        isAttacking = true;
        StopCoroutine(ReverseDirection());
        // Attacks high when player is higher up
        if (player.transform.position.y > this.transform.position.y)
        {
            anm.SetBool("attackingHigh", true);
        }
        // Attacks low when player is lower down
        else if (player.transform.position.y <= this.transform.position.y && player.transform.position.y > this.transform.position.y - 12f)
        {
            anm.SetBool("attackingLow", true);
        }
        // Kicks at player if they're really low
        else if (player.transform.position.y <= this.transform.position.y - 12f)
        {
            anm.SetBool("attackingFeet", true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!alive)
        {
            if (opacity > 0)
            {
                opacity -= Time.deltaTime * 0.15f;
                for (int i = 0; i < pieces.Length; i++)
                {
                    pieces[i].color = new Color(255, 255, 255, opacity);
                }
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }

    public void InitKill()
    {
        Destroy(this.GetComponent<BoxCollider2D>());
    }

    // Triggered at end of death animation
    public void Kill()
    {
        alive = false;
    }

    // A coroutine for flipping the titan around to prevent them from instantly turning around
    IEnumerator ReverseDirection()
    {
        yield return new WaitForSeconds(turnTime);

        if (!isAttacking)
        {
            if ((player.transform.position.x > this.transform.position.x && chaseDirection == -1) ||
                (player.transform.position.x < this.transform.position.x && chaseDirection == 1))
            {
                transform.eulerAngles += Vector3.up * 180;
                chaseDirection *= -1;
            }
        }
    }
}
