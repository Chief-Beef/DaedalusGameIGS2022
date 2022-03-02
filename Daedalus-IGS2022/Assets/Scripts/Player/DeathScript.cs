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


    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void FixedUpdate()
    {
        if(player == null)
            cam.transform.position = Vector3.Lerp(cam.transform.position + Vector3.back, new Vector3(this.transform.position.x + rb.velocity.x / 3.5f, this.transform.position.y + rb.velocity.y / 3.5f, -1), 0.15f);

        previousVelocity = rb.velocity;
    }


    private void OnCollisionEnter2D(Collision2D col)
    {
        if(firstDeath)
        {

            deathBounces++;

            float randRotate = Random.Range(-180f, 180f);
            transform.Rotate(0, 0, randRotate);

            //impact is your angle of impact, normal is the walls normal angle
            //launch is the reflected angle for the bounce
            Vector2 impactAngle, normalAngle, launchAngle;
            //angle of ragdoll impact
            impactAngle = previousVelocity;
            Debug.Log("previousVelocity:\t" + previousVelocity);

            Debug.DrawRay(this.transform.position, impactAngle.normalized*10, Color.cyan, 10f);

            //normal angle of wall needed for vector calcs
            normalAngle = col.contacts[0].normal;

            //Debugging
            Debug.DrawRay(this.transform.position, normalAngle*20, Color.red, 10f);
            Debug.Log("col.contacts[0].normal:\t" + col.contacts[0].normal);

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

        Debug.Log("RayAngle:\t" + rayAngle + "\tLaunchAngle:\t" + launchAngle + "\tLaunchPoint:\t" + launchPoint + "\tthis.position:\t" + this.transform.position);

        //play hitmarker sound effect
        NoisyBoi.Instance.MakeNoise();

    }
}
