using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shieldManage : MonoBehaviour
{
    private movController scMController;

    private Rigidbody2D shieldRB;

    private Rigidbody2D saddajRB;

    public GameObject Shield;
    private SpriteRenderer shieldRender;
    private BoxCollider2D shieldCollider2d;


    public GameObject rLight;
    private Rigidbody2D rLightRB;
    private SpriteRenderer rLightRender;

    private Vector3 luxPosIni;

    public bool shieldUP;
    public bool canUse, actUsingTimer, actCdShield;

    private float tRem;

    // Start is called before the first frame update

    void Start()
    {
        shieldUP = false;
        scMController = FindObjectOfType<movController>();

        saddajRB = transform.GetComponent<Rigidbody2D>();
        shieldRender = Shield.GetComponent<SpriteRenderer>();
        shieldCollider2d = Shield.GetComponent<BoxCollider2D>();


        rLightRB = rLight.GetComponent<Rigidbody2D>();
        rLightRender = rLight.GetComponent<SpriteRenderer>();


        rLightRB.transform.position = new Vector2(Shield.transform.position.x + 3.5f, Shield.transform.position.y);

        rLightRender.enabled = false;

        canUse = true ; actUsingTimer = false; actCdShield = false; tRem = 2f;

    }

    // Update is called once per frame
    void Update()
    {
        useShield();
        followSaddaj();
        //Debug.Log(shieldUP);

        if (actUsingTimer) { usingShieldTime(); }
        if(actCdShield) {shieldCD();}


    }

     void followSaddaj() {

        Shield.transform.position = new Vector2(saddajRB.position.x - 0.2f, saddajRB.position.y - 0.45f);

        if (Input.GetKey(KeyCode.A))
        {
            shieldRender.sortingOrder = -1;
            Shield.transform.rotation = Quaternion.Euler(0f, 0f, 30f);
            rLightRB.transform.position = new Vector2(Shield.transform.position.x - 3.5f, Shield.transform.position.y);

        }

        else if (Input.GetKey(KeyCode.D))
        {
            shieldRender.sortingOrder = 10;
            Shield.transform.rotation = Quaternion.Euler(0f, 0f, -30f);
            rLightRB.transform.position = new Vector2(Shield.transform.position.x + 3.5f, Shield.transform.position.y);
        }


     }

    void useShield()
    {
        if (Input.GetKey(KeyCode.E) && scMController.onGround() && canUse)
        {

            shieldUP = true;
            actUsingTimer = true;

        }
        else 
        { 
            shieldUP = false;
            
        }

    }
    private void OnTriggerEnter2D(Collider2D lux)
    {
        if (lux.CompareTag("luz")) { luxPosIni = lux.transform.position; }
    }

    private void OnTriggerStay2D(Collider2D lux)
    {
        Debug.Log(lux.transform.position.y);
        if (lux.CompareTag("luz") && scMController.onGround() && shieldUP)
        {
            rLightRender.enabled = true;


            float shieldCenter = shieldCollider2d.transform.position.y;
            float luxMidTopY = lux.bounds.size.y / 2;
            float shieldTopY = luxMidTopY + shieldCenter;

            Debug.Log(lux.transform.position.y);

            lux.transform.position = new Vector2(lux.transform.position.x, shieldTopY);


        }
        else if (lux.CompareTag("luz") && scMController.onGround() && !shieldUP) { lux.transform.position = luxPosIni; }

        else rLightRender.enabled = false;
    }

    private void OnTriggerExit2D(Collider2D lux)
    {
        if (lux.CompareTag("luz") ) lux.transform.position = luxPosIni;
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
}
