using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyPlayerDetectionScript : MonoBehaviour
{
    public GameObject parentObject;
    public Transform Player;
    public Transform TheForbidenOne;
    public bool inRange;

    private void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.gameObject.tag == "Player")
        {
            inRange = true;
        }
    }

    private void OnTriggerExit(Collider trigger)
    {
        if (trigger.gameObject.tag == "Player")
        {
            inRange = false;
        }
    }
}