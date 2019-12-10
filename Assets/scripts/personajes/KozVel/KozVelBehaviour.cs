﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KozVelBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator kvAnim;

    private Rigidbody2D kvRB;
    public Rigidbody2D saddajRB;
    //blast
    public GameObject blast;
    private Rigidbody2D blastRB;

    private SpriteRenderer blastSR;

    //
    private float kvIni, kvMax, kvMin, kvPosX;
    private bool guard, patrolD, kvAlive;
    private Vector3 kvPos;

    //anim
    private bool charge, shoot;


    void Start()
    {
        charge = false;
        kvAlive = true;
        guard = true;
        kvAnim = GetComponent<Animator>();
        kvAnim.applyRootMotion = true;
        kvRB = GetComponent<Rigidbody2D>();

        kvIni = Mathf.Floor(kvRB.transform.position.x);        

        kvRB.transform.position = new Vector2(kvIni, kvRB.transform.position.y);

        kvMax = kvIni + 5f;
        kvMin = kvIni - 5f;

        //Blast
        blastSR = blast.GetComponent<SpriteRenderer>();
        blastRB = blast.GetComponent<Rigidbody2D>();
        blastSR.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        kvPos.x = Mathf.Floor(kvRB.transform.position.x);
        kvPos.y = Mathf.Floor(kvRB.transform.position.y);


        kvPosX = Mathf.Floor(kvRB.transform.position.x);
        movimiento();
        if (kvAlive && Mathf.Abs(kvRB.position.x - saddajRB.position.x) < 10f)//dentro de rango de disparo?
        {
            charge = true; //inKVRange();
            guard = false;

        }
        else
        {
            guard = true;
            charge = false;
        }

        kvAnim.SetBool("charging", charge);
        kvAnim.SetBool("shooting", shoot);

        if (blastSR.enabled && shoot)
        {
            blastAim(); 
        }


        //if (Input.GetKey(KeyCode.L)) { blastSR.enabled = true; blast.transform.position += new Vector3(1f, 0, 0); }//basuro
    }

    public void movimiento()
    {
        if (guard)
        {
            //bAttack = false;
            

            if ((kvPosX <= kvMax) && !patrolD)   //patrulla hacia la derecha
            {
                kvRB.transform.position += new Vector3(0.025f, 0f, 0f);

                if (kvPosX == kvMax)
                {

                    patrolD = true;
                }
            }


            else if ((kvPosX >= kvMin) && patrolD)   //patrulla hacia la izquierda
            {         
                kvRB.transform.position += new Vector3(-0.025f, 0f, 0f);

                if (kvPosX == kvMin)
                {
                    patrolD = false;
                }
            }
        }
        else
        {
            if ((kvPosX >= kvMin) && !patrolD && ((Mathf.Abs(saddajRB.position.x - kvRB.position.x) > 3f))) { patrolD = true; }
            else if ((kvPosX <= kvMax) && patrolD && ((Mathf.Abs(saddajRB.position.x - kvRB.position.x) > 3f))) { patrolD = false;  }

        }
    }
    private void inKVRange()
    {
        /*if ((kvRB.position.x - saddajRB.position.x) < -1.5f)
        {
            
            if (patrolD) { patrolD = false;; }
            print("Te pillo por la izq");
        }


        else if ((kvRB.position.x - saddajRB.position.x) > 1.5f)
        {
            
            if (!patrolD) { patrolD = true; }

            print("Te pillo por la dcha");

        }

       /* else
        {
            if (!scBarraVida.dead) bAttack = true;
            else bAttack = false;   //no te cebes hdp
        }*/

        charge = true;


    }
    Vector2 sadPos;
    void vkShoot() //antiguo pene
    {
        //print("pum");
        blast.transform.position = kvPos;
        blastSR.enabled = true;
        sadPos = saddajRB.position;
        /*Vector3 xd = new Vector3(0f,0f,1f);
        Vector2 xd2 = new Vector3(-1f,0f);

        float x = Mathf.Abs(saddajRB.position.x - blastRB.position.x);
        float y = Mathf.Abs(saddajRB.position.y - blastRB.position.y);

        float h = Mathf.Sqrt((x * x) + (y * y));
        float angle = Mathf.Asin(x / h);

        Vector2 dir = (Vector2)(Quaternion.Euler(0, 0, angle) * Vector2.right);
        
        blastRB.velocity = dir * 10f;

        // blastRB.velocity = blastRB.transform.right* 5f;
        ////////////////////////////////////////////////////////////////////////////////////////////////
        //GameObject newBlast = Instantiate(blast, blast.transform.position, Quaternion.identity);
        //newBlast.GetComponent<Rigidbody2D>().velocity = newBlast.transform.right * 10f;
        //blast.transform.LookAt(saddajRB.transform, xd);
        //blast.transform.rotation = Quaternion.FromToRotation(blast.transform.position,xd2 );




        /*float step = 10f * Time.deltaTime;
        blast.transform.position = Vector3.MoveTowards(blast.transform.position, sadPos, step);
        //blastRB.MovePosition(blastRB.position + new Vector2(1.75f, 1.1f) * Time.fixedDeltaTime);
        //
        Vector2 a = new Vector2(1f, 1f);
        Vector2 angleBetween = Vector2.SmoothDamp(blastRB.position, sadPos, ref a , 1f);//       Angle(blastRB.position, sadPos);*/

        //float angleBetween = Vector2.SignedAngle(blastRB.position, sadPos);

        //Debug.Log(Vector2.SignedAngle(blastRB.position, sadPos));



        /*Vector3 otro = (blastRB.position - saddajRB.position).normalized;
        float a = Mathf.Atan2(otro.y, otro.x) * Mathf.Rad2Deg + 180;

        Debug.DrawLine(blastRB.position, saddajRB.position, Color.red);*/

        //blastRB.rotation = 150f;

        /*float x = Mathf.Abs(saddajRB.position.x - blastRB.position.x);
        float y = Mathf.Abs(saddajRB.position.y - blastRB.position.y);

        float h = Mathf.Sqrt((x * x) + (y * y));

        float angle = Mathf.Asin(x / h);
        Quaternion rot = Quaternion.AngleAxis(angle, Vector3.forward);
        blast.transform.rotation = Quaternion.RotateTowards(blast.transform.rotation, rot, Time.deltaTime * 5);*/

        shoot = true;
    }
    void vkReload() //antiguo pene2
    {
        shoot = false;
        blastSR.enabled = false;

        blastSR.color = new Color(1f, 1f, 1f, 1f);

    }

    void blastAim() 
    {
        //blastRB.velocity = new Vector2(-1, 0);

        Vector3 dir = blastRB.position - sadPos;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        Quaternion rot = Quaternion.AngleAxis(angle, Vector3.forward);

        blast.transform.rotation = Quaternion.RotateTowards(blast.transform.rotation, rot,  1);

        blastRB.transform.position = Vector2.MoveTowards(blastRB.transform.position,sadPos, 0.2f);
        blastSR.color = new Color(1f,1f,1f, blastSR.color.a-0.025f);
        
    }
}