using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class luzInteract : MonoBehaviour
{
    private BarraDeVida script;
    private shieldManage scShieldM;


    public Animator anim;

    public Collision2D saddaj;

    public float daño;

    private BoxCollider2D boxCollider2d;

    private bool damage, morision;
    // Start is called before the first frame update
    void Start()
    {
        scShieldM = FindObjectOfType<shieldManage>();
        boxCollider2d = transform.GetComponent<BoxCollider2D>();
        //anim = GetComponent<Animator>();
        script = FindObjectOfType<BarraDeVida>();
        morision = false;
    }

    // Update is called once per frame
    void Update()
    {
        //if (scShieldM.shieldUP) damage = false;
        //Debug.Log(script.vida);
        //anim.SetBool("Muerte", morision);
    }

    private void OnTriggerStay2D(Collider2D entidad)
    {

        //if (entidad.CompareTag("shield")) { Debug.Log("Yepa"); }

        if (entidad.CompareTag("Player"))
        {
            

            if (!scShieldM.shieldUP)
            {
                damage = true;
                script.recibirDaño(daño);
                defuncion();
            }
            else damage = false;

            anim.SetBool("Damaged",damage);

        }
        else if (entidad.CompareTag("empujable"))
        {
            float empujableCentroY = entidad.transform.position.y;
            float empujableMidTopY = entidad.bounds.size.y / 2;
            float empujableTopY = empujableCentroY + empujableMidTopY;

            float luzCentroY = boxCollider2d.transform.position.y;
            float luzMidBotY = boxCollider2d.bounds.size.y / 2;
            float luzAbajoY = luzCentroY - luzMidBotY;

            if (empujableTopY > luzAbajoY)
            {
                boxCollider2d.transform.position = new Vector2(boxCollider2d.transform.position.x, empujableTopY+luzMidBotY+0.2f);
            }
        }

        //else 


    }
    private void OnTriggerExit2D(Collider2D p) 
    {
        if (p.CompareTag("Player")) damage = false; anim.SetBool("Damaged", damage); 
       
    }
    void defuncion() {
        if (script.vida <= 0f) {  script.dead = true; }
    }
}
