using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarticleEffect : MonoBehaviour
{

    //Script to manage the gas particle effect 
    //AKA Farticle Effect

    public Transform target;
    public Rigidbody2D player;      //player rigidbody
    public float explosionForce;    //Force of the explosion
    public float explosionRange;    //range of explosion

    // Start is called before the first frame update
    void Awake()
    {
        //NoisyBoi.Instance.MakeNoise(0);
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();

            //if missile blows up near the player then launch them a bit
            if (Vector2.Distance(this.transform.position, target.position) <= explosionRange)
            {
                Vector2 launchAngle = new Vector2(target.position.x - this.transform.position.x, target.position.y - this.transform.position.y);
                Debug.DrawRay(this.transform.position, launchAngle * explosionForce / (Vector2.Distance(this.transform.position, target.position)), Color.cyan, 10f);
                player.AddForce(launchAngle * explosionForce, ForceMode2D.Impulse);  //launch the player
            }
            else
            {
                Vector2 launchAngle = new Vector2(target.position.x - this.transform.position.x, target.position.y - this.transform.position.y);
                launchAngle = launchAngle.normalized;
                Debug.DrawRay(this.transform.position, launchAngle * explosionRange, Color.red, 10f);
            }
        }
    }
}
