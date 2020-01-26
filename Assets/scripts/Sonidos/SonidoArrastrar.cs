using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonidoArrastrar : MonoBehaviour
{
    static AudioSource audioArrastrar;
    void Start()
    {
        audioArrastrar = GetComponent<AudioSource>();
        //audioArrastrar.Play();
    }

    public static void PlaySound()
    {
        audioArrastrar.mute = false;
    }

    public static void StopSound()
    {
        audioArrastrar.mute = true;
    }
}
