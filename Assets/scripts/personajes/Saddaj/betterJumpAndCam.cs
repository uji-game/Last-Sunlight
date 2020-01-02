using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class betterJumpAndCam : MonoBehaviour
{    
    public float fallMult = 2.5f;
    public float lowJumpMult = 2f;
    public float maxZoom = 10f;
    public float minZoom = 5f;

    public CinemachineVirtualCamera vcam;
    /*var camera = Camera.main;
    var brain = (camera == null) ? null : camera.GetComponent<CinemachineBrain>();
    var vcam = (brain == null) ? null : brain.AciveVirtualCamera as CinemachineVirtualCamera;*/

    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMult - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetButtonDown("Jump"))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMult - 1) * Time.deltaTime;
        }

        ZoomOut();
    }

    void ZoomOut() 
    {
        if (Input.GetKey(KeyCode.Z) && vcam.m_Lens.OrthographicSize < maxZoom)
        {
           

            //cam.lensShift    orthographicSize = 10;
            vcam.m_Lens.OrthographicSize += 0.05f;
        }
        else if( !(Input.GetKey(KeyCode.Z)) && vcam.m_Lens.OrthographicSize>minZoom) { vcam.m_Lens.OrthographicSize -= 0.1f; }
    }
   
}
