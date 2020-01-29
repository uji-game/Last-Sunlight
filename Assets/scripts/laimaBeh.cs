using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laimaBeh : MonoBehaviour
{
    private bool movement;
    private bool moveAnim;  //Animaciones

    public GameObject saddajGO;
    private Rigidbody2D saddajRB;
    private BoxCollider2D saddajBX;

    private Rigidbody2D laimaRB;
    private BoxCollider2D laimaBX;
    private bool facingLeft = true;

    private Animator laimaAnim;

    // Start is called before the first frame update
    void Start()
    {
        laimaAnim = GetComponent<Animator>();
        laimaRB = GetComponent<Rigidbody2D>();
        laimaBX = GetComponent<BoxCollider2D>();

        saddajRB= saddajGO.GetComponent<Rigidbody2D>();
        saddajBX = saddajGO.GetComponent<BoxCollider2D>();

        movement = false; 
        moveAnim = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Keypad0))         {            moveAnim = !moveAnim;        }//pruebas
        getClose();
        print("movement: "+movement);
        if (facingLeft) print("Miro a la dcha");
        else print("Miro a la izq");

        if (movement)
        {
            
            float localX = laimaRB.transform.localScale.x;
            float localY = laimaRB.transform.localScale.y;
            float localZ = laimaRB.transform.localScale.z;

            if (sadIsOnTheLeft())
            {
                laimaRB.transform.position += new Vector3(-0.065f, 0f, 0f);
            }

            else if (!sadIsOnTheLeft())
            {
                laimaRB.transform.position += new Vector3(0.065f, 0f, 0f);
            }
        }

        laimaAnim.SetBool("LaimaMoving", moveAnim);
    }
    private bool sadIsOnTheLeft() //true si Saddaj esta a la izq de Laima
    {
        if (laimaRB.position.x > saddajRB.position.x) { return true; }

        else { return false; }
    }
    void getClose() 
    {
        if (sadIsOnTheLeft() && !facingLeft) //saddaj por la izq y Laima mirando a la derecha
        {
            flip();
        }
        else if (!sadIsOnTheLeft() && facingLeft) //saddaj por la dcha y Laima mirando a la izq
        {
            flip();
        }
      
        float dist = Mathf.Abs(laimaRB.position.x - saddajRB.position.x);         
     

        if (dist > 15f)
        {
            moveAnim = true;           
        }
        else 
        { 
            moveAnim = false;
            movement = false;
        }
    }

    void earthquake() 
    {
        print("terremoto");
    }

    void LaimaMove()
    {
        movement = true;
    }

    void LaimaStop() 
    {
        movement = false;

    }

    void setMovToFalse() { moveAnim = false; }

    void flipSprite()
    {
        facingLeft = !facingLeft;
        Vector3 Scale = laimaRB.transform.localScale;
        Scale.x *= -1;
        laimaRB.transform.localScale = Scale;
    }
    void flip()
    {
        if (!facingLeft) { flipSprite(); }
        else if (facingLeft) { flipSprite(); }
    }
}
