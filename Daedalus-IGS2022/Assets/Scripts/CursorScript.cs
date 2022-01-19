using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorScript : MonoBehaviour
{

    public Texture2D baseCursor;
    public Vector2 mouseOffset = new Vector2(12.5f,12.5f); 

    // Start is called before the first frame update
    void Start()
    {
        Cursor.SetCursor(baseCursor, mouseOffset, CursorMode.ForceSoftware);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
