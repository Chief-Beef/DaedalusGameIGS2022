﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Script : MonoBehaviour
{
    /********************************/
    /*         Player script        */
    /********************************/

    // The rigidbody for handling player movement
    public Rigidbody2D rb;
    // The camera in the scene
    public Camera cam;
    private float lastVelocity;
    public float camDefaultSize;

    // The animator component on the player
    public Animator anm;
    // Player speed & force applied by jump
    public float speed;
    public float airSpeed;
    public float maneuverSpeed;
    public float jumpForce;

    public bool grounded = true; // Boolean true if player is on the ground
    private bool jumped = false; // Boolean used to allow player to have a boost when initializing a jump
    private bool canGrapple = true; // Bool for preventing grapple spam
    public Vector3 mousePos = Vector3.zero;

    public float grappleForce; // Force generated by grappleshot
    public LineRenderer rope; // The linerenderer for the grappleshot's rope
    public float grappleRange; // The max range of the grappleshot
    private RaycastHit2D grappleRay; // The raycast for checking if something is in range of the grappleshot
    public bool isGrappling = false; // Boolean for checking whether the player is in the process of grappling
    private bool canFire = true; // Prevents player from grapple spamming (can reset after player lets go of grapple button to
                                 // prevent chaining grappleshots with a single input)
    private bool canReload = false; // Another bool for preventing grapple spam (resets to true after a short coroutine ends)
    public GameObject staminaBar; // Grapple stamina bar

    public GameObject grappleSpot; // The ojbect that gets instantiated when a grappleshot lands
    private GameObject grappleSpotPos; // The variable that keeps track of the grappleSpot to allow grappling to a moving object


    public float grappleStamina; // Total stamina
    public float currentStamina; // Current stamina

    public LayerMask ground;    // Variable for ground checks
    public LayerMask grapple;   // Variable for grapple raycasts

    // The different axes of Unity's built-in input system
    private float xMove;
    private float yMove;
    private float jump;

    // Stores velocity on the x axis as a float
    private float xVel = 0.0f;
    private float xVelAbs;
    
    // Transforms used in ground checks
    public Transform boxcastA;
    public Transform boxcastB;

    //Death Stuff
    private Vector2 launchPoint;
    private Vector2 launchAngle;
    private Transform parentTitan;
    private Vector2 previousVelocity;
    public float bounciness;
    private bool alive = true;
    public int lives;
    // Resets player's alive status
    private void SetAlive()
    {alive = true;}

    // Crosshair stuff
    public GameObject crosshair;
    private SpriteRenderer crosshairSpr;
    public GameObject targetMarker;
    // Grapple is in range color
    public Color activeColor;
    // Grapple is out of range color
    public Color inactiveColor;

    // Places player can respawn at
    public Transform[] respawnPoints;
    private Transform closestPoint;

    // Called once when a scene is loaded
    void Awake()
    {
        if (cam == null)
            cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();


        currentStamina = grappleStamina;

        // Initializes crosshair
        crosshairSpr = crosshair.GetComponent<SpriteRenderer>();
        crosshairSpr.color = inactiveColor;

        // Disables cursor so it doesn't get in the way of crosshair (will probably need to be reworked into other crosshair script)
        Cursor.visible = false;
    }

    // Update called once per physics update
    void FixedUpdate()
    {
        // Camera controls
        var vel = rb.velocity.magnitude;
            cam.orthographicSize = camDefaultSize + (Mathf.Lerp(vel, lastVelocity, 0.05f) / 7.5f);

        // Sets camera position relative to player
        cam.transform.position = Vector3.Lerp(cam.transform.position + Vector3.back, new Vector3(this.transform.position.x + rb.velocity.x / 3.5f, this.transform.position.y + rb.velocity.y / 3.5f, -1), 0.15f);

        lastVelocity = vel;

        // Storing the input axes as floats for rerence in the script as well as the x axis velocity
        xMove = Input.GetAxis("Horizontal");
        yMove = Input.GetAxis("Vertical");
        jump = Input.GetAxis("Jump");
        xVel = rb.velocity.x;
        xVelAbs = Mathf.Abs(xVel);

        // Animator controls
        if (grounded)
        {
            if (xVelAbs > 0.1f) // Walking animation plays when moving and grounded
            {
                anm.Play("Run");
                anm.speed = xVelAbs / 7.5f;
            }
            else // They will stand still if not moving
                anm.Play("Stand");
        }
        else if (!grounded)
            anm.Play("Falling");

        // Flips the player if they move in a direction
        if (mousePos.x > this.transform.position.x)
            transform.eulerAngles = Vector3.zero;
        else if (mousePos.x < this.transform.position.x)
            transform.eulerAngles = Vector3.up * 180;

        // Player's horizontal movement when grounded & ungrounded
        if (grounded) // Grounded movement
        {
            rb.AddForce(new Vector2(xMove * speed, 0));
        }
        else if (canGrapple) // Airborn movement (no grapple)
        {
            rb.AddForce(new Vector2(xMove * airSpeed, 0));
        }
        else if (!canGrapple) // Airborn movement while grappling
        {
            rb.AddForce(new Vector2(xMove, yMove) * maneuverSpeed);
        }

        // Fart mechanic
        if (jump > 0)
        {
            if (grounded)
            {
                if (!jumped) // Initial boost to make jump feel more natural
                {
                    rb.AddForce(Vector2.up * jumpForce * 2, ForceMode2D.Impulse);
                    jumped = true;
                    rb.drag = 0.1f;
                }
                // Jump will add continuous force if button is held for a short duration for variable jump height
                rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                StartCoroutine(Unground());
            }
            // Jumping while grappling (grapple jump)
            else if (!canGrapple && GroundCheck())
            {
                rb.AddForce(Vector2.up * jumpForce * 5, ForceMode2D.Impulse);
                canGrapple = false;
            }
        }

        /*

        {
            Instantiate(bullet, this.transform.position, Quaternion.Euler(0, 0, (Mathf.Atan2(mousePos.y - this.transform.position.y, mousePos.x - this.transform.position.x))), null);

            canFire = false;
            StartCoroutine(Chamber());
        }

        */
            // Grapple shot mechanic
        if (Input.GetAxis("Fire2") > 0 && canFire && currentStamina > 0)
        {
            if (canGrapple)
            {
                // Triggered when grappleRay has hit an object
                if (grappleRay.collider != null)
                {
                    canReload = false;
                    isGrappling = true;

                    if (grounded)
                    {
                        grounded = false;
                        rb.drag = 0.1f;
                    }
                    canGrapple = false;

                    // Instantiates a gameObject with the transform of the object that the raycast hits
                    grappleSpotPos = Instantiate(grappleSpot, grappleRay.point, Quaternion.identity, grappleRay.collider.gameObject.transform);

                    // hookSpot = grappleRay.point; // Grappleshot will hook onto the point where the raycast hit
                     
                    //Frick u cam im gonna write the word fart in this program
                }
            }
            else if (grappleSpotPos != null)
            {
                // Add grapple force
                rb.AddForce(new Vector2(grappleSpotPos.transform.position.x - this.transform.position.x, grappleSpotPos.transform.position.y - this.transform.position.y) * grappleForce);
            }

        }
        // This statement is executed when the grapple is finished
        else if (!canGrapple)
        {
            ResetGrapple();

            if (GroundCheck()) // Checks if player is on ground when done grappling
                Ground();
        }
        else if (Input.GetAxis("Fire2") == 0 && canReload)
            canFire = true;

        //for collision physics in OnCollisionEnter2D
        previousVelocity = rb.velocity;
    }

    private void Update()
    {
        // Mouse position
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        // Sets crosshair position
        crosshair.transform.position = new Vector2(mousePos.x, mousePos.y);

        // Grapple ray is the RaycastHit2D that tells the grappleshot where to go
        grappleRay = Physics2D.Raycast(this.transform.position, mousePos - this.transform.position, grappleRange, grapple);

        // If object is in range of crosshair:
        if (grappleRay.collider != null)
        {
            targetMarker.SetActive(true);
            targetMarker.transform.position = grappleRay.point;
            crosshair.transform.localScale = Vector2.one * 0.75f;
            crosshairSpr.color = activeColor;
            
        }
        // If object is not in range of crosshair:
        else
        {
            targetMarker.SetActive(false);
            crosshair.transform.localScale = Vector2.one;
            crosshairSpr.color = inactiveColor;
        }


        if (isGrappling && grappleSpotPos != null)
        {
            // Rope visuals
            rope.positionCount = 2;
            rope.SetPosition(0, this.transform.position);
            rope.SetPosition(1, grappleSpotPos.transform.position);

            Mathf.Clamp(currentStamina -= Time.deltaTime, 0, grappleStamina); // Deplete stamina at 1 unit / second
            staminaBar.transform.localScale = new Vector3(currentStamina / grappleStamina, 1, 1); // Update scale of stamina bar
        }
        else if (currentStamina < grappleStamina)
        {
            // Resets rope
            rope.SetPosition(0, Vector3.zero);
            rope.SetPosition(1, Vector3.zero);

            Mathf.Clamp(currentStamina += Time.deltaTime * 1.5f, 0, grappleStamina); // Recharge stamina at 1.5 units / second
            staminaBar.transform.localScale = new Vector3(currentStamina / grappleStamina, 1, 1); // Update scale of stamina bar
        }
    }

    // Coroutine allows for variable jump height by delaying the falsification of the grounded variable
    IEnumerator Unground()
    {
        yield return new WaitForSeconds(0.15f);
        grounded = false;
    }

    // Coroutine stops grapple spamming
    IEnumerator CanReloadGrapple()
    {
        yield return new WaitForSeconds(0.075f);
        canReload = true;
    }

    // Triggered upon colliding with object
    public void OnCollisionEnter2D(Collision2D col)
    {
        // This statement checks whether or not there is ground underneath the player whenever they hit an object
        if (GroundCheck() && canGrapple)
            Ground();

        // if not grounded bounce off walls/platforms
        if(!GroundCheck())
        {
            Vector2 impactAngle, normalAngle, launchAngle;

            impactAngle = previousVelocity;
            
            normalAngle = col.contacts[0].normal;
            launchAngle = Vector2.Reflect(impactAngle, normalAngle);

            rb.AddForce(launchAngle * bounciness, ForceMode2D.Impulse);

            //play hitmarker sound effect
            NoisyBoi.Instance.MakeNoise();

            //Debug.Log("previousVelocity:\t" + previousVelocity);
            //Debug.DrawRay(this.transform.position, impactAngle.normalized * 10, Color.cyan, 10f);
            //Debug.DrawRay(this.transform.position, normalAngle * 20, Color.red, 10f);
            //Debug.Log("col.contacts[0].normal:\t" + col.contacts[0].normal);
            //Debug.DrawRay(this.transform.position, launchAngle, Color.green, 10f);
        }


    }

    private void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.gameObject.tag == "TitanAttack")
        {

            launchPoint = trigger.ClosestPoint(this.transform.position);
            
            //Debug.Log("LaunchPoint:\t" + launchPoint);

            //RagDoll Death Script Function Call then destroy the player bc they are dead
            DeathScript.Instance.DeathLaunch(launchPoint, trigger.gameObject.transform.position.x);
            alive = false;
            this.gameObject.SetActive(false);
        }
    }

    // Triggered whenever ending collision with object
    private void OnCollisionExit2D(Collision2D col)
    {
        // This statement checks whether or not there is ground underneath the player whenever they leave contact with an object
        if (GroundCheck() && canGrapple)
            Ground();
        else
        {
            grounded = false;
            jumped = true;
            rb.drag = 0.1f;
        }
    }

    // Checks if the player is on solid ground or not
    private bool GroundCheck()
    {
        return Physics2D.OverlapArea(boxcastA.position, boxcastB.position, ground);
    }

    // Grounds the player and corrects drag
    private void Ground()
    {
        grounded = true;
        jumped = false;
        rb.drag = 2.5f; // Ground drag
    }

    private float ShootAngle()
    {
        float num = 0f;
        return num;
    }

    public void ResetGrapple()
    {
        if (grappleSpotPos != null)
            Destroy(grappleSpotPos.gameObject); // Destroys instantiated grappleSpot
        canGrapple = true; // Resets grappleshot
        isGrappling = false; // Allows game to grapple again
        canFire = false; // Prevents player from firing another grapple shot until they release the keybind
        StartCoroutine(CanReloadGrapple()); // Coroutine to prevent grapple spamming
    }
}
