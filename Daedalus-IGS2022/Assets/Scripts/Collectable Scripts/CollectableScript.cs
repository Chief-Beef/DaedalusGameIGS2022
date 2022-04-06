using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableScript : MonoBehaviour
{
    public bool isPickedUp = false;
    public string itemType;
    public UICol UICollect;
    public Player_Script player;
    public AudioSource FnafYay;
    public AudioClip clip;
    public float volume = 1f;
    public bool ready2PlayFnaf = true;
    //public CollectableSounds allCollected;
    //public bool collected = false;

    private void Start()
    {
        UICollect = UICollect.GetComponent<UICol>();
        player = player.GetComponent<Player_Script>();
        FnafYay = FnafYay.GetComponent<AudioSource>();
        FnafYay.clip = clip;

        //FnafYay.PlayOneShot(clip, volume);
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
                //ready2PlayFnaf = true;
                if (ready2PlayFnaf)
                {
                    Debug.Log("Play Fnaf Sound");
                    FnafYay.PlayOneShot(clip, volume);
                    ready2PlayFnaf = false;
                    if (ready2PlayFnaf == false)
                    {
                        Debug.Log("ready2PlayFnaf is false");
                    }
                }
            }

            
        }
    }
}
