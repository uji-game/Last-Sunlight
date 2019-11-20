using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class recogerObjeto : MonoBehaviour
{
    private GameObject obj;
    public Text myText;
    private Rigidbody2D sadRB;
    private string r;

    // Start is called before the first frame update
    void Start()
    {
       // player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

       
    }

    private void OnTriggerStay2D(Collider2D pickea)
    {
        if (pickea.tag == "pickeable") {
            //Debug.Log("holap");
            myText.text = "Pulsa F para recoger el objeto";
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
}
