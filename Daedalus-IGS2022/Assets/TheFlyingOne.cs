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
    private bool isInRange;
    public CircleCollider2D range;
    public Transform Player;
    public Transform TheForbidenOne;
    //public GameObject flyingEnemy;
    //private FlyingEnemyPlayerDetectionScript range;
    void Start()
    {
        //range = flyingEnemy.GetComponent<FlyingEnemyPlayerDetectionScript>();
        firstMove = true;
        TheForbidenOneRange = scriptObject.GetComponent<FlyingEnemyPlayerDetectionScript>();
        isInRange = TheForbidenOneRange.inRange;

    }

    
    void Update()
    {
        if (transform.position == targetPos1)
        {
            transform.localScale = Vector3.one;
            firstMove = false;
        }
        if (transform.position == targetPos2)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            firstMove = true;
        }
        if (canMove && !isInRange)
        {
            if (firstMove)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPos1, speed * Time.deltaTime);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPos2, speed * Time.deltaTime);
            }
        }
        else if(canMove && isInRange)
        {
            TheForbidenOne.position = Vector2.MoveTowards(TheForbidenOne.position, Player.position, speed * Time.deltaTime);
        }

    }
}

