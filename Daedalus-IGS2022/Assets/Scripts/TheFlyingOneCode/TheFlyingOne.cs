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
    public FlyingEnemyPlayerDetectionScript TheForbidenOneRange;
    public bool isInRange;
    public Transform Player;
    public Transform TheForbidenOne;
    public GameObject TheForbiddenOne;
    public Rigidbody2D body;
    public float rangeRadius = 50;
    public float distanceFromPlayer;
    public Transform PlayerFirstContact;
    [SerializeField] public float waitTime = 5;
    public bool moveRight;
    private bool coroutineReset = true;
    public LayerMask playerSpottingLayer;
    private bool shooting = false;
    private bool shot = false;
    public GameObject laser;
    private BoxCollider2D laserCol;
    private SpriteRenderer laserSpr;
    public GameObject gun;
    private float timer = 1f;
    public GameObject fart;
    private bool canShoot = false;


    void Awake()
    {
        moveRight = false;

        body = TheForbiddenOne.GetComponent<Rigidbody2D>();

        laserCol = laser.GetComponent<BoxCollider2D>();
        laserSpr = laser.GetComponent<SpriteRenderer>();

        if (Player == null)
            Player = GameObject.FindGameObjectWithTag("Player").transform;
    }


    void FixedUpdate()
    {
        if (Player != null)
        {
            isInRange = TheForbidenOneRange.inRange;
            distanceFromPlayer = Vector2.Distance(Player.position, transform.position);
            var dif = Player.position - gun.transform.position;
            var rayHit = Physics2D.Raycast(gun.transform.position, dif, Mathf.Infinity, playerSpottingLayer);


            //if x position of enemy is larger than the x position of the target position
            if (transform.position.x > targetPos1.x)
            {
                moveRight = false;
            }
            else if (transform.position.x < targetPos2.x)
            {
                moveRight = true;
            }

            if (canMove && !shooting && !canShoot)
            {
                if (distanceFromPlayer < rangeRadius && rayHit.collider.tag == "Player")
                    canShoot = true;
                else if ((shooting || shot) && distanceFromPlayer > rangeRadius)
                    ResetLaser();

                if (moveRight)
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
                if (rayHit.collider != null)
                {
                    if (coroutineReset && !shot && rayHit.collider.gameObject.tag == "Player" && distanceFromPlayer < rangeRadius)
                    {
                        coroutineReset = false;
                        shooting = true;
                        timer = 1.5f;
                        StartCoroutine(ChargeUp());
                        GetComponent<AudioPlay>().PlayWithPitch(0.8f);
                    }
                }

                if (distanceFromPlayer > rangeRadius)
                {
                    isInRange = false;
                    ResetLaser();
                }

                if (shooting && !shot)
                {
                    if (timer > -0.28f)
                    {
                        if (rayHit.collider != null)
                        {
                            if (rayHit.collider.tag == "Player")
                            {
                                Vector3 diff = Player.transform.position - gun.transform.position;
                                Vector3 rotatedDiff = Quaternion.Euler(0, 0, 90) * diff;
                                Quaternion targetAngle = Quaternion.LookRotation(Vector3.forward, rotatedDiff);
                                gun.transform.rotation = Quaternion.RotateTowards(gun.transform.rotation, targetAngle, 150 * Time.deltaTime);
                            }
                        }

                        timer -= Time.deltaTime;
                        laserSpr.color = new Color(1 - timer, 0, 0);
                    }

                    laser.transform.localScale = new Vector3(1, laser.transform.localScale.y + (Time.deltaTime * 0.125f), 1);
                }
                else if (shot)
                {
                    // Laser shot and cooldown
                    if (timer > 0)
                    {
                        if (timer < 0.75f)
                            laserCol.enabled = false;
                        else
                            laserCol.enabled = true;
                        laser.transform.localScale = new Vector3(1, timer, 1);
                        laserSpr.color = new Color(timer, timer / 4, timer / 4);
                        timer -= Time.deltaTime;
                    }
                    // Ends laser shooting
                    else
                    {
                        shot = false;
                        shooting = false;
                        laser.transform.localScale = Vector3.zero;
                    }
                }
                else
                {
                    Vector3 diff = Player.transform.position - gun.transform.position;
                    Vector3 rotatedDiff = Quaternion.Euler(0, 0, 90) * diff;
                    Quaternion targetAngle = Quaternion.LookRotation(Vector3.forward, rotatedDiff);

                    gun.transform.rotation = Quaternion.RotateTowards(gun.transform.rotation, targetAngle, 150 * Time.deltaTime);
                }
            }  
        }
    }

    // Resets laser variables when player exits range
    private void ResetLaser()
    {
        StopAllCoroutines();
        shot = false;
        shooting = false;
        canShoot = false;
        laser.transform.localScale = Vector3.zero;
        laserCol.enabled = false;
        coroutineReset = true;

        GetComponent<AudioPlay>().asource.Stop();
    }

    IEnumerator ChargeUp()
    {
        if (Player != null)
        {
            yield return new WaitForSeconds(2);
            shot = true;
            timer = 1f;
            StartCoroutine(ShootDelay());
        }
    }

    IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(waitTime);

        if (Player != null)
        {
            coroutineReset = true;
        }
        else
            laser.transform.localScale = Vector3.zero;
    }

    public void InstantiateDeathEffect()
    {
        var f = Instantiate(fart, this.transform.position, Quaternion.identity, null);
        f.GetComponent<AudioSource>().pitch = 0.75f;
    }
}


