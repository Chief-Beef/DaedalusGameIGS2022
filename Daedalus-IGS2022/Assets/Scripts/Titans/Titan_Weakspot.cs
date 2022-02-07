using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Titan_Weakspot : MonoBehaviour
{
    public Animator titanAnm;

    private void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.gameObject.tag == "PlayerAttack")
            titanAnm.SetBool("alive", false);
    }

    /* To use this script, simply place it in a titan's kill zone and attach the animator to this script (the animator must contain the boolean "alive") */
}
