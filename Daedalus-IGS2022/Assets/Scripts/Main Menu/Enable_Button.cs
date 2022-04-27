using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enable_Button : MonoBehaviour
{
    // This script enables or disables a button based on how far the player has progressed
    // Meant to be used on level select screen to disable levels past what player has unlocked

    // Attach this script to a UI element that blocks the button behind it

    // Player must be this far in the game to select the button
    public int requiredLevel;

    private void Start()
    {
        // Progression int is used to keep track of how far player has progressed
        int progression = PlayerPrefs.GetInt("Level", -1);

        if (requiredLevel <= progression)
        {
            this.gameObject.SetActive(true);
        }
        else
            this.gameObject.SetActive(false);

        if (Cursor.visible == false)
            Cursor.visible = true;
    }
}
