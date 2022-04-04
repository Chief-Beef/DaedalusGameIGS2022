using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableScript : MonoBehaviour
{
    public bool isPickedUp = false;
    public string itemType;
    public UICol UICollect;
    public Player_Script player;
    //public CollectableSounds allCollected;
    //public bool collected = false;

    private void Start()
    {
        UICollect = UICollect.GetComponent<UICol>();
        player = player.GetComponent<Player_Script>();
        //allCollected = allCollected.GetComponent<CollectableSounds>();
        
    }

    private void Update()
    {
        
        if (isPickedUp == true) 
        {
            //figure out what type of item has been picked up and set it to collected
            switch (itemType) 
            {
                case "1/7": 
                    UICollect.collected = true;
                    break;

                case "2/7":
                    UICollect.collected = true;
                    break;

                case "3/7":
                    UICollect.collected = true;
                    break;

                case "4/7":
                    UICollect.collected = true;
                    break;

                case "5/7":
                    UICollect.collected = true;
                    break;

                case "6/7":
                    UICollect.collected = true;
                    break;

                case "7/7":
                    UICollect.collected = true;
                    break;

                default:
                    break;

            }

            if (player.items.Count == 7) 
            {
                //CollectableSounds.colInstance.playFnaf();
            }

            
        }
    }
}
