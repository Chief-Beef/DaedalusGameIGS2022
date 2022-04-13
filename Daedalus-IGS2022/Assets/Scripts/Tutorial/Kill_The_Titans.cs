using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Kill_The_Titans : MonoBehaviour
{
    public GameObject[] titans;
    public int remainingTitans;

    public GameObject fadeOut;
    private SpriteRenderer fadeOutSpr;
    private float fade = -0.5f;
    private bool fading = false;

    public LevelProgression prog;

    public void KillTitan()
    {
        // Keeps track of how many titans are remaining
        // Will progress to new level in player prefs once
        // all titans are dead
        remainingTitans--;
        if (remainingTitans <= 0)
        {
            // Updates current level
            prog.UpdateLevel(1);
            // Begins fading
            fading = true;
            fadeOutSpr = fadeOut.GetComponent<SpriteRenderer>();
        }
    }

    private void Update()
    {
        if (fading)
        {
            // Fade out
            fade += Time.deltaTime;
            fadeOutSpr.color = new Color(0, 0, 0, fade);
            // Load scene when done fading
            if (fade >= 1.0f)
            {
                SceneManager.LoadScene(0);
            }
        }
    }
}
