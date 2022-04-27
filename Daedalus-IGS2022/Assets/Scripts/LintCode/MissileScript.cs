using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileScript : MonoBehaviour
{


    //Player Detection
    public Transform target;        //player location
    public Rigidbody2D player;      //player rigidbody
    public Vector2 lastLoc;         //last location of the player
    public float explosionForce;    //Force of the explosion

    private RaycastHit2D playerRay; //ray that aims at player
    private Vector2 playerAngle;    //angle of player relative to missile
    private Vector2 rayDirection;   //direction of the ray
    public LayerMask ground;        //ground layer

    //Movement
    public float speed, maxSpeed;   //start and max speed of missile
    public Rigidbody2D rb;          //missile RB

    //timers and shit
    private float timer;            //
    public float attackTime;        //timers
    public float deathTime;         //
    public float angleTime;

    //rotation shit
    float angle;
    Vector2 targetPos;
    Vector2 launchPos;
    Vector2 diff;

    private bool soundPlayed = false;

    //Farticle Effect
    public GameObject farticleEffect;

    // Start is called before the first frame update
    void Start()
    {
       
        //Debug.Log("Missile Fired");
        timer = 0.0f;

        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            // find player location
            target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        }
        
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (player != null)
        {
            // Start the missile off by slowly moving upward before rotating toward player
            if (timer < angleTime)
            {
                transform.position = Vector2.MoveTowards(this.transform.position, this.transform.position + Vector3.up, speed * Time.deltaTime);
                this.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            }
            //rotate the missile to go to look at the player
            else if (timer < attackTime && timer >= angleTime)
            {
                lastLoc = target.position;   //last known location before attackTime is met
                launchPos = this.transform.position;

                diff = lastLoc - launchPos;

                targetPos = new Vector2(target.position.x - this.transform.position.x, target.position.y - this.transform.position.y);
                angle = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg;
                this.transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(new Vector3(0, 0, angle - 90)), 0.1f);
            }


            timer += Time.deltaTime;

            if (timer >= deathTime)//destroy missile if still alive after time x2
            {
                Explode();
                Destroy(this.gameObject);
            }
            else if (timer >= attackTime)    //after time x1 fly at guy at max speed
            {
                if (!soundPlayed)
                {
                    GetComponent<AudioPlay>().PlayWithPitch(0.9f);
                    GetComponent<CapsuleCollider2D>().enabled = true;
                    soundPlayed = true;
                }

                targetPos = new Vector2(lastLoc.x - this.transform.position.x, lastLoc.y - this.transform.position.y);
                angle = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
                this.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));

                speed = maxSpeed;   //inc speed to max speed
                transform.position = Vector2.MoveTowards(this.transform.position, this.transform.position + new Vector3(diff.x, diff.y, 0), speed * Time.deltaTime);

                //Missile Hit Player Last Location
                if (Vector2.Distance(this.transform.position, lastLoc) <= .05f)
                {
                    // I commented this out because it would often fall short and miss the player if they simply move backwards
                    //Destroy(this.gameObject);
                }
            }
            else if (timer >= angleTime)   //start flying and slowly track player
            {
                //start missile pointing at player
                transform.position = Vector2.MoveTowards(this.transform.position, target.position, speed * Time.deltaTime);
            }
        }
        else if (player == null)
        {
            Explode();
            Destroy(this.gameObject);
        }
    }

    public void OnCollisionEnter2D(Collision2D col)
    {
        Explode();
        Destroy(this.gameObject);
    }

    private void Explode()
    {
        //when the missile is destroyed create the explosion prefab
        Instantiate(farticleEffect, this.transform.position, Quaternion.identity, null);
    }

}
