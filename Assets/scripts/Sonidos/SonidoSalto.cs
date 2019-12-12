using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonidoSalto : MonoBehaviour
{
    static AudioSource audioCaida;
    void Start()
    {
        audioCaida = GetComponent<AudioSource>();
    }

    public static void PlaySound()
    {
        audioCaida.Play();
    }
}
