using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioControll : MonoBehaviour
{

    public AudioMixer masterMixer;

    public void SetFXVolume(float fxVolume)
    {
        masterMixer.SetFloat("FXVolume", fxVolume);
    }

    public void SetMusicVolume(float MusicVolume)
    {
        masterMixer.SetFloat("MusicVolume", MusicVolume);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
