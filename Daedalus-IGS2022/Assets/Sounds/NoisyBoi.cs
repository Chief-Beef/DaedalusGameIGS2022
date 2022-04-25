using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoisyBoi : MonoBehaviour
{

    public static NoisyBoi Instance;
    public AudioSource shootyNoise;
    public AudioSource windowsError;
    public AudioSource tacoBellBong;
    public AudioSource vineBoom;
    public AudioSource classicHurt;

    //public AudioSource[] soundCollection = new AudioSource[2];


    // Start is called before the first frame update
    void Start()
    {
        Instance = this;

    }

    public void MakeNoise(int num)
    {
        switch (num)
        {
            case 0:
                shootyNoise.Play();
                break;
            case 1:
                windowsError.Play();
                break;
            case 2:
                tacoBellBong.Play();
                break;
            case 3:
                vineBoom.Play();
                break;
            case 4:
                classicHurt.Play();
                break;
            default:
                shootyNoise.Play();
                break;
        }
    }
}