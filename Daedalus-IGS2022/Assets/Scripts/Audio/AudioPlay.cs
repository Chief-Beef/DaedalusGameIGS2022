using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlay : MonoBehaviour
{
    public AudioClip[] sounds;
    public AudioSource asource;

    public void PlayClip(int sound)
    {
        asource.PlayOneShot(sounds[sound]);
    }

    public void PlayFirstClip()
    {
        asource.pitch = 1;
        asource.PlayOneShot(sounds[0]);
    }

    public void PlayWithPitch(float pitch)
    {
        asource.pitch = pitch;
        asource.PlayOneShot(sounds[0]);
    }

    public void ChangeVolume(float volume)
    {
        asource.volume = volume;
    }

    public void ChangePitch(float pitch)
    {
        asource.pitch = pitch;
    }
}
