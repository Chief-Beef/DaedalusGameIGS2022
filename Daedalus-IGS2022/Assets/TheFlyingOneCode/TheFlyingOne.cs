using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TheFlyingOne : MonoBehaviour
{
    [SerializeField] public Vector3 targetPos1;
    [SerializeField] public Vector3 targetPos2;
    [SerializeField] public float speed = 1f;
    [SerializeField] public float attackSpeed = 1f;
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
    [SerializeField] public float waitTime = 5;
    public bool moveRight;
    public bool coroutineReset;
    private Vector2 diff;
    private Vector2 diffNorm;


    void Start()
    {
        firstMove = true;
        moveRight = false;
        coroutineReset = true;
        body = TheForbiddenOne.GetComponent<Rigidbody2D>();
        TheForbidenOneRange = scriptObject.GetComponent<FlyingEnemyPlayerDetectionScript>();
    }


    void FixedUpdate()
    {
        if (Player != null)
        {
            isInRange = TheForbidenOneRange.inRange;
            diff = Player.position - TheForbidenOne.position;
            diff = (diff.normalized) * 2;
        }
        

        //if x position of enemy is larger than the x position of the target position
        if (transform.position.x > targetPos1.x)
        {
            transform.localScale = Vector3.one; //face enemy right 
            firstMove = false;
            moveRight = false;
        }
        else if (transform.position.x < targetPos2.x)
        {
            transform.localScale = new Vector3(-1, 1, 1); //face enemy left
            firstMove = true;
            moveRight = true;
        }

        if (canMove && !isInRange)
        {

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

        else if (canMove && isInRange)
        {
            if (coroutineReset)
            {
                coroutineReset = false;
                StartCoroutine(EnemyAttack());
                //body.velocity = Vector2.zero;
                
            }
            //diff = Player.position - TheForbidenOne.position;
            //diff = (diff.normalized) * 2;
            //body.AddForce((diff) * attackSpeed);
            Debug.Log("I am in range so I should attack");
            //body.AddForce(Vector2.zero);
            //StartCoroutine(EnemyAttack());
            //TheForbidenOne.position = Vector2.MoveTowards(TheForbidenOne.position, Player.position, speed * Time.deltaTime);

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
    }
    IEnumerator EnemyAttack()
    {
        yield return new WaitForSeconds(waitTime);
        
        diff = Player.position - TheForbidenOne.position;
        diff = (diff.normalized);
        body.AddForce((diff) * attackSpeed, ForceMode2D.Impulse);
        Debug.Log("Coroutine Called");
        coroutineReset = true;
    }
    //private void OnCollisionEnter(Collision collision)
    //{
    //if(collison.gameObject )
    //}
    //IEnumerator EnemyAttack()
    //{
    //yield return new WaitForSeconds(waitTime);
    //body.AddForce((transform.position - Player.position) * speed);
    //coroutineReset = true;
    //}

}


