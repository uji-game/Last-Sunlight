using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class luzInteract : MonoBehaviour
{
    private BarraDeVida script; 
    
    public Animator anim;

    public Collision2D saddaj;

    private bool damage, morision;
    // Start is called before the first frame update
    void Start()
    {
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


    }
    private void OnTriggerExit2D(Collider2D sad) 
    {
        if (sad.CompareTag("Player")) damage = false; anim.SetBool("Damaged", damage); ; 
    }
    void defuncion() {
        if (script.vida == 0f) { morision = true; }
    }
}
