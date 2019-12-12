using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class luzInteract : MonoBehaviour
{

    Scene scene;

    private BarraDeVida script;
    private shieldManage scShieldM;


    public Animator anim;

    public Collision2D saddaj;

    public float daño;

    private BoxCollider2D boxCollider2d;

    private bool damage, morision;
    private Vector3 luxPosIni;

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
        scene = SceneManager.GetActiveScene();

        //if (scShieldM.shieldUP) damage = false;
        Debug.Log("posIni: "+luxPosIni+"pos luz: "+ boxCollider2d.transform.position);
        //anim.SetBool("Muerte", morision);
    }
    /*private void OnCollisionEnter2D(Collision2D obj)
    {
        if (obj.collider.CompareTag("empujable")) { luxPosIni = boxCollider2d.transform.position; print("funco"); }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        
    }*/

    private void OnCollisionExit2D(Collision2D p)
    {
        if (p.collider.CompareTag("empujable") && scene.name == "Nivel 2") { boxCollider2d.attachedRigidbody.position = luxPosIni;  }

    }

    private void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.CompareTag("empujable")) { luxPosIni = boxCollider2d.transform.position; print("funco"); }

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
                if(scene.name == "Nivel 1")boxCollider2d.transform.position = new Vector2(boxCollider2d.transform.position.x, empujableTopY+luzMidBotY+0.2f);
                else if(scene.name == "Nivel 2")boxCollider2d.transform.position = new Vector2(boxCollider2d.transform.position.x, empujableTopY+luzMidBotY-0.4f);
            }
        }

        //else 


    }

    
    private void OnTriggerExit2D(Collider2D p) {

        if (p.CompareTag("Player")) { damage = false; anim.SetBool("Damaged", damage); }
        else if (p.CompareTag("empujable") && scene.name == "Nivel 2") { boxCollider2d.attachedRigidbody.transform.position = luxPosIni; }



    }
    void defuncion() {
        if (script.vida <= 0f) {  script.dead = true; }
    }
}
