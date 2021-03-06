﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class laimaBeh : MonoBehaviour
{
    private bool movement, luzRefON;
    public float LaimaHP;
    private bool moveAnim, eqAnim, meleeAnim, laimaAlive, blink;  //Animaciones
    public int numGolpes;

    public GameObject saddajGO;
    private Rigidbody2D saddajRB;
    private BoxCollider2D saddajBX;

    private Rigidbody2D laimaRB;
    private BoxCollider2D laimaBX;
    private SpriteRenderer laimaSR;
    private bool facingLeft = true;

    //Luz    
    public GameObject refGO;
    private Rigidbody2D refRB;
    public SpriteRenderer refSR;

    private Animator laimaAnim;

    private movController scMov;
    private BarraDeVida scBarraVida;
    private PauseMenu scPause;
    private camShake scShakeCam;
    private rocksBeha scRocks;

    new AudioSource audio;

    public int availableEQ, maxEQ;  //Nº de terremotos que puede lanzar

    // Start is called before the first frame update
    void Start()
    {
        numGolpes = 0;
        laimaAlive = true;
        LaimaHP = 200;

        laimaAnim = GetComponent<Animator>();
        laimaRB = GetComponent<Rigidbody2D>();
        laimaBX = GetComponent<BoxCollider2D>();
        laimaSR = GetComponent<SpriteRenderer>();

        saddajRB= saddajGO.GetComponent<Rigidbody2D>();
        saddajBX = saddajGO.GetComponent<BoxCollider2D>();
        //Luz     
        refRB = refGO.GetComponent<Rigidbody2D>();
        //
        scMov = FindObjectOfType<movController>();
        scBarraVida = FindObjectOfType<BarraDeVida>();
        scPause= FindObjectOfType<PauseMenu>();
        scShakeCam = FindObjectOfType<camShake>();
        scRocks = FindObjectOfType<rocksBeha>();

        audio = GetComponent<AudioSource>();

        movement = false; 
        moveAnim = false;
        eqAnim = false;
        meleeAnim = false;
        blink = false;

        maxEQ = 1;
        availableEQ = maxEQ;


    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKey(KeyCode.Keypad0)) { blink=true; }
        //print("Laima Alive UPDATE: " + laimaAlive);

        if (scBarraVida.vida <= 0f) { scBarraVida.dead = true; }

        /*if (LaimaHP <= 0) 
        { 
            //print("Laima Alive UP IF: " + laimaAlive); 
            laimaAlive = false; 
            meleeAnim = false;
            eqAnim = false;

            
        }*/
        if(numGolpes==3)
        {
            laimaAlive = false;
            meleeAnim = false;
            eqAnim = false;
        }
        
        if (!scPause.gamePaused && !scBarraVida.dead && laimaAlive)
        {
            if (refSR.enabled) luzRefON = true;
            else { luzRefON = false; }

            getClose();
            

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

            
        }
        laimaAnim.SetBool("LaimaMoving", moveAnim);
        laimaAnim.SetBool("RangedAttack", eqAnim);
        laimaAnim.SetBool("MeleeAttack", meleeAnim);
        laimaAnim.SetBool("LaimaLife", laimaAlive);
        laimaAnim.SetBool("LaimaBlink", blink);
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

        if (dist > 8f) //Saddaj se sale del rango //Modificar la distancia (acortarla)
        {
            meleeAnim = false;
            //elegir entre ir a por saddaj 
            moveAnim = true;      //Ir a por Saddaj     

            //o atacar a distancia

        }
        else            //Saddaj dentro de rango, le ataca a melee SI ESTA A LA ALTURA CORRECTA, SINO, EL RANGO
        { 
            moveAnim = false;
            movement = false;

            if (Mathf.Abs(laimaRB.position.y - saddajRB.position.y) > 3f) 
            {
                //eqAnim = true; /*rangedAttack(); */
                if (availableEQ > 0) eqAnim = true;
                else if (availableEQ <= 0) eqAnim = false;
            }
            else { meleeAnim = true; eqAnim = false; availableEQ = maxEQ; }
        }
    }

    void rangedAttack()
    {
        eqAnim = true;
    }

    //Llamadas desde las animaciones//
    void endBlink()
    {
        blink = false;
        numGolpes += 1;

    }

    void earthquake() 
    {
        LaimaTerremotoSound.PlaySound();
        scShakeCam.shakeON = true;

        if (availableEQ > 0) 
        { 
            if(!scRocks.hayLuz)scRocks.caerRoca = true; 
            availableEQ--; 
        }
       // print("terremoto");
        //eqAnim = true;
        if (scMov.onGround()) 
        {
            print("Saddaj esta tocando el suelo");
            //Hacer daño a Saddaj 
            scBarraVida.recibirDaño(45f);
        }
        //
    }

    void stopShake() 
    {
        scShakeCam.shakeON = false;
    }

    void meleeAtack() 
    {
        LaimaMeleSound.PlaySound();
        float difPos = laimaRB.position.x - saddajRB.position.x;

        if (facingLeft)   //atacando por la izquierda
        {

            if (difPos >= 0 && difPos <= 8.5f)
            {
                //print("saddaj es apuñalado desde la dcha");
                //scBarraVida.vida = 0f;
                if (Mathf.Abs(laimaRB.position.y - saddajRB.position.y) < 2.5f)
                {
                    scBarraVida.recibirDaño(45f);                   
                }
               
            }
        }
        else        //atacando por la derecha
        {
            if (difPos <= 0 && difPos >= -8.5f)
            {
                //print("saddaj es apuñalado desde la izq");
                 //scBarraVida.vida = 0f;

                if (Mathf.Abs(laimaRB.position.y - saddajRB.position.y) < 2.5f)
                {
                    scBarraVida.recibirDaño(45f);
                }

            }
        }
    }

    void LaimaMove()
    {
        movement = true;
        audio.Play();
    }

    void LaimaStop() 
    {
        movement = false;
        audio.Stop();
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
    void nextStage() 
    {
        //aqui el código para pasar a la siguiente pantalla
        SceneManager.LoadScene("diapos6");
    }

    //Triggers
    private void OnTriggerStay2D(Collider2D obj)
    {
        if (obj.CompareTag("luzRef") && luzRefON)
        {
            print("Le diste a la arañita");
            /* print("Dañando a Laima");
             LaimaHP -= 5f;
             if ((LaimaHP) <= 0)
             {              
                 LaimaHP = 0;
                 laimaAlive = false;
             }*/
            blink = true;
        }
    }
}
