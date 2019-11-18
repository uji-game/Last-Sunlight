using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class recogerObjeto : MonoBehaviour
{
    public Text myText;
    private Rigidbody2D sadRB;
    private string r;

    // Start is called before the first frame update
    void Start()
    {
        myText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {

       myText.Text = r;
    }

    private void OnTriggerEnter2D(Collider2D pj)
    {
        if (pj.tag == "pickeable") {
            Debug.Log("holap");
            r = "Pulsa F para recoger";
        }
    }

}
