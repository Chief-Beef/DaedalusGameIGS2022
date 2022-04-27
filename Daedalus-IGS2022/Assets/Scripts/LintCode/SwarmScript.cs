using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwarmScript : MonoBehaviour
{
    //rigidbody
    public Rigidbody2D rb;
    
    //move and attack speeds
    public float lungeForce;
    public float speed;

    private float lungeTimer; //prevent lunge spam
    private float missileTimer;//prevent missile spam
    
    //distance and direction floats
    public float engageDistance;    //when you see the player and chase them
    public float attackDistance;    //when you attack the player
    private float playerDirection;   //left or right
    private float playerDistance;    //distance from player

    //player location and detection
    private GameObject player;

    //LayerMasks
    public LayerMask ground;
    public LayerMask playerMask;

    //Wall Detection Variables
    private RaycastHit2D wallRay;
    private RaycastHit2D playerRay;
    private float wallDistance;
    private Vector2 wallNormal;
    private float wallAngle;

    public Vector2 playerAngle;
    public GameObject missile;
    public Transform missileTube;
    public GameObject fart;
    public Animator anm;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void FixedUpdate()
    {
        //Reset Lunge Timer
        //lungeTimer -= Time.deltaTime;
        missileTimer -= Time.deltaTime;

        //Find Player
        if (player.activeInHierarchy && player != null)
        {
            playerDirection = player.transform.position.x - this.transform.position.x;
            playerDistance = Vector2.Distance(player.transform.position, this.transform.position);

            /* if the player is close enought to be chased but not yet in attack distance
            if (playerDistance < attackDistance && lungeTimer < 0f)
            {
                Lunge();    //Jump at and Attack Player
                Debug.Log("Lunge");
            }*/

            if (playerDistance < engageDistance)
            {
                //Missile launch stuff
                //instantiate missile and do the rest of the work in the missile code
                if (missileTimer < 0f && playerDistance < attackDistance)
                    fireMissile();

                // Player is to the left
                if (playerDirection < 0 && playerDistance > 10f)
                {
                    //give it speed and boxcast to detect walls
                    rb.AddForce(new Vector2(speed * -1f, 0));
                    wallRay = Physics2D.Raycast(this.transform.position, Vector2.left, 20f, ground);

                    anm.Play("Walk");

                    transform.eulerAngles = Vector3.zero;
                }
                // Player is to the right
                else if (playerDirection > 0 && playerDistance > 10f)
                {
                    //give it speed and boxcast to detect walls
                    rb.AddForce(new Vector2(speed, 0));
                    wallRay = Physics2D.Raycast(this.transform.position, Vector2.right, 20f, ground);

                    anm.Play("Walk");

                    transform.eulerAngles = Vector3.up * 180;
                }
                else
                    anm.Play("Idle");
            }

            playerAngle = new Vector2(player.transform.position.x - this.transform.position.x, player.transform.position.y - this.transform.position.y);
            playerRay = Physics2D.Raycast(this.transform.position, playerAngle, 20f, ground);

            wallAngle = wallRay.normal.x;

            if (wallAngle == 1.0f && wallRay.distance <= 2.0f)
            {
                climbWall();
            }
        }
    }

    public void Fart()
    {
        Instantiate(fart, this.transform.position, Quaternion.identity, null);
    }

    //Lunge Attack
    public void Lunge()
    {
        //Add RNG to lunge for fun
        float jumpHeight = Random.Range(2.5f, 4.5f);

        //calc angle of jump then launch Lint at player at that angle
        playerAngle = new Vector2(player.transform.position.x - this.transform.position.x, player.transform.position.y - this.transform.position.y + jumpHeight);
        rb.AddForce(playerAngle * lungeForce);

        //reset lunge timer
        lungeTimer = 3f;
    }

    public void climbWall()
    {
        //
        rb.AddForce(new Vector2(0, speed*2));
    }

    public void fireMissile()
    {
        if (player.activeInHierarchy && player != null)
        {
            Instantiate(missile, missileTube.position + Vector3.up * 1.5f, Quaternion.identity, null);
            //Debug.Log("Missile Launch");
            missileTimer = 7.5f;
        }
    }

}
