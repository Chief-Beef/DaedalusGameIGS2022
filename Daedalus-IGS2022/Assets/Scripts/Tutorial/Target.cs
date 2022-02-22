using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public Target_Counter tc;

    private void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.gameObject.tag == "PlayerAttack")
        {
            AddToCounter();
            Destroy(this.gameObject);
        }
    }

    private void AddToCounter()
    {
        tc.AddTargetDown();
    }
}
