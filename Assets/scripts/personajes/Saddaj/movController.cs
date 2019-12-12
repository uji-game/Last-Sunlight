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
    private shieldManage scShieldM;
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

    //Sounds
    private bool falling, jumping = false;

    new AudioSource audio;
    public AudioClip audioWalk;
    public AudioClip audioPushPull;
    //public AudioClip audioFall;

// Start is called before the first frame update
    void Start()
    {
        trig = false;
        obj = GetComponent<Transform>();
        rb2d = transform.GetComponent<Rigidbody2D>();
        boxCollider2d = transform.GetComponent<BoxCollider2D>();

        anim = GetComponent<Animator>();

        scBarraVida = FindObjectOfType<BarraDeVida>();
        scPause = FindObjectOfType<PauseMenu>();
        scRecoger = FindObjectOfType<recogerObjeto>();
        scShieldM = FindObjectOfType<shieldManage>();


        audio = GetComponent<AudioSource>();
        //audioJump = GetComponent<AudioSource>();
        //audioFall = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (onGround()) { platJump = true; topClimb = false; }
        else { falling = true; platJump = false; }//Uno de los cambios audiosC

        if(!scBarraVida.dead && !scPause.gamePaused && !scRecoger.recoger) Move();
        if (scBarraVida.dead) 
        { 
            rb2d.velocity = new Vector2(0, rb2d.velocity.y);
            //if (!onGround()) { rb2d.position += new Vector2(0, -0.2f); }
        }

        if (activateTimer) cooldown();
        animScript();
        //Debug.Log("yepa "+empujaIdle);
        //Todo esto es para audiosC


        if (falling && platJump)
        {
            falling = false;
            SonidoCaida.PlaySound();
        }

        if (moving || push || pull)
        {
            if (onGround() == true)
            {
                if (!audio.isPlaying)
                    audio.PlayOneShot(audioWalk);
            }

            else //if (onGround() == false && falling && !audio.isPlaying)
            {
                audio.Stop();
                Debug.Log("cayendo");
            }


            if (push || pull)
            {
                SonidoArrastrar.PlaySound();
            }
        }
        else //if(falling)
        {
            audio.Stop();
            Debug.Log("falling");
        }

        if (jumping)
        {
            audio.Stop();
            //audio.PlayOneShot(audioJump);
            SonidoSalto.PlaySound();
            jumping = false;
        }


        //Debug.Log("trig: "+trig);
    }
    
    void Move() {       //movimiento de saddaj
        if (Input.GetKey(KeyCode.S) && climbing == false && topClimb == false)
        {
            float slow=0;

            if (onGround() && rb2d.velocity.x != 0)
            {
                if (rb2d.velocity.x > 0.1) slow = -0.1f;// rb2d.velocity -= new Vector2(0.1f, 0);
                else if (rb2d.velocity.x < -0.1) slow = 0.1f;//rb2d.velocity += new Vector2(0.1f, 0);

                else { slow = 0; rb2d.velocity=new Vector2(slow, 0); }
                rb2d.velocity += new Vector2(slow, 0);
            }
            
            moving = false;
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
                    rb2d.velocity = new Vector2(-pushSpeed+0.35f, rb2d.velocity.y);
                }
                else {
                    //empujaMov = false; 
                    rb2d.velocity = new Vector2(-speed, rb2d.velocity.y);
                    moving = true;
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
            if (!scShieldM.shieldUP) { Jump();  }

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
                
                //Modificación para audiosC
                audio.Stop();
                jumping = true;
                
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

    private void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.CompareTag("blast")  ) 
        {
            if (!scShieldM.shieldUP) 
            {
                scBarraVida.vida = 0f;
                scBarraVida.recibirDaño(0f);
                if (scBarraVida.vida <= 0f) { scBarraVida.dead = true; }
                print("pupita"); 
            }
        }
    }


    private void OnTriggerStay2D(Collider2D obj)
    {
        //Trepar//
        float sadajCenter = boxCollider2d.transform.position.y;
        float sadajHigh = boxCollider2d.size.y / 2;
        float platformTop = obj.bounds.size.y;
        float trepYMax = obj.transform.position.y + obj.bounds.size.y / 2f;

        float pies = sadajCenter - sadajHigh;

        if (obj.CompareTag("trepable") && Input.GetKey(KeyCode.W) && control ) //&& !(sadajCenter > platformTop)
        {
            
            

            //Debug.Log("sadajjCenter: "+sadajCenter+"// trepY: "+trepYMax);
            Debug.Log("platformTop: "+ platformTop+ "\ntrepY: " + trepYMax + "\ntrepY/1.9: " + (trepYMax/1.05f));

            trig = false;
            
            platJump = true;
            
            
            if (sadajCenter < trepYMax / 1.05f) { 
                rb2d.velocity = new Vector2(0f, cSpeed); 
                climbing = true; 
                topClimb = false; 
            }

                
            else if(sadajCenter >= trepYMax) {
                rb2d.gravityScale = 0f;
                rb2d.velocity = new Vector2(0f, 0f);
                boxCollider2d.transform.position = new Vector2(obj.transform.position.x, trepYMax);//  + sadajHigh / 2f
                topClimb = true;
            }
            
            else
            {
                climbing = false; 
                topClimb = true;
                
                rb2d.velocity = Vector2.up * 0;
                rb2d.gravityScale = 0f;
            }


            if (pies > trepYMax) { platJump = false; }

        }                
        else climbing = false; //topClimb = false;
        if (pies > trepYMax) { platJump = false; }


        if ((Input.GetKeyDown(KeyCode.Space) && platJump && /*onGround() &&*/ !empujaIdle && !trig && !scShieldM.shieldUP) )//|| ((sadajCenter >= platformTop) && platJump)) //&& !(sadajCenter>trepYMax))
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

        if (obj.CompareTag("empujable"))
        {
            trig = true;
        }

    }
    private void OnTriggerExit2D(Collider2D obj)
    {

        if (obj.CompareTag("empujable"))
        {
            empujaIdle = false;
            empujaMov = false;
            push = false;
            pull = false;
            obj.attachedRigidbody.velocity = new Vector2(0f, obj.attachedRigidbody.velocity.y);
            trig = false;

        }
        if (obj.CompareTag("trepable"))
        {
            trig = false;

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
