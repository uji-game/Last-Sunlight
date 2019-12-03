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

    private float velPatrulla;
    private bool patrullando, pDir, facingRight=false; //false derecha, true izq
    public float posBane, posMax, posMin, posIni;
    // Start is called before the first frame update
    void Start()
    {
       // BaneConstructor bane = new BaneConstructor(100f, 100f, -13f, -3f, baneRB);
        //scMov = FindObjectOfType<movController>();

        baneRB = GetComponent<Rigidbody2D>();

        baneRB.transform.position = new Vector2(-35f, 4f); 

        posIni = baneRB.transform.position.x;
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
        //if (baneRB.transform.position.x >= patrulla) { baneRB.transform.position += new Vector3(-2f,0f,0f); }
    }


    public void movimiento()
    {
        if (patrullando)
        {
            //flip();
            if ((posBane <= posMax) && !pDir)
            {
                //Debug.Log(posBane + " " + posMax);

                baneRB.transform.position += new Vector3(0.025f, 0f, 0f);

                if (posBane == posMax)
                {
                    //Debug.Log("hago pocas cosas");

                    pDir = true;
                    flip();
                }
            }
            else if ((posBane >= posMin) && pDir)
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

}
