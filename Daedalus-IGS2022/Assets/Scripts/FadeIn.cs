using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour
{
    // Place this script on a gameObject with a sprite renderer to fade in from black
    private float fade = 1f;
    private Image fadeImg;
    private bool fadingIn = true;
    private bool fadingOut = false;

    public GameObject pauseMenu;

    // Set variables and initialize
    private void Start()
    {
        fadeImg = GameObject.FindGameObjectWithTag("Fade").GetComponent<Image>();
        fadeImg.enabled = true;
        fadeImg.color = new Color(0, 0, 0, 1);
    }

    // Fade out and progress player to a new scene
    // Call this function when the player beats a level
    public void FadeOut(int scene)
    {
        fadingOut = true;
        GetComponent<LevelProgression>().UpdateLevel(scene);
    }

    void Update()
    {
        // Fade into a scene from black
        if (fadingIn)
        {
            fadeImg.color = new Color(0, 0, 0, fade);
            fade -= Time.deltaTime;
            // Stop fading in after opacity is 0
            if (fade <= 0f)
            {
                fadingIn = false;
                fadeImg.color = new Color(0, 0, 0, 0);
            }
        }
        // Fade back out to to black back to the menu
        else if (fadingOut)
        {
            fadeImg.color = new Color(0, 0, 0, fade);
            fade += Time.deltaTime;
            // Load menu scene once opacity is 1
            if (fade >= 1f)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(0);
            }
        }
        // Game can be paused when not fading in or out
        if (!fadingIn && !fadingOut)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                // Disable menu
                if (pauseMenu.activeInHierarchy)
                {
                    pauseMenu.SetActive(false);
                    Time.timeScale = 1;
                }
                // Enable menu
                else
                {
                    pauseMenu.SetActive(true);
                    Time.timeScale = 0;
                }
            }
            else if (pauseMenu.activeInHierarchy && Input.GetKeyDown(KeyCode.R))
            {
                pauseMenu.SetActive(false);
                Time.timeScale = 1;

                FadeOut(0);
            }
        }

    }
}
