using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script is to be used when text should appear when a GameObject is destroyed

public class Text_On_Destroy : MonoBehaviour
{
    // objects to monitor
    public GameObject destroy;
    // Object to be moved when 'destroy' disappears
    public GameObject textTrigger;
    // spot for trigger to be placed when 'destroy' is destroyed
    public Vector3 targetSpot;

    // Update is called once per frame
    void Update()
    {
        if (destroy == null)
            textTrigger.transform.position = targetSpot;
    }
}
