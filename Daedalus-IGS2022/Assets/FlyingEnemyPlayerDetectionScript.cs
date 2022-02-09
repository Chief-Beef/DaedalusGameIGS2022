using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyPlayerDetectionScript : MonoBehaviour
{
    public GameObject parentObject;
    public Transform Player;
    public Transform TheForbidenOne;
    public bool inRange;
    public GameObject TheForbiddenOne;
    public Rigidbody2D body;
    public float speed;

    void moveEnemy()
    {
        TheForbidenOne.position = Vector2.MoveTowards(TheForbidenOne.position, Player.position, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D trigger)
    {
        //if the trigger collides with an object with the tag of player
        //then calculate the direction of the player by subtracting the positon of
        //the flying enemy from the position of the player
        if (trigger.gameObject.tag == "Player")
        {
            /*
            Vector3 directionOfPlayer = Player.position - TheForbidenOne.position;
            Debug.Log(directionOfPlayer);
            float angle = Mathf.Atan2(directionOfPlayer.y, directionOfPlayer.x) * Mathf.Rad2Deg;
            body.rotation = -angle;
            Debug.Log(inRange);
            */
            Debug.Log("Hello");
            TheForbidenOne.position = Vector2.MoveTowards(TheForbidenOne.position, Player.position, speed * Time.deltaTime);
            //inRange = true;
        }
    }

    void start()
    {
        //body = TheForbiddenOne.GetComponent<Rigidbody2D>();
    }

    void update()
    {
        if(inRange == true)
        {
            //Vector3 directionOfPlayer = Player.position - TheForbidenOne.position;
            //Debug.Log(directionOfPlayer);
            //float angle = Mathf.Atan2(directionOfPlayer.y, directionOfPlayer.x) * Mathf.Rad2Deg;
            //body.rotation = angle;
            //TheForbidenOne.position = Vector2.MoveTowards(TheForbidenOne.position, Player.position, speed * Time.deltaTime);
            moveEnemy();
        }
        
    }

    
}

    

    /*
        // Start is called before the first frame update
        void Start()
        {
            //flyingEnemyRB = GameObject.FindWithTag("Enemy (No Grapple)");
            //body = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {
            if (inRange == true)
            {
                Vector3 directionOfPlayer = Player.position - TheForbidenOne.position;
                Debug.Log(directionOfPlayer);
            }
            if (flyingEnemyRB != null)
            {
                //cube_script = cube.GetComponent<CubeScript>();
            }

        }
    */
//}
