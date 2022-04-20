﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableColorScript : MonoBehaviour
{

    public Player_Script player;
    public UICol UICollect;
    private bool colorRange = false;
    public CollectableScript CollectableScript;
    // Start is called before the first frame update
    void Start()
    {
        player = player.GetComponent<Player_Script>();
        UICollect = UICollect.GetComponent<UICol>();
        CollectableScript = CollectableScript.GetComponent<CollectableScript>();

    }

    // Update is called once per frame
    void Update()
    {
        if (colorRange == true && CollectableScript.isPickedUp == false)
        {
            float distance = Vector2.Distance(this.gameObject.transform.position, player.gameObject.transform.position)/5f;
            float color = ((30f - distance) / 30f);
            Debug.Log("Distance: " + distance.ToString() + "  Color: " + color.ToString());
            UICollect.image.color = new Color(color, color, color);
        }
    
    }
    private void OnTriggerStay2D(Collider2D trigger)
    {
        if (trigger.tag == "Player")
        {
            colorRange = true;
            Debug.Log("In range of collectable");
        }
    }

    private void OnTriggerExit2D(Collider2D trigger)
    {
        if (trigger.tag == "Player" && UICollect.collected != true)
        {
            colorRange = false;
            UICollect.image.color = Color.black;
        }
    }
}