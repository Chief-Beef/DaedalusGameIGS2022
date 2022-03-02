using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoisyBoi : MonoBehaviour
{

    public static NoisyBoi Instance;
    public AudioSource shootyNoise;
    public AudioSource vineBoom;
    public AudioSource windowsError;
    public AudioSource tacoBell;

    public AudioSource[] soundCollection = new AudioSource[4];

    private int soundRand;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;

        soundCollection[0] = shootyNoise;
        soundCollection[1] = vineBoom;
        soundCollection[2] = windowsError;
        soundCollection[3] = tacoBell;


        soundRand = Random.Range(0, soundCollection.Length);
        Debug.Log("soundRand:\t" + soundRand);
    }

    public void MakeNoise()
    {
        soundCollection[soundRand].Play();
    }
}