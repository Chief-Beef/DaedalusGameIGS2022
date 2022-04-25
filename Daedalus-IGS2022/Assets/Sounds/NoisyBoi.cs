using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoisyBoi : MonoBehaviour
{

    public static NoisyBoi Instance;
    public AudioSource shootyNoise;     //CoD Hitmarker
    public AudioSource vineBoom;        //annoying vine sound
    public AudioSource windowsError;    //basic titan death sound
    public AudioSource tacoBell;        //annoying tbell sound

    //public AudioSource[] soundCollection = new AudioSource[4];


    // Start is called before the first frame update
    void Start()
    {
        Instance = this;

        //soundCollection[0] = shootyNoise;
        //soundCollection[1] = vineBoom;
        //soundCollection[2] = windowsError;
        //soundCollection[3] = tacoBell;

    }

    public void MakeNoise(int num)
    {
        //soundCollection[num].Play();

        shootyNoise.Play();
    }
}