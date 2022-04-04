using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Kill_The_Titans : MonoBehaviour
{
    public GameObject[] titans;
    public int remainingTitans;

    private GameObject player;
    public GameObject fadeOut;
    private SpriteRenderer fadeOutSpr;
    private float fade = -0.5f;
    private bool fading = false;

    public void KillTitan()
    {
        remainingTitans--;
        if (remainingTitans <= 0)
        {
            fading = true;
            player = GameObject.FindGameObjectWithTag("Player");
            fadeOutSpr = fadeOut.GetComponent<SpriteRenderer>();
        }
    }

    private void Update()
    {
        if (fading)
        {
            fadeOut.transform.position = player.transform.position;
            fade += Time.deltaTime;
            fadeOutSpr.color = new Color(0, 0, 0, fade);

            if (fade >= 1.0f)
            {
                SceneManager.LoadScene(0);
            }
        }
    }
}
