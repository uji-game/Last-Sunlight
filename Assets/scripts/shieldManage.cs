using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shieldManage : MonoBehaviour
{
    private movController scMController;

    private BoxCollider2D shieldCollider2d;
    private Rigidbody2D shieldRB;

    private Rigidbody2D saddajRB;

    public GameObject Shield;
    private SpriteRenderer shieldRender;

    public GameObject rLight;
    private Rigidbody2D rLightRB;
    private SpriteRenderer rLightRender;



    public bool shieldUP;
    public bool canUse, actUsingTimer, actCdShield;

    private float tRem;

    // Start is called before the first frame update

    void Start()
    {
        scMController = FindObjectOfType<movController>();

        saddajRB = transform.GetComponent<Rigidbody2D>();
        shieldRender = Shield.GetComponent<SpriteRenderer>();


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
        Debug.Log(shieldUP);

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

    private void OnTriggerStay2D(Collider2D lux)
    {
        if (lux.CompareTag("luz") && scMController.onGround() && shieldUP)
        {
            rLightRender.enabled = true;

        }
        else rLightRender.enabled = false;
    }

    void usingShieldTime()
    {
        Debug.Log("Usando");

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
