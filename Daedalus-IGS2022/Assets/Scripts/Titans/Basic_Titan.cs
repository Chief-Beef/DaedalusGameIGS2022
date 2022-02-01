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
        float playerDirection = player.transform.position.x - this.transform.position.x;
        var distanceFromPlayer = Mathf.Abs(playerDirection);

        if (distanceFromPlayer < engageDistance)
        {
            // Player is in front of titan (left-facing)
            if (player.transform.position.x < this.transform.position.x && chaseDirection == -1)
            {
                rb.velocity = new Vector2(-speed, rb.velocity.y);
            }
            // Player is in front of titan (right-facing)
            else if ((player.transform.position.x > this.transform.position.x && chaseDirection == 1))
            {
                rb.velocity = new Vector2(speed, rb.velocity.y);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D trigger)
    {
        // Titan is facing left
        if (chaseDirection == -1)
        {
            // Player is behind titan (turn around)
            if (player.transform.position.x > this.transform.position.x)
            {
                Debug.Log("player is behind!");
                StopCoroutine(ReverseDirection());
                StartCoroutine(ReverseDirection());
            }
            // Player is in front of titan (attack player)
            else if (player.transform.position.x < this.transform.position.x)
            {
                Debug.Log("player is in front!");
                // Do attack animation
            }
        }
        // Titan is facing right
        else
        {
            // Player is behind titan (turn around)
            if (player.transform.position.x < this.transform.position.x)
            {
                Debug.Log("player is behind!");
                StopCoroutine(ReverseDirection());
                StartCoroutine(ReverseDirection());
            }
            // Player is in front of titan (attack player)
            else if (player.transform.position.x > this.transform.position.x)
            {
                Debug.Log("player is in front!");
                // Do attack animation
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ReverseDirection()
    {
        yield return new WaitForSeconds(turnTime);

        if ((player.transform.position.x > this.transform.position.x && chaseDirection == -1) ||
            (player.transform.position.x < this.transform.position.x && chaseDirection == 1))
        {
            transform.eulerAngles += Vector3.up * 180;
            chaseDirection *= -1;
        }
    }
}
