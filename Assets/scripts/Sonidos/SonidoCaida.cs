using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonidoCaida : MonoBehaviour
{
    // Start is called before the first frame update
    static AudioSource audioSalto;
    void Start()
    {
        audioSalto = GetComponent<AudioSource>();
    }

    public static void PlaySound()
    {
        audioSalto.Play();
    }
}
