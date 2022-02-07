using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorScript : MonoBehaviour
{

    public static CursorScript Instance;

    public Texture2D grappleCursor;
    public Texture2D fireGun;
    public Texture2D iceGun;
    public Texture2D poisonGun;

    public Texture2D[] cursors = new Texture2D[3];

    public int active;

    public KeyCode switchWeapon;
    public KeyCode grappleButton;

    public Vector2 mouseOffset = new Vector2(12.5f,12.5f);

    public bool grappleActive;

    //Click on the cursor you want to use, go to max size and change it from 2048 to 32

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;

        //order can be changed who cares
        cursors[0] = fireGun;
        cursors[1] = iceGun;
        cursors[2] = poisonGun;

        active = 0;

        //on start grapple active, can change to whatever
        Cursor.SetCursor(grappleCursor, mouseOffset, CursorMode.ForceSoftware);
        grappleActive = true;
    }


    // Update is called once per frame
    void Update()
    {

        //when you switch weapons it changes the cursor
        //active%3 to remove index out of bounds error

        if(Input.GetKeyDown(switchWeapon))
        {
            active = (active + 1) % 3;
            Cursor.SetCursor(cursors[active], mouseOffset, CursorMode.ForceSoftware);
            grappleActive = false;
            //Audio.play for weapon swap noise
        }

        if (Input.GetKeyDown(grappleButton))
        {
            Cursor.SetCursor(grappleCursor, mouseOffset, CursorMode.ForceSoftware);
            grappleActive = true;
        }

    }

}
