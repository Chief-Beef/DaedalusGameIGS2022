using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Bullet : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;
    // Int to store damage type (1 = Fire, 2 = Poison, 3 = Ice, 4 = Basic)
    public int damageType;
    public float damage;

    void Update()
    {
        rb.velocity = transform.forward * speed;
    }
}
