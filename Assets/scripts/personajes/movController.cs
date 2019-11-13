using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movController : MonoBehaviour
{//jump tutorial
    public float jumpVel = 5f;
    public float cSpeed = 3f;
    [SerializeField] private LayerMask platformsLayerMask;
    //[SerializeField] private LayerMask trepableLayerMask;

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
    private bool moving, agachado, climbing, volando;
    private bool platJump = true;
    private bool recojido = false;

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
        if(onGround()) platJump = true; 
        Move();
        if (activateTimer) cooldown();
        animScript();
        //Debug.Log(platJump);
    }
    public void recogerObj(bool valor)
    {
        recojido = valor;
    }


    void Move() {
        if (Input.GetKey(KeyCode.S) && climbing == false)
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

            if (Input.GetKey(KeyCode.A) && climbing==false)
            {
                rb2d.velocity = new Vector2(-speed, rb2d.velocity.y);
                moving = true;
            }
            else if (Input.GetKey(KeyCode.D) && climbing == false)
            {
                rb2d.velocity = new Vector2(speed, rb2d.velocity.y);
                moving = true;
            }

            else
            {
                rb2d.velocity = new Vector2(0, rb2d.velocity.y);
                moving = false;
            }
            Jump();

        }

        if (!facingRight && rb2d.velocity.x > 0)     {   flipSprite(); }
        else if (facingRight && rb2d.velocity.x/*mH*/ < 0) {   flipSprite(); }
    }

    void Jump() {
        if (onGround() &&  Input.GetKeyDown(KeyCode.Space) && platJump)
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
        float platformTop = trep.transform.position.y + trep.bounds.size.y/1.8f;
        float trepYMax = trep.transform.position.y + trep.bounds.size.y / 1.95f;

        float pies = sadajCenter - sadajHigh;

        //hay un bug al llegar arriba de la plataforma, siguiente sprint para solucionarlo

        if (trep.CompareTag("trepable") && Input.GetKey(KeyCode.W) && control ) //&& !(sadajCenter > platformTop)
        {
            
            platJump = true;
           
            climbing = true;
            if (sadajCenter < platformTop ) rb2d.velocity = new Vector2(0f, cSpeed);
            else {  rb2d.velocity = new Vector2(0f, 0f); }


        }
        else climbing = false;

        if (pies > trepYMax) { platJump = false; }

        if ((Input.GetKeyDown(KeyCode.Space) && platJump ) || ((sadajCenter >= platformTop) && platJump)) //&& !(sadajCenter>trepYMax))
        {
            climbing = false;
            
            rb2d.velocity = Vector2.up * jumpVel;
            platJump = false;
            activateTimer = true;
            control = false;
        }
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
        anim.SetBool("Recojido", recojido);


    }
}
