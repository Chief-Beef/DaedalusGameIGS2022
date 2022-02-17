using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*public class Boundary
{
    public float xMin, xMax;
}

public class TheFlyingOne : MonoBehaviour
{
    public Transform transformx;
    private Vector3 xAxis;
    private float secondsForOneLength = 1f;
    public Boundary boundary;  

    void Start()
    {
        body = GetComponent<Rigidbody2D>();

    }

    void Update()
    {
        //Let's ping pong the guy along the X-axis, i.e. change the value of X, and keep the values of Y & Z constant.            
       transformx.position = new Vector3(Mathf.PingPong(boundary.xMin, boundary.xMax), transform.position.y, transform.position.z);
    }
}
*/
/*public class TheFlyingOne : MonoBehaviour
{
    public Rigidbody2d body;
    public Animator anim;

    
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2d>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        body.addForce(new Vector2(10,0));

    }
}*/
/*public class TheFlyingOne : MonoBehaviour
{
    public Rigidbody2D body;

    void start()
    {
       body = GetComponent<Rigidbody2D>();
    }

    void update()
    {
        body.transform.Translate(100, 0, Time.deltaTime);
    }
}*/

public class TheFlyingOne : MonoBehaviour
{
    [SerializeField]
    public Vector3 targetPos1;
    [SerializeField]
    public Vector3 targetPos2;
    [SerializeField]
    public float speed = 1f;
    public bool canMove;
    public bool firstMove;
    public FlyingEnemyPlayerDetectionScript TheForbidenOneRange;
    public GameObject scriptObject;
    public bool isInRange;
    public CircleCollider2D range;
    public Transform Player;
    public Transform TheForbidenOne;
    public GameObject TheForbiddenOne;
    public Rigidbody2D body;
    public float rangeRadius = 50;
    public float distanceFromPlayer;
    public Transform PlayerFirstContact;
    public int waitTime = 5;
    public bool moveRight;

    //public GameObject flyingEnemy;
    //private FlyingEnemyPlayerDetectionScript range;
    void Start()
    {
        //range = flyingEnemy.GetComponent<FlyingEnemyPlayerDetectionScript>();
        firstMove = true;
        moveRight = false;
        body = TheForbiddenOne.GetComponent<Rigidbody2D>();
        TheForbidenOneRange = scriptObject.GetComponent<FlyingEnemyPlayerDetectionScript>();

        //isInRange = false;

    }


    void FixedUpdate()
    {
        isInRange = TheForbidenOneRange.inRange;

        if (transform.position.x > targetPos1.x)
        {
            transform.localScale = Vector3.one;
            firstMove = false;
            moveRight = false;
        }
        else if (transform.position.x < targetPos2.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            firstMove = true;
            moveRight = true;
        }

        //isInRange = TheForbidenOneRange.inRange;
        if (canMove && !isInRange)
        {
            //TheForbidenOne.position = Vector2.MoveTowards(TheForbidenOne.position, Player.position, speed * Time.deltaTime);

            if (firstMove && moveRight)
            {
                //transform.position = Vector3.MoveTowards(transform.position, targetPos1, speed * Time.deltaTime);
                body.AddForce(Vector2.right * speed);
            }
            else if (!moveRight)
            {
                //transform.position = Vector3.MoveTowards(transform.position, targetPos2, speed * Time.deltaTime);
                body.AddForce(Vector2.left * speed);
            }

            if (transform.position.y > targetPos1.y)
            {
                body.AddForce(Vector2.down * speed);
            }
            else if (transform.position.y < targetPos2.y)
            {
                body.AddForce(Vector2.up * speed);
            }
        }

        //IEnumerator EnemyAttack()
        //{
        //yield return new WaitForSeconds(waitTime);
        //PlayerFirstContact = Player.transform;
        //}
        /*else if (canMove && isInRange)
        {
            StartCoroutine(EnemyAttack());
            TheForbidenOne.position = Vector2.MoveTowards(TheForbidenOne.position, PlayerFirstContact.position, speed * Time.deltaTime);

            distanceFromPlayer = Vector2.Distance(Player.position, transform.position);
            if (distanceFromPlayer > rangeRadius)
            {
                TheForbidenOneRange.inRange = false;
                isInRange = false;
                Debug.Log("Goodbye");
            }

            //Vector3 directionOfPlayer = Player.position - TheForbidenOne.position;
            //Debug.Log(directionOfPlayer);
            //float angle = Mathf.Atan2(directionOfPlayer.y, directionOfPlayer.x) * Mathf.Rad2Deg;
            //body.rotation = -angle;
        }
    }*/

        //IEnumerator EnemyAttack()
        //{
        //yield return new WaitForSeconds(waitTime);
        //PlayerFirstContact = Player.transform;
        //}
    }
}

