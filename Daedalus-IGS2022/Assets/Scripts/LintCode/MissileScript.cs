using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileScript : MonoBehaviour
{


    //Player Detection
    private Transform target;
    public Vector2 lastLoc;

    private RaycastHit2D playerRay;
    private Vector2 rayDirection;
    public LayerMask ground;

    //Movement
    public float speed, startSpeed, maxSpeed;
    public Rigidbody2D rb;

    //timers and shit
    private float timer;
    public float attackTime;
    public float deathTime;

    //rotation shit
    float angle;
    Vector2 targetPos;

    //Farticle Effect
    public GameObject farticleEffect;

    // Start is called before the first frame update
    void Start()
    {
       
        Debug.Log("Missile Fired");
        timer = 0.0f;
 
        // find player location
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        
        maxSpeed = speed * 5;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //rotate the missile to go to look at the player
        if (timer < attackTime)
        {
            lastLoc = target.position;   //last known location before attackTime is met
            targetPos = new Vector2(target.position.x - this.transform.position.x, target.position.y - this.transform.position.y);
            angle = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg;
            this.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
        }


        timer += Time.deltaTime;

        if (timer >= deathTime)//destroy missile if still alive after time x2
        {
            Destroy(this.gameObject);
        }
        else if (timer >= attackTime)    //after time x fly at guy at max speed
        {
            
            targetPos = new Vector2(lastLoc.x - this.transform.position.x, lastLoc.y - this.transform.position.y);
            angle = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg;
            this.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));

            speed = maxSpeed;
            transform.position = Vector2.MoveTowards(this.transform.position,lastLoc, speed * Time.deltaTime);

            if (Vector2.Distance(this.transform.position, lastLoc) <= 1.0f)
                Destroy(this.gameObject);

        }
        else    //start flying and slowly track player
        {
            //start missile pointing at player
            transform.position = Vector2.MoveTowards(this.transform.position, target.position, speed * Time.deltaTime);
        }

        
    }

    public void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
        }
    }

    private void OnDestroy()
    {
        Instantiate(farticleEffect, this.transform.position, Quaternion.identity, null);
        //when the missile is destroyed create the explosion prefab
    }

}
