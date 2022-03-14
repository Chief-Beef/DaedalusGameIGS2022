using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScript : MonoBehaviour
{
    //Instace Call
    public static DeathScript Instance;
    //Camera Stuff
    public Camera cam;

    //Player position 
    public Transform playerPos;
    public GameObject player;

    //Physics stuff
    public Rigidbody2D rb;
    public float launchForce;
    public LayerMask enemy;
    public Vector2 previousVelocity;

    //bool
    private bool firstDeath = false;
    public int deathBounces;

    // The spot the ragdoll waits to be summoned from
    private Vector3 waitingSpot;
    private bool reset = true;

    // Respawn point + bool to check if player is alive
    public Transform[] respawnPoints;
    private Transform closestPoint;
    private Transform lastPos;
    private bool alive = true;

    // Player's total lives
    private int lives;


    // Start is called before the first frame update
    void Awake()
    {
        waitingSpot = this.transform.position;

        Instance = this;
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        player = GameObject.FindGameObjectWithTag("Player");
        // Add a camera if there is not already one attached
        if (cam == null)
            cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    private void FixedUpdate()
    {
        previousVelocity = rb.velocity;

        if (!alive)
            cam.transform.position = Vector3.Lerp(cam.transform.position + Vector3.back, new Vector3(this.transform.position.x + rb.velocity.x / 3.5f, this.transform.position.y + rb.velocity.y / 3.5f, -1), 0.15f);
    }

    private void Update()
    {
        if (!player.activeInHierarchy && alive)
        {
            lives = player.GetComponent<Player_Script>().lives;
            alive = false;
            lastPos = player.transform;
        }
        else if (!alive)
        {
            if (lives > 0)
            {
                if (Input.GetKeyDown(KeyCode.R))
                {
                    closestPoint = respawnPoints[0];
                    for (int i = 0; i < respawnPoints.Length; i++)
                    {
                        var shortestDistance = Vector2.Distance(lastPos.position, closestPoint.position);
                        var checkDistance = Vector2.Distance(lastPos.position, respawnPoints[i].position);

                        if (checkDistance < shortestDistance)
                            closestPoint = respawnPoints[i];
                        else
                            continue;
                    }
                    ResetPlayerState();
                }
            }
            else if (lives == 0)
            {
                if (Input.GetKeyDown(KeyCode.R))
                {
                    Debug.Log("dead, no more lives!");
                }
            }
        }
    }

    // This respawns the player at a nearby checkpoint
    private void ResetPlayerState()
    {
        // Sets player to active
        player.gameObject.SetActive(true);

        // Sets player script + attack script to accessible variables
        var script = player.GetComponent<Player_Script>();
        var attackScript = player.GetComponentInChildren<Player_Attacks>();

        // Sets player's position to closest checkpoint
        player.transform.position = closestPoint.position;

        // Resets ragdoll velocity and places it back into waiting spot
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0;
        this.transform.position = waitingSpot;
        // Resetting variables in ragdoll
        reset = true;
        firstDeath = false;
        alive = true;
        // Resetting variables in player script and attack script + fixing animator
        script.ResetGrapple();
        attackScript.anm.Play("Idle");
        attackScript.CanAttack();
        attackScript.ResetCooldown();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (firstDeath)
        {
            deathBounces++;

            float randRotate = Random.Range(-180f, 180f);
            transform.Rotate(0, 0, randRotate);

            //impact is your angle of impact, normal is the walls normal angle
            //launch is the reflected angle for the bounce
            Vector2 impactAngle, normalAngle, launchAngle;
            //angle of ragdoll impact
            impactAngle = previousVelocity;
            //Debug.Log("previousVelocity:\t" + previousVelocity);

            Debug.DrawRay(this.transform.position, impactAngle.normalized*10, Color.cyan, 10f);

            //normal angle of wall needed for vector calcs
            normalAngle = col.contacts[0].normal;

            //Debugging
            //Debug.DrawRay(this.transform.position, normalAngle*20, Color.red, 10f);
            //Debug.Log("col.contacts[0].normal:\t" + col.contacts[0].normal);

            //create launch angle
            launchAngle = Vector2.Reflect(impactAngle, normalAngle);
            Debug.DrawRay(this.transform.position, launchAngle, Color.green, 10f);

            //launchForce *= .8f;
            //rb.AddForce(launchAngle * launchForce);

            //launch character
            rb.velocity = launchAngle * .8f;
          
            //play hitmarker sound effect
            NoisyBoi.Instance.MakeNoise();
        }

        /* Destroy Player after 20 bounces
        if(deathBounces > 20)
        {
            Destroy(this.gameObject);
        }
        */
    }


    public void DeathLaunch(Vector2 launchPoint, float parentTitan)
    {

        Vector2 launchAngle, rayAngle;
        RaycastHit2D ray;

        //tp ragdoll to player
        this.transform.position = playerPos.position;

        rayAngle = new Vector2(launchPoint.x - this.transform.position.x, launchPoint.y - this.transform.position.y);

        if (rayAngle != Vector2.zero)
            ray = Physics2D.Raycast(this.transform.position, rayAngle, 20f, enemy);
        else
            ray = Physics2D.Raycast(this.transform.position, Vector2.up, 20f, enemy);

        Debug.DrawRay(this.transform.position, rayAngle*5, Color.red, 10f);


        launchAngle = ray.normal*10;
        Debug.DrawRay(this.transform.position, launchAngle, Color.blue, 10f);


        //if titan to the right and launch to the right, flip x to launch left
        if (parentTitan > this.transform.position.x && launchAngle.x > 0)
            launchAngle.x *= -1;

        //if titan to the left and launch to the left, flip x to right
        if (parentTitan < this.transform.position.x && launchAngle.x < 0)
            launchAngle.x *= -1;

        //launch the ragdoll
        rb.AddForce(launchAngle * launchForce);

        firstDeath = true;
        reset = false;

        //Debug.Log("RayAngle:\t" + rayAngle + "\tLaunchAngle:\t" + launchAngle + "\tLaunchPoint:\t" + launchPoint + "\tthis.position:\t" + this.transform.position);

        //play hitmarker sound effect
        NoisyBoi.Instance.MakeNoise();

    }
}
