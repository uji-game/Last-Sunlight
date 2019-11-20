using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class luzInteract : MonoBehaviour
{
    private BarraDeVida script; 
    
    public Animator anim;

    public Collision2D saddaj;

    private BoxCollider2D boxCollider2d;

    private bool damage, morision;
    // Start is called before the first frame update
    void Start()
    {
        boxCollider2d = transform.GetComponent<BoxCollider2D>();
        //anim = GetComponent<Animator>();
        script = FindObjectOfType<BarraDeVida>();
        morision = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(script.vida);
        anim.SetBool("Muerte", morision);
    }

    private void OnTriggerStay2D(Collider2D sad)
    {

        if (sad.CompareTag("Player"))
        {
            
            damage = true;
            script.recibirDaño(0.25f);
            defuncion();

            anim.SetBool("Damaged",damage);

        }
        else if (sad.CompareTag("empujable"))
        {
            float empujableCentroY = sad.transform.position.y;
            float empujableMidTopY = sad.bounds.size.y / 2;
            float empujableTopY = empujableCentroY + empujableMidTopY;

            float luzCentroY = boxCollider2d.transform.position.y;
            float luzMidBotY = boxCollider2d.bounds.size.y / 2;
            float luzAbajoY = luzCentroY - luzMidBotY;

            if (empujableTopY > luzAbajoY)
            {
                boxCollider2d.transform.position = new Vector2(boxCollider2d.transform.position.x, empujableTopY+luzMidBotY+0.2f);
            }
        }


    }
    private void OnTriggerExit2D(Collider2D sad) 
    {
        if (sad.CompareTag("Player")) damage = false; anim.SetBool("Damaged", damage); ; 
    }
    void defuncion() {
        if (script.vida == 0f) { morision = true; }
    }
}
