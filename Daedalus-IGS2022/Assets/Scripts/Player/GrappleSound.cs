using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleSound : MonoBehaviour
{
    public bool grappling;
    private bool reset = true;

    // Player rigidbody
    public Rigidbody2D rb;

    public AudioSource grappleSounds;

    void Update()
    {
        if (grappling)
        {
            grappleSounds.enabled = true;
            grappleSounds.volume = Mathf.Clamp(rb.velocity.magnitude / 200, 0, 0.5f);
            reset = false;
        }
        else if (!grappling && !reset)
        {
            grappleSounds.enabled = false;
            reset = true;
        }
    }
}
