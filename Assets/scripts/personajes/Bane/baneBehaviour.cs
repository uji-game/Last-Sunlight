using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*public class BaneConstructor : ClaseEnemigo {
    private Rigidbody2D rb;
    
    public BaneConstructor(float HP, float Dmg, float posX, float posY, Rigidbody2D rb): base( HP,  Dmg,  posX,  posY) {

        this.rb = rb;

    }
}*/

public class baneBehaviour : MonoBehaviour
{
    public GameObject baneObj;
    private Rigidbody2D baneRB;
    private BoxCollider2D baneBX;


    private movController scMov;
    private BarraDeVida scBarraVida;
    private PauseMenu scPause;
    //private shieldManage scShield;

    public SpriteRenderer rLight;

    public Rigidbody2D saddajRB;

    private float velPatrulla;
    private bool patrullando, pDir, facingRight=false, cazando, lxON; //false derecha, true izq
    public float posBane, posMax, posMin;
    private float posIni;

    //VidaBane
    private int baneHP;
    private Animator baneAnim;
    private bool alive, bAttack, fading,fadeComplete;



    // Start is called before the first frame update
    void Start()
    {
        // BaneConstructor bane = new BaneConstructor(100f, 100f, -13f, -3f, baneRB);
        //scMov = FindObjectOfType<movController>();
        //scShield = FindObjectOfType<shieldManage>();
        scBarraVida = FindObjectOfType<BarraDeVida>();
        scPause = FindObjectOfType<PauseMenu>();


        alive = true;
        bAttack = false;
        fading = false;
        fadeComplete = false;

        //baneObj = GetComponent<GameObject>();
        baneRB = GetComponent<Rigidbody2D>();
        baneBX = GetComponent<BoxCollider2D>();
        baneAnim = GetComponent<Animator>();
        //rLight = GetComponent<SpriteRenderer>();

        posIni = Mathf.Floor(baneRB.transform.position.x);
        //posIni = (baneRB.transform.position.x);

        baneRB.transform.position = new Vector2(posIni, baneRB.transform.position.y); 

        posMax = posIni + 7f;
        posMin = posIni - 7f;
        flip();
        patrullando = true;
        pDir = false;

        //HP
        baneHP= 100;

    }

    // Update is called once per frame
    void Update()
    {
        posBane = Mathf.Floor(baneRB.transform.position.x);
        if(alive && !scPause.gamePaused) movimiento();
        if (alive && !scPause.gamePaused && Mathf.Abs(baneRB.position.x - saddajRB.position.x) < 15f && Mathf.Abs(baneRB.position.y - saddajRB.position.y) < 3.5f) { inRange(); patrullando = false; /*cazando = true;*/ }
        else { patrullando = true;  /*cazando = false;*/  }
        
        if (rLight.enabled) lxON = true;
        else { lxON = false; }
        baneAnim.SetBool("aliveBane", alive); 
        baneAnim.SetBool("attack", bAttack);

        if (fading)
        {
            /*if (baneBX.transform.localScale.x >= 0) { baneBX.transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f); }
            else { fadeComplete = true; }*/
            print("xd");
        }
        if (fadeComplete) { Destroy(this); }
        //print(baneHP);
    }


    public void movimiento()
    {
        if (patrullando)
        {
            bAttack = false;
            //print("Patrusho");

            if ((posBane <= posMax) && !pDir)   //patrulla hacia la derecha
            {

                baneRB.transform.position += new Vector3(0.025f, 0f, 0f);

                if (posBane == posMax)
                {

                    pDir = true;
                    flip();
                }
            }


            else if ((posBane >= posMin) && pDir)   //patrulla hacia la izquierda
            {
                //Debug.Log("hago muchas cosas");

                baneRB.transform.position += new Vector3(-0.025f, 0f, 0f);

                if (posBane == posMin)
                {
                    pDir = false;
                    flip();
                }
            }
        }

        else 
        {
            if ((posBane >= posMin) && !pDir && ((Mathf.Abs(saddajRB.position.x - baneRB.position.x) > 3f)/* && !cazando*/)) { pDir = true; flip(); }
            else if ((posBane <= posMax) && pDir && ((Mathf.Abs(saddajRB.position.x - baneRB.position.x)>3f)/* && !cazando*/)) { pDir = false; flip(); }

        }
    }


    void flipSprite()
    {
        facingRight = !facingRight;
        Vector3 Scale = baneRB.transform.localScale;
        Scale.x *= -1;
        baneRB.transform.localScale = Scale;
    }
    void flip()
    {
        if (!facingRight) { flipSprite(); }
        else if (facingRight) { flipSprite(); }
    }

    private void inRange()
    {
        //if (baneRB.position.x < saddajRB.position.x)
        if ((baneRB.position.x - saddajRB.position.x) < -1.5f)
        {
            bAttack = false;

            baneRB.transform.position += new Vector3(0.05f, 0f);
            if (pDir) { pDir = false; flip(); }
            //print("Te pillo por la izq");
        }


        else if ((baneRB.position.x - saddajRB.position.x) > 1.5f)
        {
            bAttack = false;

            baneRB.transform.position += new Vector3(-0.05f, 0f);
            if (!pDir) { pDir = true; flip(); }

            //print("Te pillo por la dcha");

        }

        else 
        {
            if (!scBarraVida.dead) bAttack = true;
            else bAttack = false;   //no te cebes hdp
        }


    }
    private void OnTriggerStay2D(Collider2D obj)
    {
        if (obj.CompareTag("luzRef") && lxON) 
        {
            baneHP -= 5;
            if ((baneHP) <= 0) 
            {
                //Destroy(this.gameObject); 
                baneHP = 100;
                alive = false; 
                
            }

        }
    }
    void fSaddaj() 
    {
        float difPos = baneRB.position.x - saddajRB.position.x;
       
        if (pDir)   //atacando por la izquierda
        {
            
            if (difPos >= 0 && difPos <= 1.5f) 
            { 
                print("saddaj es apuñalado desde la dcha");
                scBarraVida.vida = 0f;
                scBarraVida.recibirDaño(0f);
                scBarraVida.dead = true;
            }
        }
        else        //atacando por la derecha
        {
            if (difPos <= 0 && difPos >= -1.5f) 
            {
                scBarraVida.vida = 0f;
                scBarraVida.recibirDaño(0f);
                print("saddaj es apuñalado desde la izq");
                scBarraVida.dead = true;

            }
        }

    }

    void fBane() 
    {
        Destroy(baneObj);
        fading = true;
    }

}
