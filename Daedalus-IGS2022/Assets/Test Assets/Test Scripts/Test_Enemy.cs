using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Enemy : MonoBehaviour
{
    /*******************************
     * Cameron's test enemy script *
     *******************************/

    // Bool to determine if the player is in range or not
    private bool spotted = false;
    // Stores player gameobject
    private GameObject player;

    // Enemy types (1 = Fire, 2 = Poison, 3 = Ice, 4 = Basic (no resistances or weaknesses))
    public int enemyType = 4;

    public Rigidbody2D rb;
    public float speed;
    public float health;

    private void FixedUpdate()
    {
        if (spotted && player != null)
        {
            // Go right
            if (player.transform.position.x > this.transform.position.x)
            {
                rb.velocity = Vector2.right * speed;
            }
            // Go left
            else
            {
                rb.velocity = Vector2.left * speed;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D trigger)
    {
        // The player is in range of the spotting circle
        if (trigger.gameObject.tag == "Player")
        {
            spotted = true;
            player = trigger.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D trigger)
    {
        // The player is no longer in range of the spotting circle
        if (trigger.gameObject.tag == "Player")
        {
            spotted = false;
            player = null;
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        // Hit by bullet
        if (col.gameObject.tag == "Bullet")
        {
            // Stores bullet damage type as a var
            var damageType = col.gameObject.GetComponent<Test_Bullet>().damageType;
            // Stores bullet damage as a var
            var damage = col.gameObject.GetComponent<Test_Bullet>().damage;

            Damage(enemyType, damageType, damage);
        }
    }

    // Function to deal damage (copy this to other enemy scripts for re-use)
    private void Damage(int enemyType, int damageType, float damage)
    {
        // Enemy types (1 = Fire, 2 = Poison, 3 = Ice)
        // Damage types (1 = Fire, 2 = Poison, 3 = Ice, 4 = Basic)
       
        /*Fire > Ice
          Poison > Fire
          Ice > Poison
          Basic is neutral*/

        // Damage type is basic
        if (damageType == 4)
        {
            health -= damage;
        }
        // Damage type is fire
        else if (damageType == 1)
        {
            if (enemyType == 3)         // Enemy is ice (x2 damage)
                health -= damage * 2;
            else if (enemyType == 2)    // Enemy is poison (x1/2 damage)
                health -= damage / 2;
            else                        // Enemy is same type (or other)
                health -= damage;
        }
        // Damage type is poison
        else if (damageType == 2)
        {
            if (enemyType == 1)         // Enemy is fire (x2 damage)
                health -= damage * 2;
            else if (enemyType == 3)    // Enemy is ice (x1/2 damage)
                health -= damage / 2;
            else                        // Enemy is same type
                health -= damage;
        }
        // Damage type is ice
        else if (damageType == 3)       
        {
            if (enemyType == 2)         // Enemy is poison (x2 damage)
                health -= damage * 2;
            else if (enemyType == 1)    // Enemy is fire (x1/2 damage)
                health -= damage / 2;
            else                        // Enemy is same type
                health -= damage;
        }
        else
            // Bullet has no valid damage type
            Debug.Log("Bullet is not a valid type! No damage was dealt.");

        // Enemy is dead
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
