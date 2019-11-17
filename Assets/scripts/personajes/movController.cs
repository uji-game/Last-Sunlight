using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movController : MonoBehaviour
{//jump tutorial
    public float jumpVel = 5f;
    public float cSpeed = 3f;
    [SerializeField] private LayerMask platformsLayerMask;
   // private GameObject xd;

    public float desp = 0.3f;

    public bool control = true;
    //
    private Rigidbody2D rb2d;
    private float mH, mV;
    public float speed = 7;
    
    private Transform obj;
    private bool facingRight = true;

    //public Vector2 jump = new Vector2(0, 20);

    private BoxCollider2D boxCollider2d;

    public float tLeft = 0.25f;
    public bool activateTimer = false;

    //Anim controller
    public Animator anim;
    private bool moving, agachado, climbing, topClimb, empujaIdle, empujaMov, push, pull ;
    private bool platJump = true;

    public bool canMove;

    
// Start is called before the first frame update
    void Start()
    {
        
        obj = GetComponent<Transform>();
        rb2d = transform.GetComponent<Rigidbody2D>();
        boxCollider2d = transform.GetComponent<BoxCollider2D>();

        anim = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!canMove)
        {
            return;
        }
        if (onGround()) { platJump = true; topClimb = false; }
        Move();
        if (activateTimer) cooldown();
        animScript();
        //Debug.Log("yepa "+empujaIdle); 

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
                    empujaMov = true;
                    rb2d.velocity = new Vector2(-speed / 2.45f, rb2d.velocity.y);
                }
                else { 
                    empujaMov = false; 
                    rb2d.velocity = new Vector2(-speed, rb2d.velocity.y);
                    moving = true;
                }
                
            }
            else if (Input.GetKey(KeyCode.D) && climbing == false && topClimb == false)
            {
                if (empujaIdle)
                {
                    empujaMov = true;
                    rb2d.velocity = new Vector2(speed/2.35f, rb2d.velocity.y);
                }
                else
                {
                    empujaMov = false;
                    rb2d.velocity = new Vector2(speed, rb2d.velocity.y);
                    moving = true;
                }
                
            }

            else
            {
                rb2d.velocity = new Vector2(0, rb2d.velocity.y);
                moving = false;
                empujaMov = false;
            }
            Jump();

        }

        if(!pull && !push) flip();

    }

    void flip() {
        if (!facingRight && rb2d.velocity.x > 0) { flipSprite(); }
        else if (facingRight && rb2d.velocity.x < 0) { flipSprite(); }
    }

    void Jump() {
        if (onGround() &&  Input.GetKeyDown(KeyCode.Space) && platJump && !empujaIdle && !empujaMov)
        {
            rb2d.velocity = Vector2.up * jumpVel;
            platJump = false;
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

    private void OnTriggerStay2D(Collider2D trep)
    {
        float sadajCenter = boxCollider2d.transform.position.y;
        float sadajHigh = boxCollider2d.size.y/2;
        float platformTop =  + trep.bounds.size.y ;
        float trepYMax = trep.transform.position.y + trep.bounds.size.y / 1.95f;

        float pies = sadajCenter - sadajHigh;

        //hay un bug al llegar arriba de la plataforma, siguiente sprint para solucionarlo

        if (trep.CompareTag("trepable") && Input.GetKey(KeyCode.W) && control ) //&& !(sadajCenter > platformTop)
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
                boxCollider2d.transform.position = new Vector2(trep.transform.position.x, platformTop / 1.6f + sadajHigh / 2);
                topClimb = true;
            }
            
            else
            {
                topClimb = true;
                climbing = false;
                rb2d.velocity = Vector2.up * 0;
                rb2d.gravityScale = 0f;
            }

            

        }
                
        else climbing = false; //topClimb = false;

        if (pies > trepYMax) { platJump = false;  }

        if ((Input.GetKeyDown(KeyCode.Space) && platJump ) )//|| ((sadajCenter >= platformTop) && platJump)) //&& !(sadajCenter>trepYMax))
        {
            climbing = false;
            topClimb = false;

            rb2d.gravityScale = 1f;

            rb2d.velocity = Vector2.up * jumpVel;
            platJump = false;
            activateTimer = true;
            control = false;
        }
    }

    //Empujar/tirar
   /* private void OnCollisionStay2D(Collision2D obj)
    {
        if (obj.collider.CompareTag("empujable") && Input.GetKey(KeyCode.F))//
        {
            
            empujaIdle = true;
            chooseSide(obj);

            if (Input.GetKey(KeyCode.D))
            {
                obj.rigidbody.velocity = new Vector2(speed / 2, obj.rigidbody.velocity.y);

            }
            else if (Input.GetKey(KeyCode.A))
            {
                obj.rigidbody.velocity = new Vector2(-speed / 2, obj.rigidbody.velocity.y);
            }
            else
            {
                obj.rigidbody.velocity = new Vector2(0f, obj.rigidbody.velocity.y);
            }

        }
        else 
        {
            empujaIdle = false ; obj.rigidbody.velocity = new Vector2(0f, obj.rigidbody.velocity.y);
        }
    }*/

    private void OnCollisionStay2D(Collision2D obj)
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

 //Ver desde que lado se empuja/tira
 void chooseSide(Collision2D p) {

        float sadajHigh = boxCollider2d.size.y / 2;
        float sadajCenterY = boxCollider2d.transform.position.y;
        float pies = sadajCenterY - sadajHigh;
        float pYMax = p.transform.position.y + p.collider.bounds.size.y / 2.2f;

        float sadajCenterX = boxCollider2d.transform.position.x;

        if ((sadajCenterX > p.collider.transform.position.x))//&& p.collider.CompareTag("empujable")
        {
            Debug.Log("derecha"); 
            if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.F)) { pull = true; push = false; Debug.Log("Tiro desde la derecha"); }

            else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.F)) { pull = false; push = true; Debug.Log("Empujo desde la derecha"); }

            else { pull = false; push = false; }

        }
        else if ((sadajCenterX < p.collider.transform.position.x))// && p.collider.CompareTag("empujable")
        {
            Debug.Log("izquierda");
            if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.F)) { pull = false; push = true; Debug.Log("Empujo desde la izquierda"); }

            else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.F)) { pull = true; push = false; Debug.Log("Tiro desde la izquierda"); }

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
            activateTimer = false; tLeft = 0.5f; control = true;
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


    }
}
