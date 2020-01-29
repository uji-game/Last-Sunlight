using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocksBeha : MonoBehaviour
{
    public GameObject roca; 
    private Rigidbody2D rocaRB;

    public GameObject luz;
    private Rigidbody2D luzRB;
    private Transform luzTr;

    private laimaBeh scLaima;

    public bool caerRoca, hayLuz;


    public Animator luzAnim;


    // Start is called before the first frame update
    void Start()
    {
        scLaima=FindObjectOfType<laimaBeh>();

        caerRoca = false;
        hayLuz = false;
        rocaRB = roca.GetComponent<Rigidbody2D>();

        
        luzRB = luz.GetComponent<Rigidbody2D>();
        luzTr = luz.GetComponent<Transform>();
        /*rockActive = false;
        roca.SetActive(rockActive);*/
    }

    // Update is called once per frame
    void Update()
    {
        //print("hayLuz: " + luzTr.position.y);

        if (luzRB.transform.position.y > 35) { hayLuz = false; }
        else if (luzRB.transform.position.y < 16) { hayLuz = true; }

        //roca.SetActive(rockActive);
        /*if (Input.GetKey(KeyCode.Keypad0)) 
        {
            desaparecer(luz);
        }*/
                    
        if (caerRoca && !hayLuz)//(Input.GetKey(KeyCode.Keypad0))
        {
            
            rocaRB.bodyType = RigidbodyType2D.Dynamic;
        }

        //luzAnim.SetBool("Fade",hayLuz);

        if (hayLuz) { desaparecer(luz); }
      /* {
            rocaRB.bodyType = RigidbodyType2D.Kinematic;
            luz.transform.localScale = new Vector3(0.11f, 1.22746f, 1);
        }*/
    }
    private void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.tag == "muerteRocas") {
            print("palmo");

            caerRoca = false;
            rocaRB.bodyType = RigidbodyType2D.Static;

            rocaRB.transform.position = new Vector2(rocaRB.transform.position.x, 22f);//22
        }
    }
    //Scalar luz en x a 0

    void desaparecer(GameObject luz) 
    {
        float scaleX = luz.transform.localScale.x;
        if (scaleX>0.001 && rocaRB.bodyType ==RigidbodyType2D.Static)
        {
            luz.transform.localScale -= new Vector3(0.0001f, 0, 0);
        }
        else 
        {
            rocaRB.bodyType = RigidbodyType2D.Kinematic;
            hayLuz = false;
            luz.transform.localScale = new Vector3(0.11f, 1.22746f, 1);
        }
    }
}
