using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingLint : MonoBehaviour
{

    public float speed;
    public Transform target;


    // Start is called before the first frame update
    void Start()
    {
        // find player location
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(this.transform.position, target.position, speed * Time.deltaTime);
    }
}
