using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class recogerObjeto : MonoBehaviour
{
    private GameObject obj;
    public Text myText;
    private Rigidbody2D sadRB;
    private Transform sad;
    public Animator anim;

    private bool activate;
    private float tRem;
    public bool recoger=false;
    public bool recogido = false;

    public GameObject casa;
    private Scene currentLvl;
    public SpriteRenderer shieldRender;

    //public movController player;

    // Start is called before the first frame update
    void Start()
    {
        tRem = 0.8f;
        anim = GetComponent<Animator>();
        sad = GetComponent<Transform>();

    }
        // Update is called once per frame
    void Update()
    {
        currentLvl = SceneManager.GetActiveScene();
        //Debug.Log(sad.transform.position.x);
        anim.SetBool("Recoger", recoger);
        if (activate) cd();
        if (recogido) finNivel();

    }

    private void OnTriggerStay2D(Collider2D pickea)
    {
        if (pickea.tag == "pickeable") {
            //Debug.Log("holap");
            myText.text = "Pulsa R para recoger el aguita";


            if (Input.GetKey(KeyCode.R)) { 
                pickea.attachedRigidbody.gameObject.SetActive(false); 
                recoger = true; 
                activate = true; 
                recogido = true;
                casa.SetActive(true);
            }           
        }

        else if (pickea.tag == "takeShield")
        {
            //Debug.Log("holap");
            myText.text = "Pulsa R para recoger el escudo";


            if (Input.GetKey(KeyCode.R))
            {
                pickea.attachedRigidbody.gameObject.SetActive(false);
                recoger = true;
                activate = true;
                //recogido = true;
                //casa.SetActive(true);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D pj)
    {
       
        if (pj.tag == "pickeable" || pj.tag == "takeShield")
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

    void finNivel()
    {
        if (currentLvl.name == "Nivel 1")
        {
            if (TestRange(sad.transform.position.x, 31, 33))
                SceneManager.LoadScene("Scenes/diapos2");
        }
    }

    bool TestRange(float numberToCheck, int bottom, int top)
    {
        return (numberToCheck >= bottom && numberToCheck <= top);
    }
}
