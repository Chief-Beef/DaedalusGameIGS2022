using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwarmScript : MonoBehaviour
{
    public Rigidbody2D rb;
    
    //move and attack speeds
    public float lungeForce;
    public float speed;

    public float lungeTimer; //prevent lunge spam

    //health is current health while max health is what it starts with
    public float health;
    public float maxHealth;

    //distance and direction floats
    public float engageDistance;
    public float attackDistance;
    public float playerDirection;
    public float playerDistance;

    public GameObject player;

    //true if right false if left
    public bool playerRight;

    public Vector2 playerAngle;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void FixedUpdate()
    {

        lungeTimer -= Time.deltaTime;

        playerDirection = player.transform.position.x - this.transform.position.x;
        playerDistance = Vector2.Distance(player.transform.position, this.transform.position);

        if (playerDistance < engageDistance && playerDistance > attackDistance)
        {
            // Player is to the left
            if (playerDirection < 0)
            {
                rb.AddForce(new Vector2(speed * -1f, 0));
                playerRight = false;
            }
            // Player is to the right
            else if (playerDirection > 0)
            {
                rb.AddForce(new Vector2(speed, 0));
                playerRight = true;
            }
        }

        if (playerDistance < attackDistance && lungeTimer <= 0)
        {
            lungeTimer = 3f;    
            Lunge();
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Lunge()
    {

        float jumpHeight = Random.Range(2.5f, 4.5f);
        playerAngle = new Vector2(player.transform.position.x - this.transform.position.x, player.transform.position.y - this.transform.position.y + jumpHeight);

        rb.AddForce(playerAngle * lungeForce);

    }

}
