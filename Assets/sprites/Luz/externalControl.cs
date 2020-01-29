using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class externalControl : MonoBehaviour
{
    // Start is called before the first frame update
    private rocksBeha scRocks;
    public bool fadeComplete;
    private Animator myAnim;
    void Start()
    {
        fadeComplete = false;
        scRocks = FindObjectOfType<rocksBeha>();
    }

    // Update is called once per frame
    void Update()
    {
        print("hayLuz: " + scRocks.hayLuz);

        //myAnim.SetBool("Fade",scRocks.hayLuz );
    }
    void fadeComp() 
    {
        fadeComplete = true;
        scRocks.hayLuz = false;
    }
}
