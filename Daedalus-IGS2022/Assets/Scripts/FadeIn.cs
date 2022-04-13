using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeIn : MonoBehaviour
{
    // Place this script on a gameObject with a sprite renderer to fade in from black
    private float fade = 1f;
    private SpriteRenderer spr;

    private void Start()
    {
        spr = GetComponent<SpriteRenderer>();
        spr.color = new Color(0, 0, 0, 1);
    }

    void Update()
    {
        // Delete this script once faded in
        if (fade <= 0)
        {
            spr.color = new Color(0, 0, 0, 0);
            Destroy(this);
        }
        spr.color = new Color(0, 0, 0, fade);
        fade -= Time.deltaTime;
    }
}
