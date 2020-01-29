using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class shieldManage : MonoBehaviour
{
    private movController scMController;
    private BarraDeVida scBarraVida;
    private PauseMenu scPause;
    private KozVelBehaviour scKV;

    private Rigidbody2D shieldRB;
    

    private Rigidbody2D saddajRB;

    public GameObject Shield;
    public Rigidbody2D ShieldRB;
    private SpriteRenderer shieldRender;
    private BoxCollider2D shieldCollider2d;


    public GameObject rLight;
    private Rigidbody2D rLightRB;
    private SpriteRenderer rLightRender;

    public GameObject counterBlast;
    private SpriteRenderer cBlastSR;
    private Rigidbody2D cBlastRB;



    private Vector3 luxPosIni;

    public bool shieldUP;
    public bool canUse, actUsingTimer, actCdShield, touchingLux, touchingBlast;

    private float tRem;

    Scene currentScene;

    // Start is called before the first frame update

    void Start()
    {
        //currentScene = SceneManager.GetActiveScene();

        touchingLux = false;
        touchingBlast = false;
        shieldUP = false;

        scMController = FindObjectOfType<movController>();
        scBarraVida = FindObjectOfType<BarraDeVida>();
        scPause = FindObjectOfType<PauseMenu>();
        scKV = FindObjectOfType<KozVelBehaviour>();

        saddajRB = transform.GetComponent<Rigidbody2D>();
        ShieldRB = Shield.transform.GetComponent<Rigidbody2D>();
        shieldRender = Shield.GetComponent<SpriteRenderer>();
        shieldCollider2d = Shield.GetComponent<BoxCollider2D>();


        rLightRB = rLight.GetComponent<Rigidbody2D>();
        rLightRender = rLight.GetComponent<SpriteRenderer>();


        /*if (SceneManager.GetActiveScene().name != "Nivel 3")
        {*/
            cBlastRB = counterBlast.transform.GetComponent<Rigidbody2D>();
            cBlastSR = counterBlast.GetComponent<SpriteRenderer>();
        //}


        rLightRB.transform.position = new Vector2(Shield.transform.position.x + 5.5f, Shield.transform.position.y);

        rLightRender.enabled = false;

        canUse = true ; actUsingTimer = false; actCdShield = false; tRem = 2f;

    }

    // Update is called once per frame
    void Update()
    {
        //print(scMController.onGround());
        //print(shieldUP);
        if (scBarraVida.dead) 
        {
            ShieldRB.bodyType = RigidbodyType2D.Dynamic;
        }

        currentScene = SceneManager.GetActiveScene();
        if(shieldRender.enabled) useShield();
        if (!scBarraVida.dead) { followSaddaj(); }
        //Debug.Log(shieldUP);

        if (actUsingTimer) { usingShieldTime(); }
        if(actCdShield) {shieldCD();}

        if (!cBlastSR.enabled) { cBlastRB.transform.position = Shield.transform.position; }
        else 
        {
            if (cBlastSR.enabled && comprueba())
            { 
                //print("holo"); 
                cBlastSR.enabled = false; 
                scKV.kvActive = false; 
            }
            else if (cBlastSR.enabled && !comprueba()) 
            { 
                counterBlast.transform.eulerAngles += Vector3.forward * 50f;
            }


        }

        if (shieldRender.enabled && !canUse) { print("onCD"); shieldRender.color = Color.red; }
        else { shieldRender.color = new Color(255, 255, 255, 255); }

    }

    bool comprueba() 
    {
        float difX = Mathf.Abs(cBlastRB.position.x - scKV.kvPos.x);
        float difY = Mathf.Abs(cBlastRB.position.y - scKV.kvPos.y);

        if (difX < 1f && difY < 1f) return true;
        else return false;

    }

     void followSaddaj() {

        Shield.transform.position = new Vector2(saddajRB.position.x - 0.2f, saddajRB.position.y - 0.45f);

        if (Input.GetKey(KeyCode.A) && !scBarraVida.dead && !scPause.gamePaused)
        {
            shieldRender.sortingOrder = -1;
            Shield.transform.rotation = Quaternion.Euler(0f, 0f, 30f);

            rLightRB.transform.rotation = Quaternion.Euler(0f, 0f, -90f);
            rLightRB.transform.position = new Vector2(Shield.transform.position.x - 6f, Shield.transform.position.y);

        }

        else if (Input.GetKey(KeyCode.D) && !scBarraVida.dead && !scPause.gamePaused)
        {
            shieldRender.sortingOrder = 10;
            Shield.transform.rotation = Quaternion.Euler(0f, 0f, -30f);

            rLightRB.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
            rLightRB.transform.position = new Vector2(Shield.transform.position.x + 6f, Shield.transform.position.y);
        }


     }

    void useShield()
    {
        if (Input.GetKey(KeyCode.E) && scMController.onGround() && canUse /*&& (touchingLux || touchingBlast)*/)
        {

            shieldUP = true;
            actUsingTimer = true;

        }
        else 
        { 
            shieldUP = false;
            
        }

    }
    private void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.CompareTag("luz")) { luxPosIni = obj.transform.position; touchingLux = true; }

        if (obj.CompareTag("blast"))
        {
            touchingBlast = true;
            if (shieldUP)
            {
                scKV.blastSR.enabled = false;
                cBlastSR.enabled = true;
                //cBlastRB.MovePosition(scKV.kvPos);
                //cBlastRB.position += (Vector2)scKV.kvPos;

                Vector3 dir = cBlastRB.position - (Vector2)scKV.kvPos;
                Vector3 x = dir * 2;
                if (cBlastRB.transform.position != x) cBlastRB.velocity = -(dir);
                //scKV.kvActive = false;

                //if (cBlastRB.transform.position == scKV.kvPos) cBlastSR.enabled = false; ;

            }
            else
            {
                print("la paraste de pecho colorao");
            }

        }
    }

    private void OnTriggerStay2D(Collider2D lux)
    {
        if (currentScene.name == "Nivel 2" || currentScene.name == "Nivel 3")
        {
            if (lux.CompareTag("luz") && scMController.onGround() && shieldUP)
            {
                rLightRender.enabled = true;


                float shieldCenter = shieldCollider2d.transform.position.y;
                float luxMidTopY = lux.bounds.size.y / 2;
                float shieldTopY = luxMidTopY + shieldCenter;
                

                //Debug.Log(lux.transform.position.y);

                lux.transform.position = new Vector2(lux.transform.position.x, shieldTopY);


            }
            else if (lux.CompareTag("luz") && scMController.onGround() && !shieldUP) { lux.transform.position =  new Vector3(lux.attachedRigidbody.transform.position.x, 15, lux.attachedRigidbody.transform.position.z); }

            else rLightRender.enabled = false;
        }
    }

    private void OnTriggerExit2D(Collider2D lux)
    {
        if (lux.CompareTag("luz") && currentScene.name == "Nivel 2") { lux.transform.position = new Vector3(lux.attachedRigidbody.transform.position.x, 15, lux.attachedRigidbody.transform.position.z);  touchingLux = false; }
        if (lux.CompareTag("blast")) { touchingLux = false; }
    }

    void usingShieldTime()
    {
        //ebug.Log("Usando");

        tRem -= Time.deltaTime;
        if (tRem <= 0)
        {

            canUse = false;
            shieldUP = false;
            actUsingTimer = false;
            tRem = 2f;

            actCdShield = true;
        }
    }
    void shieldCD()
    {
        tRem -= Time.deltaTime;
        if (tRem <= 0)
        {
            actCdShield = false;
            canUse = true;
            tRem = 2f;
        }
    }

    void mismuertos() 
    {                
        if (currentScene.name == "Nivel 2") { shieldRender.enabled = true; print("xd"); }
    }
}
