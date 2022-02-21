using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingLint : MonoBehaviour
{

    //Movement variables
    public float speed;
    
    //Player Detection
    private RaycastHit2D playerRay;
    private Transform target;
    private GameObject player;
    private Vector2 rayDirection;
    public LayerMask ground;

    // Start is called before the first frame update
    void Start()
    {
        // find player location
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void FixedUpdate()
    {
        //go to player location

        rayDirection = new Vector2(player.transform.position.x - this.transform.position.x, player.transform.position.y - this.transform.position.y);
        rayDirection = rayDirection.normalized;

        playerRay = Physics2D.Raycast(this.transform.position,rayDirection, 100f, ground);

        Debug.Log("Ray Hit: " + playerRay.collider.tag);

        transform.position = Vector2.MoveTowards(this.transform.position, target.position, speed * Time.deltaTime);

        //no pathfinding, only mindless chase atm
    }
}
