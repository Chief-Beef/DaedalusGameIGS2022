using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Script : MonoBehaviour
{
    /********************************
     * Cameron's test player script *
     ********************************/

    // The rigidbody for handling player movement
    public Rigidbody2D rb;
    // The camera in the scene + mouse position
    public GameObject cam;
    private Vector3 mousePos;
    // The animator component on the player
    public Animator anm;
    // Player speed & force applied by jump
    public float speed;
    public float jumpForce;

    // Boolean true if player is on the ground
    private bool grounded = true;
    // Boolean used to allow player to have a boost when initializing a jump
    private bool jumped = false;
    // Bool for preventing grapple spam
    private bool canGrapple = true;
    // Transform for grappleshot + force to apply + lineRenderer
    private Vector3 hookSpot;
    public float grappleForce;
    public LineRenderer rope;
    public float grappleRange;

    // Unused variable (for detecting the ground)
    public LayerMask ground;

    // The different axes of Unity's built-in input system
    private float xMove;
    private float yMove;
    private float jump;
    // Stores velocity on the x axis as a float
    private float xVel = 0.0f;
    private float xVelAbs;



    // Called once when a scene is loaded
    void Start()
    {
        if (cam == null)
            cam = GameObject.FindGameObjectWithTag("MainCamera");
    }



    // Update called once per physics update
    void FixedUpdate()
    {
        // Storing the input axes as floats for rerence in the script as well as the x axis velocity
        xMove = Input.GetAxis("Horizontal");
        yMove = Input.GetAxis("Vertical");
        jump = Input.GetAxis("Jump");
        xVel = rb.velocity.x;
        xVelAbs = Mathf.Abs(xVel);

        // Sets camera position relative to player
        cam.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -10);

        // Variable stores mouse position (in world position)
        mousePos = cam.GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);

        // Animator controls
        if (grounded)
        {
            if (xVelAbs > 0.25f)
            {
                anm.Play("Run");
                anm.speed = xVelAbs / 3f;
            }
            else
                anm.Play("Stand");
        }
        else if (!grounded)
            anm.Play("Falling");

        // Flips the player if they move in a direction
        if (xVel > 0.1f)
            transform.eulerAngles = Vector3.zero;
        else if (xVel < -0.1f)
            transform.eulerAngles = Vector3.up * 180;

        // Player's horizontal movement when grounded & ungrounded
        if (grounded)
        {
            if (Mathf.Abs(xMove) > 0)
                rb.velocity = new Vector2(xMove * speed, rb.velocity.y);
            else
                rb.velocity = new Vector2(xVel * 0.25f, rb.velocity.y);
        }
        else if (!canGrapple && rb.velocity.x < 20)
        {
            rb.AddForce(new Vector2(xMove, 0));
        }

        // Jump mechanic
        if (jump > 0)
        {
            if (grounded)
            {
                if (!jumped)
                {
                    rb.AddForce(Vector2.up * jumpForce * 2, ForceMode2D.Impulse);
                    jumped = true;
                }

                rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                StartCoroutine(Unground());
            }
            else if (!canGrapple && GroundCheck())
            {
                rb.AddForce(Vector2.up * jumpForce * 5, ForceMode2D.Impulse);
            }
        }

        // Grapple shot mechanic
        if (Input.GetAxis("Fire2") > 0)
        {
            if (canGrapple)
            {
                RaycastHit2D ray = Physics2D.Raycast(this.transform.position, mousePos - this.transform.position, grappleRange, ground);
                if (ray.collider != null)
                {
                    if (grounded)
                        grounded = false;
                    canGrapple = false;
                    hookSpot = ray.point;
                }
            }
            else
            {
                rb.AddForce(new Vector2(hookSpot.x - this.transform.position.x, hookSpot.y - this.transform.position.y) * grappleForce);
                rope.positionCount = 2;
                rope.SetPosition(0, this.transform.position);
                rope.SetPosition(1, hookSpot);
            }

        }
        else if (!canGrapple)
        {
            if (GroundCheck())
                Ground();
            rope.SetPosition(0, Vector3.zero);
            rope.SetPosition(1, Vector3.zero);
            canGrapple = true;
        }
    }

    // Coroutine allows for variable jump height by delaying the falsification of the grounded variable
    IEnumerator Unground()
    {
        yield return new WaitForSeconds(0.15f);
        grounded = false;
    }



    // Triggered upon colliding with object
    private void OnCollisionEnter2D(Collision2D col)
    {
        // This statement checks whether or not there is ground underneath the player whenever they hit an object
        if (GroundCheck() && canGrapple)
        {
            Ground();
        }
    }



    // Triggered whenever ending collision with object
    private void OnCollisionExit2D(Collision2D col)
    {
        // This statement checks whether or not there is ground underneath the player whenever they leave contact with an object
        if (GroundCheck() && canGrapple)
        {
            Ground();
        }
        else
        {
            grounded = false;
            jumped = true;
        }
    }

    // Checks if the player is on solid ground or not
    private bool GroundCheck()
    {
        return (Physics2D.BoxCast(new Vector2(this.transform.position.x, this.transform.position.y) - new Vector2(0, 1.06f), new Vector2(0.75f, 0.1f), 0, Vector2.zero));
    }

    private void Ground()
    {
        grounded = true;
        jumped = false;
    }
}
