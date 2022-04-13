using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileScript : MonoBehaviour
{


    //Player Detection
    private RaycastHit2D playerRay;
    private Transform target;
    private GameObject player;
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
    


    // Start is called before the first frame update
    void Start()
    {
       
        Debug.Log("Missile Fired");
        timer = 0.0f;
 
        // find player location
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        player = GameObject.FindGameObjectWithTag("Player");

        maxSpeed = speed * 5;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //rotate the missile to go to look at the player
        targetPos = new Vector2(target.position.x - this.transform.position.x, target.position.y - this.transform.position.y);
        angle = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg;
        this.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));

        timer += Time.deltaTime;

        if (timer >= deathTime)//destroy missile if still alive
        {
            Destroy(this.gameObject);
        }
        else if (timer > attackTime)    //fly at guy
        {
            rb.velocity = Vector2.zero;
            speed = maxSpeed;

            rayDirection = new Vector2(player.transform.position.x - this.transform.position.x, player.transform.position.y - this.transform.position.y);
            Debug.DrawRay(this.transform.position, rayDirection* 20, Color.cyan, 10.0f);

            transform.position = Vector2.MoveTowards(this.transform.position, target.position, speed * Time.deltaTime);

        }
        else
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

}
