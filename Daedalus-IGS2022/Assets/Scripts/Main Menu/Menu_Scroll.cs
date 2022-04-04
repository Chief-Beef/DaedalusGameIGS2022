using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu_Scroll : MonoBehaviour
{
    public GameObject anchor;
    public float maxY;
    public Scrollbar scrollbar;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateScrollBar()
    {
        anchor.transform.position = new Vector2(anchor.transform.position.x, 540 + maxY * scrollbar.value);
    }
}
