using System.Collections;
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

            if (distanceFromPlayer < engageDistance && !isAttacking)
            {
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

                if (player.transform.position.x > this.transform.position.x && chaseDirection == -1)
                {
                    StopCoroutine(ReverseDirection());
                    StartCoroutine(ReverseDirection());
                }
                else if (player.transform.position.x < this.transform.position.x && chaseDirection == 1)
                {
                    StopCoroutine(ReverseDirection());
                    StartCoroutine(ReverseDirection());
                }
            }
            else
                anm.SetBool("isWalking", false);
        }
        else
            anm.SetBool("isWalking", false);
    }

    private void OnTriggerStay2D(Collider2D trigger)
    {
        if (!isAttacking)
        {
            Attack();
        }
    }
    
    public void EndAttack()
    {
        isAttacking = false;
        anm.SetBool("attackingHigh", false);
        anm.SetBool("attackingLow", false);
        anm.SetBool("attackingFeet", false);
        Debug.Log("end");
    }

    private void Attack()
    {
        isAttacking = true;
        StopCoroutine(ReverseDirection());
        if (player.transform.position.y > this.transform.position.y)
        {
            anm.SetBool("attackingHigh", true);
        }
        else if (player.transform.position.y <= this.transform.position.y && player.transform.position.y > this.transform.position.y - 10f)
        {
            anm.SetBool("attackingLow", true);
        }
        else if (player.transform.position.y <= this.transform.position.y - 10f)
        {
            anm.SetBool("attackingFeet", true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
