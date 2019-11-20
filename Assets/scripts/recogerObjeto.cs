using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class recogerObjeto : MonoBehaviour
{
    private GameObject obj;
    public Text myText;
    private Rigidbody2D sadRB;
    public Animator anim;

    private bool activate;
    private float tRem;
    public bool recoger=false;

    //public movController player;

    // Start is called before the first frame update
    void Start()
    {
        tRem = 0.8f;
        anim = GetComponent<Animator>();
        
        
    }
        // Update is called once per frame
    void Update()
    {


        anim.SetBool("Recoger", recoger);
        if (activate) cd();
        
    }

    private void OnTriggerStay2D(Collider2D pickea)
    {
        if (pickea.tag == "pickeable") {
            //Debug.Log("holap");
            myText.text = "Pulsa R para recoger el objeto";

            if (Input.GetKey(KeyCode.R)) { pickea.attachedRigidbody.gameObject.SetActive(false); recoger = true; activate = true; }
           

            if (Input.GetKey(KeyCode.R)) {  pickea.attachedRigidbody.gameObject.SetActive(false) ; }
        }
    }
    private void OnTriggerExit2D(Collider2D pj)
    {
       
        if (pj.tag == "pickeable")
        {
            myText.text = "";
        }
    }

    void cd()
    {

        tRem -= Time.deltaTime;
        if (tRem <= 0)
        {
            activate = false;
            tRem = 0.8f;
            recoger = false;
            
        }

    }
}
