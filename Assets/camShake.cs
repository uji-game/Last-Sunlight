using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Events;

public class camShake : MonoBehaviour
{
    public CinemachineVirtualCamera VirtualCamera;
    private CinemachineBasicMultiChannelPerlin virtualCameraNoise;
    //public Camera vcam;

    public float ShakeDuration = 0.3f;
    public float ShakeAmplitude = 1.2f;
    public float ShakeFrequency = 2.0f;

    private float ShakeElapsedTime = 0;

    public bool shakeON;
    // Start is called before the first frame update
    void Start()
    {
        shakeON = false;
        if (VirtualCamera != null) 
        {
            virtualCameraNoise = VirtualCamera.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: Replace with your trigger
        /*if (Input.GetKey(KeyCode.Keypad0))
        {
            ShakeElapsedTime = ShakeDuration;
        }

        // If the Cinemachine componet is not set, avoid update
        if (VirtualCamera != null && virtualCameraNoise != null)
        {
            // If Camera Shake effect is still playing
            if (ShakeElapsedTime > 0)
            {
                // Set Cinemachine Camera Noise parameters
                virtualCameraNoise.m_AmplitudeGain = ShakeAmplitude;
                virtualCameraNoise.m_FrequencyGain = ShakeFrequency;

                // Update Shake Timer
                ShakeElapsedTime -= Time.deltaTime;
            }
            else
            {
                // If Camera Shake effect is over, reset variables
                virtualCameraNoise.m_AmplitudeGain = 0f;
                ShakeElapsedTime = 0f;
            }
        }*/
        print("ShakeON: "+shakeON);
        shake(shakeON);
        /*if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            shakeON = true;
            //print("shakeON");
            //Invoke("stopShake", 1.5f);
            //shake();
            //shake(0.1f, 2f);
            //vcam.m_Lens.Dutch = 0;

        }
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {

            shakeON = false;//Invoke("stopShake", 1.5f);
            //print("shakeOFF");


        }*/
    }
    private void shake(bool enable) 
    {
        if (/*Input.GetKey(KeyCode.Keypad0)*/enable)
        {
            ShakeElapsedTime = ShakeDuration;
        }

        // If the Cinemachine componet is not set, avoid update
        if (VirtualCamera != null && virtualCameraNoise != null)
        {
            // If Camera Shake effect is still playing
            if (ShakeElapsedTime > 0)
            {
                // Set Cinemachine Camera Noise parameters
                virtualCameraNoise.m_AmplitudeGain = ShakeAmplitude;
                virtualCameraNoise.m_FrequencyGain = ShakeFrequency;

                // Update Shake Timer
                ShakeElapsedTime -= Time.deltaTime;
            }
            else
            {
                // If Camera Shake effect is over, reset variables
                virtualCameraNoise.m_AmplitudeGain = 0f;
                ShakeElapsedTime = 0f;
            }
        }
    }

    void stopShake() 
    {
        CancelInvoke();
    }

}
