using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class movController : MonoBehaviour
{//jump tutorial
    public float jumpVel = 5f;
    public float cSpeed = 3f;
    [SerializeField] private LayerMask platformsLayerMask;
    private BarraDeVida scBarraVida;
    private PauseMenu scPause;
    private recogerObjeto scRecoger;

    public bool trig;

    public float desp = 0.3f;

    public bool control = true;
    //
    private Rigidbody2D rb2d;
    private float mH, mV;
    public float speed = 7;
    public float pushSpeed =4;
    
    private Transform obj;
    private bool facingRight = true;

    //public Vector2 jump = new Vector2(0, 20);

    private BoxCollider2D boxCollider2d;

    public float tLeft =5f;
    public bool activateTimer = false;

    //Anim controller
    public Animator anim;
    private bool moving, agachado, climbing, topClimb, empujaIdle, empujaMov, push, pull ;
    private bool platJump = true;
    //string cargar;

    AudioSource audioWalk;

    //bool isMoving; // Jordi
    //bool objectPick; // Jordi

// Start is called before the first frame update
    void Start()
    {
        /* Scene pantalla = SceneManager.GetActiveScene();
         cargar = pantalla.name;*/
        trig = true;
        obj = GetComponent<Transform>();
        rb2d = transform.GetComponent<Rigidbody2D>();
        boxCollider2d = transform.GetComponent<BoxCollider2D>();

        anim = GetComponent<Animator>();

        scBarraVida = FindObjectOfType<BarraDeVida>();
        scPause = FindObjectOfType<PauseMenu>();
        scRecoger = FindObjectOfType<recogerObjeto>();

        audioWalk = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (onGround()) { platJump = true; topClimb = false; }
        if(!scBarraVida.dead && !scPause.gamePaused && !scRecoger.recoger) Move();
        if (activateTimer) cooldown();
        animScript();
        //Debug.Log("yepa "+empujaIdle);
        
        //if(isMoving)
        if (moving || push || pull)
        {
            if (onGround() == true)
            {
                if (!audioWalk.isPlaying)
                    audioWalk.Play();
            }
            if (onGround() == false)
            {
                audioWalk.Stop();
            }
        }
        else
        {
            audioWalk.Stop();
        }

    }
    
    void Move() {       //movimiento de saddaj
        if (Input.GetKey(KeyCode.S) && climbing == false && topClimb == false)
        {
            float slow=0;

            if (onGround() && rb2d.velocity.x != 0)
            {
                //isMoving = true; // Jordi
                if (rb2d.velocity.x > 0.1) slow = -0.1f;// rb2d.velocity -= new Vector2(0.1f, 0);
                else if (rb2d.velocity.x < -0.1) slow = 0.1f;//rb2d.velocity += new Vector2(0.1f, 0);

                else { slow = 0; rb2d.velocity=new Vector2(slow, 0); }
                rb2d.velocity += new Vector2(slow, 0);
            }
            
            moving = false;
            //isMoving = flase; // Jordi
            boxCollider2d.size = new Vector2(6.5f, 6);
            boxCollider2d.offset = new Vector2(1.75f, -1.8f);
            agachado = true;
        }
        else
        {
            boxCollider2d.size = new Vector2(4, 9);
            boxCollider2d.offset = new Vector2(-0.25f, -0.3f);
            agachado = false;

            if (Input.GetKey(KeyCode.A) && climbing==false && topClimb==false)
            {
                if (empujaIdle) {
                    //empujaMov = true;
                    //isMoving = true; // Jordi
                    rb2d.velocity = new Vector2(-pushSpeed+0.35f, rb2d.velocity.y);
                }
                else {
                    //empujaMov = false; 
                    rb2d.velocity = new Vector2(-speed, rb2d.velocity.y);
                    moving = true;
                    //isMoving = true; // Jordi
                }
                
            }
            else if (Input.GetKey(KeyCode.D) && climbing == false && topClimb == false)
            {
                if (empujaIdle)
                {
                    //empujaMov = true;
                    rb2d.velocity = new Vector2(pushSpeed-0.35f, rb2d.velocity.y);
                }
                else
                {
                //empujaMov = false;
                    rb2d.velocity = new Vector2(speed, rb2d.velocity.y);
                    moving = true;
                }
                
            }

            else
            {
                rb2d.velocity = new Vector2(0, rb2d.velocity.y);
                moving = false;
                //empujaMov = false;
            }
            Jump();

        }

        if (!empujaIdle) flip();//Solo gira el sprite si no empuja ni tira

    }

    void flip() {
        if (!facingRight && rb2d.velocity.x > 0) { flipSprite(); }
        else if (facingRight && rb2d.velocity.x < 0) { flipSprite(); }
    }

    void Jump() {
        if (onGround() && Input.GetKeyDown(KeyCode.Space) && platJump && !empujaIdle)
        {
            if (!trig)
            {
                rb2d.velocity = Vector2.up * jumpVel;
                platJump = false;
            }
        }
    }
    public bool onGround()
    {
        RaycastHit2D raycastHit2d = Physics2D.BoxCast(boxCollider2d.bounds.center, boxCollider2d.bounds.size, 0f, Vector2.down, .1f, platformsLayerMask);        
        return raycastHit2d.collider != null;
    }


    void flipSprite(){   
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

    private void OnTriggerStay2D(Collider2D obj)
    {
        if (obj.CompareTag("empujable")) {
            trig = true;
        }
        float sadajCenter = boxCollider2d.transform.position.y;
        float sadajHigh = boxCollider2d.size.y/2;
        float platformTop =  + obj.bounds.size.y ;
        float trepYMax = obj.transform.position.y + obj.bounds.size.y / 1.95f;

        float pies = sadajCenter - sadajHigh;
        
        if (obj.CompareTag("trepable") && Input.GetKey(KeyCode.W) && control ) //&& !(sadajCenter > platformTop)
        {
            
            platJump = true;
            
            
            if (sadajCenter < platformTop ) { 
                rb2d.velocity = new Vector2(0f, cSpeed); 
                climbing = true; 
                topClimb = false; 
            }

                
            else if(sadajCenter >= trepYMax) {
                rb2d.gravityScale = 0f;
                rb2d.velocity = new Vector2(0f, 0f);
                boxCollider2d.transform.position = new Vector2(obj.transform.position.x, platformTop / 1.6f + sadajHigh / 2);
                topClimb = true;
            }
            
            else
            {
                climbing = false; 
                topClimb = true;
                
                rb2d.velocity = Vector2.up * 0;
                rb2d.gravityScale = 0f;
            }  

        }
                
        else climbing = false; //topClimb = false;

        if (pies > trepYMax) { platJump = false;  }

        if ((Input.GetKeyDown(KeyCode.Space) && platJump && onGround() && !empujaIdle && !trig) )//|| ((sadajCenter >= platformTop) && platJump)) //&& !(sadajCenter>trepYMax))
        {
            climbing = false;
            topClimb = false;

            rb2d.gravityScale = 1f;

            rb2d.velocity = Vector2.up * jumpVel;
            platJump = false;
            tLeft = 0.5f;
            activateTimer = true;
            control = false;
        }

    }

    //Empujar/tirar
    private void OnCollisionStay2D(Collision2D obj)
    {
        float sadajCenter = boxCollider2d.transform.position.y;
        float sadajHigh = boxCollider2d.size.y / 2;
        float sadajFoots = sadajCenter - sadajHigh+2;

        float platformMiddle = obj.collider.transform.position.y;
        float platformMidTop = obj.collider.bounds.size.y / 2;
        float platformTop = platformMiddle + platformMidTop-2;

        if (platformTop > sadajFoots)
        {
            if (obj.collider.CompareTag("empujable"))
            {
                chooseSide(obj);    //Desde donde estas interactuando con el objeto
                if (Input.GetKeyDown(KeyCode.F) && !empujaIdle)
                {
                    empujaIdle = true;
                }
                else if (Input.GetKeyDown(KeyCode.F) && empujaIdle)
                {
                    empujaIdle = false;
                }

                //Empujar//
                if (Input.GetKey(KeyCode.D) && empujaIdle)
                {
                    empujaMov = true;

                    obj.rigidbody.velocity = new Vector2(pushSpeed, obj.rigidbody.velocity.y);
                    //push = true;
                }
                //Tirar//
                else if (Input.GetKey(KeyCode.A) && empujaIdle)
                {
                    empujaMov = true;

                    obj.rigidbody.velocity = new Vector2(-pushSpeed, obj.rigidbody.velocity.y);
                    //push = true;
                }
                //Ninguna de las dos
                else { obj.rigidbody.velocity = new Vector2(0f, obj.rigidbody.velocity.y); empujaMov = false; }
            }
            else //Frenar objeto en X al separarte de el
            {
                obj.rigidbody.velocity = new Vector2(0f, obj.rigidbody.velocity.y);// obj.rigidbody.inertia = 0f;
            }
        }

        

    }


    private void OnTriggerExit2D(Collider2D obj)
    {
        trig = false;
        if (obj.CompareTag("empujable"))
        {
            empujaIdle = false;
            empujaMov = false;
            push = false;
            pull = false;
            obj.attachedRigidbody.velocity = new Vector2(0f, obj.attachedRigidbody.velocity.y);
        }
        if (obj.CompareTag("trepable"))
        {
            //climbing = false;
            Debug.Log("Paz y Amor");
        }
    }


    //Muerte fuera del mapa
    private void OnCollisionEnter2D(Collision2D outMap)
    {

        if (outMap.collider.CompareTag("fueraMapa"))
        {
            scBarraVida.dead = true;
            tLeft = 3f;
            scBarraVida.setHP(0f);
            scBarraVida.recibirDaño(0f);
            activateTimer = true;
        }
    }



   /*private void OnCollisionStay2D(Collision2D obj)
            {
                if (obj.collider.CompareTag("empujable") && Input.GetKey(KeyCode.F))//
                {

                    empujaIdle = true;

                    chooseSide(obj);

                    if (Input.GetKey(KeyCode.D)) { 
                        obj.rigidbody.velocity = new Vector2(speed / 2, obj.rigidbody.velocity.y);

                    }
                    else if (Input.GetKey(KeyCode.A)) {
                        obj.rigidbody.velocity = new Vector2(-speed / 2, obj.rigidbody.velocity.y); 
                    }
                    else {
                        obj.rigidbody.inertia = 0f;
                        float lx = obj.rigidbody.position.x;
                        obj.rigidbody.position=new Vector2(lx, obj.rigidbody.position.y);

                        obj.rigidbody.velocity = new Vector2(0f, obj.rigidbody.velocity.y);
                    }
                }

                else //if (Input.GetKeyUp(KeyCode.F))
                {
                    empujaMov = false;
                    obj.rigidbody.velocity = new Vector2(0f, obj.rigidbody.velocity.y); 
                    float lx = obj.rigidbody.position.x;
                    obj.rigidbody.position = new Vector2(lx, obj.rigidbody.position.y);
                    empujaIdle = false;
                }
    }

    private void OnCollisionExit2D(Collision2D obj) {
        if (obj.collider.CompareTag("empujable") )//
        {
            empujaMov = false;
            obj.rigidbody.velocity = new Vector2(0f, obj.rigidbody.velocity.y);
            float lx = obj.rigidbody.position.x;
            obj.rigidbody.position = new Vector2(lx, obj.rigidbody.position.y);
            empujaIdle = false;
        }
    }*/
    
 //Ver desde que lado se empuja/tira
   void chooseSide(Collision2D p) {

        float sadajHigh = boxCollider2d.size.y / 2;
        float sadajCenterY = boxCollider2d.transform.position.y;
        float pies = sadajCenterY - sadajHigh;
        float pYMax = p.transform.position.y + p.collider.bounds.size.y / 2.2f;

        float sadajCenterX = boxCollider2d.transform.position.x;

        if ((sadajCenterX > p.collider.transform.position.x))//&& p.collider.CompareTag("empujable")
        {
            if (Input.GetKey(KeyCode.D)) { pull = true; push = false;  }

            else if (Input.GetKey(KeyCode.A)) { pull = false; push = true;  }

            else { pull = false; push = false; }

        }
        else if ((sadajCenterX < p.collider.transform.position.x))// && p.collider.CompareTag("empujable")
        {
            if (Input.GetKey(KeyCode.D)) { pull = false; push = true;  }

            else if (Input.GetKey(KeyCode.A)) { pull = true; push = false;  }

            else { pull = false; push = false; }

        }
        else { pull = false; push = false;  }
    
    }



    /*void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D col = collision.collider;
        float pScaleY = col.transform.localScale.y / 1.99f;
        float scaleSadajY = (transform.localScale.y) - 0.1f;

        float res = pScaleY + scaleSadajY;

        if (col.CompareTag ("wall"))
        {
            Vector3 contactPoint = collision.contacts[0].point;
            Vector3 center = col.bounds.center;

            bool left = contactPoint.x < center.x;
            bool right = contactPoint.x > center.x;
            bool top = contactPoint.y > (center.y);
            bool bottom = contactPoint.y < (center.y);
            Debug.Log(center.y);
            if (top)
            {
                if (left)
                {
                    control = false;

                    Vector3 pos = transform.localPosition;
                    float pX = pos.x - desp;
                    pos.x -= desp;
                    transform.localPosition = pos;

                    if (pX == transform.localPosition.x)
                    {activateTimer = true;}
                }
                else if(right){
                    control = false;

                    Vector3 pos = transform.localPosition;
                    float pX = pos.x + desp;
                    pos.x += desp;
                    transform.localPosition = pos;

                    if (pX == transform.localPosition.x)
                    {activateTimer = true; }

                }
            }
            else if(bottom)
            {
                if (left)
                {
                    control = false;

                    Vector3 pos = transform.localPosition;
                    float pX = pos.x - desp;
                    pos.x -= desp;
                    transform.localPosition = pos;

                    if (pX == transform.localPosition.x)
                    { activateTimer = true; }
                }
                else if (right)
                {
                    control = false;

                    Vector3 pos = transform.localPosition;
                    float pX = pos.x + desp;
                    pos.x += desp;
                    transform.localPosition = pos;

                    if (pX == transform.localPosition.x)
                    { activateTimer = true; }

                }
            }
        }
    }
    */
    void cooldown() {
  
        tLeft -= Time.deltaTime;
        if (tLeft <= 0) {
            activateTimer = false; 
            tLeft =3f; 
            control = true;
            //if(scBarraVida.dead)SceneManager.LoadScene("level_1");
        }

    }

    

    void animScript() { 
        //math.abs return a '+¡ number
        //anim.SetFloat("Speed", Mathf.Abs(mH)/*Mathf.Abs(rb2d.velocity.x)*/); 
        anim.SetBool("Moving", moving);
        anim.SetBool("Grounded", onGround());
        anim.SetBool("Agachado", agachado); 
        anim.SetBool("Climbing", climbing); 
        anim.SetBool("topClimb", topClimb); 

        anim.SetBool("EmpujaIdle", empujaIdle); 
        anim.SetBool("EmpujaMov", empujaMov); 
        anim.SetBool("Push", push); 
        anim.SetBool("Pull", pull); 

        anim.SetBool("Muerte", scBarraVida.dead);

        //anim.SetBool("RecojeObjeto", objectPick);
    }
}
