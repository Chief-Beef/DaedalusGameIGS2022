using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarticleEffect : MonoBehaviour
{

    //Script to manage the gas particle effect 
    //AKA Farticle Effect

    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxis("Fire2") > 0)
        {

        }
    }
}
