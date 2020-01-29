using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KozvelAttack : MonoBehaviour
{
    static AudioSource attack;
    void Start()
    {
        attack = GetComponent<AudioSource>();
    }

    public static void PlaySound()
    {
        attack.Play();
    }
}
