using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonidoArrastrar : MonoBehaviour
{
    static AudioSource audioArrastrar;
    void Start()
    {
        audioArrastrar = GetComponent<AudioSource>();
    }

    public static void PlaySound()
    {
        audioArrastrar.Play();
    }
}
