using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basic_Titan : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;
    public float health;
    public float engageDistance;
    public int direction;
    private GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void FixedUpdate()
    {
        var playerDirection = player.transform.position.x - this.transform.position.x;
        var distanceFromPlayer = Mathf.Abs(playerDirection);

        if (distanceFromPlayer < engageDistance)
        {
            // Player is to the left
            if (playerDirection < 0)
            {
                rb.AddForce(new Vector2(speed * -1f, 0));
            }
            // Player is to the right
            else if (playerDirection > 0)
            {
                rb.AddForce(new Vector2(speed, 0));
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
