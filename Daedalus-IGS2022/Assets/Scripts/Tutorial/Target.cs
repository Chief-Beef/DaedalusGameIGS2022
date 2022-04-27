using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public Target_Counter tc;
    public GameObject breakSound;

    private void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.gameObject.tag == "PlayerAttack")
        {
            AddToCounter();
            Instantiate(breakSound, this.transform.position, Quaternion.identity, null);
            Destroy(this.gameObject);
        }
    }

    private void AddToCounter()
    {
        tc.AddTargetDown();
    }
}
