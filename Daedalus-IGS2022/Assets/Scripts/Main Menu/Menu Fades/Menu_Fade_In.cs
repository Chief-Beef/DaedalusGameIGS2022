using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu_Fade_In : MonoBehaviour
{
    // I'm feeling faded rn
    private bool fadingIn = true;
    private bool fadingOut = false;
    private float fadeIn = 1.15f;
    // The image that encompasses the screen to fade in and out
    public Image fadeScreen;
    // Menu music
    public AudioSource menuMusic;
    private float musicVolume;
    // Bools determine how to load a scene
    private bool loadingByIndex = false;
    private bool loadingByName = false;
    // Stores scene info for scene that's being loaded
    private int levelIndex = 0;
    private string levelName = " ";
    // Sounds and stuff
    public AudioSource menuSounds;
    public AudioClip click;

    private void Start()
    {
        Cursor.visible = true;
        if (menuMusic != null)
            musicVolume = menuMusic.volume;
    }

    private void Update()
    {
        // Begin fading in
        if (fadingIn && fadeIn > 0.0f)
        {
            fadeScreen.color = new Color(0, 0, 0, fadeIn);
            fadeIn -= Time.deltaTime * 4;

            if (menuMusic != null)
                menuMusic.volume = (1.0f - fadeIn) * musicVolume;
        }
        // Done fading in
        else if (fadingIn && fadeIn <= 0.0f)
        {
            fadeIn = 0.0f;
            fadingIn = false;
            fadeScreen.enabled = false;

            if (menuMusic != null)
                menuMusic.volume = musicVolume;
        }
        // Begin fading out
        if (fadingOut && fadeIn < 1.1f)
        {
            if (fadeScreen.enabled == false)
                fadeScreen.enabled = true;

            fadeScreen.color = new Color(0, 0, 0, fadeIn);
            fadeIn += Time.deltaTime * 4;

            if (menuMusic != null)
                menuMusic.volume = (1.0f - fadeIn) * musicVolume;
        }
        // Done fading out
        else if (fadingOut && fadeIn >= 1.1f)
        {
            // Load scene by index
            if (loadingByIndex)
                SceneManager.LoadScene(levelIndex);
            // Load scene by name
            if (loadingByName)
                SceneManager.LoadScene(levelName);
        }
    }
    // Load scene by build index (int)
    public void BeginLoadSceneByIndex(int sceneIndex)
    {
        menuSounds.PlayOneShot(click);

        loadingByIndex = true;
        levelIndex = sceneIndex;

        fadingOut = true;
    }
    // Load scene by name (string)
    public void BeginLoadSceneByName(string sceneName)
    {
        menuSounds.PlayOneShot(click);

        loadingByName = true;
        levelName = sceneName;

        fadingOut = true;
    }
}
