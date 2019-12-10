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

        //audioJump = ./Assets.Universal Sound FX/ THUDS_THUMPS
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySound(string clip)
    {
        switch (clip)
        {
            case "jump":
                audio.PlayOneShot(audioJump);
                break;
            case "fall":
                audio.PlayOneShot(audioFall);
                break;
        }
    }
}
