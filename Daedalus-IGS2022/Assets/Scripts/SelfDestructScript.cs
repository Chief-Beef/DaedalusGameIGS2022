using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestructScript : MonoBehaviour
{
    /*
    This script is for destroying particle effects
    Adjust killTimer in the editor to whatever time
    you want for the lifetime of the particle system 
    */

    public float killTimer;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, killTimer);
    }
    
}
