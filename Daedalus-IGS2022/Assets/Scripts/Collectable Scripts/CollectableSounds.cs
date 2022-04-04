using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableSounds : MonoBehaviour
{
    public static CollectableSounds colInstance;
    public AudioSource FnafYay;
    public AudioSource[] sound = new AudioSource[1];


    // Start is called before the first frame update
    void Start()
    {
        colInstance = this;

        sound[0] = FnafYay;
    }

    // Update is called once per frame
    public void playFnaf()
    {
        //sound[0].Play();
    }
}
