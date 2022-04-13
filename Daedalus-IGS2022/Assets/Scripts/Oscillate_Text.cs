using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Oscillate_Text : MonoBehaviour
{
    public Text text;
    private bool fadeIn = true;
    private float opacity = 0.0f;

    private void Update()
    {
        if (text.enabled == true)
        {
            if (fadeIn)
            {
                text.color = new Color(255, 255, 255, opacity);
                opacity += Time.deltaTime;

                if (opacity >= 1f)
                {
                    fadeIn = false;
                }
            }
            else if (!fadeIn)
            {
                text.color = new Color(255, 255, 255, opacity);
                opacity -= Time.deltaTime;

                if (opacity <= 0f)
                {
                    fadeIn = true;
                }
            } 
        }
        else
        {
            opacity = 0f;
            fadeIn = true;
        }
    }
}
