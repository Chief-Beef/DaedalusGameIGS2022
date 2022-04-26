﻿using System.Collections;
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
    private bool coroutineReset = false;
    private Vector2 diff;
    private Vector2 diffNorm;

    private bool shooting = false;
    private bool shot = false;
    public GameObject laser;
    private BoxCollider2D laserCol;
    private SpriteRenderer laserSpr;
    public GameObject gun;
    private float timer = 1f;


    void Awake()
    {
        firstMove = true;
        moveRight = false;
        coroutineReset = true;
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
            diff = Player.position - TheForbidenOne.position;
            diff = (diff.normalized) * 2;
        }


        //if x position of enemy is larger than the x position of the target position
        if (transform.position.x > targetPos1.x)
        {
            transform.eulerAngles = Vector3.up * 180;
            firstMove = false;
            moveRight = false;
        }
        else if (transform.position.x < targetPos2.x)
        {
            transform.eulerAngles = Vector3.zero;
            firstMove = true;
            moveRight = true;
        }

        if (canMove && !isInRange && !shooting)
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
            if (coroutineReset && !shot && Player.gameObject.activeInHierarchy == true)
            {
                coroutineReset = false;
                shooting = true;
                timer = 1.5f;
                StartCoroutine(ChargeUp());
            }

            if (Player != null)
            {
                distanceFromPlayer = Vector2.Distance(Player.position, transform.position);
                if (distanceFromPlayer > rangeRadius)
                {
                    isInRange = false;
                }

                if (shooting && !shot)
                {
                    if (timer > 0)
                    {
                        Vector3 diff = Player.transform.position - gun.transform.position;
                        Vector3 rotatedDiff = Quaternion.Euler(0, 0, 90) * diff;
                        Quaternion targetAngle = Quaternion.LookRotation(Vector3.forward, rotatedDiff);
                        gun.transform.rotation = Quaternion.RotateTowards(gun.transform.rotation, targetAngle, 25 * Time.deltaTime);
                        timer -= Time.deltaTime;
                        laserSpr.color = new Color(1 - timer, 0, 0);
                    }

                    laser.transform.localScale = new Vector3(1, laser.transform.localScale.y + (Time.deltaTime * 0.125f), 1);
                }
                else if (shot)
                {
                    if (timer > 0)
                    {
                        timer -= Time.deltaTime;
                        laserCol.enabled = true;
                        laser.transform.localScale = new Vector3(1, timer, 1);
                        laserSpr.color = new Color(timer, timer / 4, timer / 4);
                    }
                    else
                    {
                        laserCol.enabled = false;
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

                    gun.transform.rotation = Quaternion.RotateTowards(gun.transform.rotation, targetAngle, 50 * Time.deltaTime);
                }
            }
           
        }
    }
    IEnumerator ChargeUp()
    {
        yield return new WaitForSeconds(2);
        shot = true;
        timer = 1f;
        StartCoroutine(ShootDelay());
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
}


