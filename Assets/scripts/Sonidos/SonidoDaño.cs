using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonidoDaño : MonoBehaviour
{
    static AudioSource audioDaño;
    void Start()
    {
        audioDaño = GetComponent<AudioSource>();
    }

    public static void PlaySound()
    {
        audioDaño.mute = false;
    }

    public static void StopSound()
    {
        audioDaño.mute = true;
    }
}
