using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Kill_The_Titans : MonoBehaviour
{
    public GameObject[] titans;
    public int remainingTitans;

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

            // Begins fading out to black
            GameObject.FindGameObjectWithTag("Fade").GetComponent<FadeIn>().FadeOut(1);
        }
    }
}
