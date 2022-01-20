using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIScript: MonoBehaviour
{

    //current location
    public Vector2 current;

    public Rigidbody2D rb;

    //attack range
    public float attackRange;
    //spotting range
    public float spotRange;
    //enemy current health
    public float health;
    //enemy starting health
    public float startHealth;
    //movement speed
    public float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        health = startHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
