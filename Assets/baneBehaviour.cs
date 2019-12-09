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
    private Rigidbody2D baneRB;
    private movController scMov;

    public Rigidbody2D saddajRB;

    private float velPatrulla;
    private bool patrullando, pDir, facingRight=false, cazando; //false derecha, true izq
    public float posBane, posMax, posMin;
    private float posIni;
    // Start is called before the first frame update
    void Start()
    {
       // BaneConstructor bane = new BaneConstructor(100f, 100f, -13f, -3f, baneRB);
        //scMov = FindObjectOfType<movController>();

        baneRB = GetComponent<Rigidbody2D>();

        posIni = Mathf.Floor(baneRB.transform.position.x);
        //posIni = (baneRB.transform.position.x);

        baneRB.transform.position = new Vector2(posIni, baneRB.transform.position.y); 

        posMax = posIni + 7f;
        posMin = posIni - 7f;
        flip();
        patrullando = true;
        pDir = false;

    }

    // Update is called once per frame
    void Update()
    {
        posBane = Mathf.Floor(baneRB.transform.position.x);
        movimiento();
        if (Mathf.Abs(baneRB.position.x - saddajRB.position.x) < 15f) { inRange(); patrullando = false; /*cazando = true;*/ }
        else { patrullando = true;  /*cazando = false;*/  }
        
        //rint("Cazando: "+ cazando+"\nPatrullando: "+patrullando+"\npDir: "+pDir);
    }


    public void movimiento()
    {
        if (patrullando)
        {
            print("Patrusho");

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
            baneRB.transform.position += new Vector3(0.05f, 0f);
            if (pDir) { pDir = false; flip(); }
            print("Te pillo por la izq");
        }


        else if ((baneRB.position.x - saddajRB.position.x) > 1.5f)
        {
            baneRB.transform.position += new Vector3(-0.05f, 0f);
            if (!pDir) { pDir = true; flip(); }

            print("Te pillo por la dcha");

        }


    }

    void OnTriggerStay2D(Collider2D entity)
    {
        if (entity.CompareTag("luzRef")) { Debug.Log("Bane se hace pupa"); }
    }

}
