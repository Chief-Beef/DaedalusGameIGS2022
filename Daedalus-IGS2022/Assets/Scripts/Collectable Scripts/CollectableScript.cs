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
    public GameObject BurgerFormation;
    public Animator anim;
    private bool colorRange = false;
    //public CollectableSounds allCollected;
    //public bool collected = false;

    public AudioSource collectibleSoundPlayer;
    public AudioClip shine;

    private void Start()
    {
        UICollect = UICollect.GetComponent<UICol>();
        player = player.GetComponent<Player_Script>();
        FnafYay = FnafYay.GetComponent<AudioSource>();
        FnafYay.clip = clip;
        anim = BurgerFormation.GetComponent<Animator>();

        //FnafYay.PlayOneShot(clip, volume);
        //allCollected = allCollected.GetComponent<CollectableSounds>();
    }

    // Play pickup sound
    public void PlaySound()
    {
        if (player.items.Count != 7)
            collectibleSoundPlayer.PlayOneShot(shine);
    }

    private void Update()
    {
        if (isPickedUp == true)
        {
            //figure out what type of item has been picked up and set it to collected
            switch (itemType)
            {
                case "TopBun":
                    UICollect.collected = true;
                    break;

                case "Bacon":
                    UICollect.collected = true;
                    break;

                case "Cheese":
                    UICollect.collected = true;
                    break;

                case "Lettuce":
                    UICollect.collected = true;
                    break;

                case "Tomato":
                    UICollect.collected = true;
                    break;

                case "Patty":
                    UICollect.collected = true;
                    break;

                case "BottomBun":
                    UICollect.collected = true;
                    break;

                default:
                    break;

            }

            if (player.items.Count == 7)
            {
                anim.Play("BurgerAssemble");
                //ready2PlayFnaf = true;
                if (ready2PlayFnaf)
                {
                    FnafYay.PlayOneShot(clip, 0.075f);
                    ready2PlayFnaf = false;
                }
            }
        }

    }
   
}
