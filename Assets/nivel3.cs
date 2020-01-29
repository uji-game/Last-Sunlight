using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class nivel3 : MonoBehaviour
{
    public CinemachineVirtualCamera vcam;
    //public Camera vcam;

    private float shakeAm = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad0))
        { 
            print("xxxx");
            //shake(0.1f, 2f);
           /* vcam.m_Lens.Dutch++;//1
            vcam.m_Lens.Dutch--;//0
            vcam.m_Lens.Dutch--;//-1
            vcam.m_Lens.Dutch++;//0
            
            vcam.m_Lens.Dutch++;//1
            vcam.m_Lens.Dutch--;//0
            vcam.m_Lens.Dutch--;//-1
            vcam.m_Lens.Dutch++;//0

            vcam.m_Lens.Dutch = 0;*/

        }
    }


    public void shake(float amt, float lenght) 
    {
        shakeAm = amt;
        InvokeRepeating("startShake", 0, 0.01f);
        Invoke("endShake", lenght);

    }

    void startShake() 
    {
        if (shakeAm > 0) 
        {
            Vector3 camPos = vcam.transform.position;

            float offsetX = Random.value * shakeAm * 2 - shakeAm;

            float offsetY = Random.value * shakeAm * 2 - shakeAm;

            camPos.x = offsetX;
            camPos.y = offsetY;

            vcam.transform.position = camPos;
        }
    }
    void endShake() 
    {
        CancelInvoke("startShake");
        vcam.transform.localPosition = Vector3.zero;
    }
}
