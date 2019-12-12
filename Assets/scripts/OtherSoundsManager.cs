using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherSoundsManager : MonoBehaviour
{
    // Start is called before the first frame update

    new static AudioSource audio;
        static AudioClip audioJump;
        static AudioClip audioFall;

    void Start()
    {
        audio = GetComponent<AudioSource>();

        audioJump = Resources.Load<AudioClip>("WHOOSH_Air_Very_Fast_RR1_mono");
        //audioFall = Resources.Load<AudioClip>("//Assets/Universal Sound FX/WHOOSHES/Air/WHOOSH_Air_Very_Fast_RR1_mono.wav");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "jump":
                audio.Play();
                break;
            case "fall":
                audio.PlayOneShot(audioFall);
                break;
        }
    }
}
