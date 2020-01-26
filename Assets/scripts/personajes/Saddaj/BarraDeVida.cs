using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraDeVida : MonoBehaviour
{
    // Start is called before the first frame update
    public Scrollbar barraDeVida;
    public Image enrojecido;
    private shieldManage scShieldM;
    public bool dead;


    public float vida, vidaMax, tiempoEsperaParaRegeneracion,vidaRecuperadaPorDecima;
    float tiempoParaRegeneracion, tiempoaux = 0;
    float vidaRecuperable;
    bool dañado = false;
    void Start()
    {
        scShieldM = FindObjectOfType<shieldManage>();
        enrojecido.canvasRenderer.SetAlpha(0.0f);

        vida = vidaMax;
        vidaRecuperable = vidaMax;
        dead = false;

        SonidoDaño.StopSound();
    }

    public void recibirDaño(float daño)
    {
        if (!scShieldM.shieldUP)
        {
            dañado = true;
            vida = Mathf.Clamp(vida - daño, 0f, vidaRecuperable);
            barraDeVida.size = vida / vidaMax;
            SonidoDaño.PlaySound();
        }
        else
        {
            dañado = false;
        }

        if (vida <= vidaMax / 2)
            vidaRecuperable = vidaMax / 2;
    }

    public void curarVida(float vidacurada)
    {
        if (!dead) { 
            vida = Mathf.Clamp(vida + vidacurada, 0f, vidaRecuperable);
            barraDeVida.size = vida / vidaMax;
        }
    }
    public void setHP(float v) { vida = v; }
    void Update()
    {
        //if (vida == 0f) {  dead = true; }
        if (!dead)
        {
            if (dañado)
            {
                tiempoParaRegeneracion = tiempoEsperaParaRegeneracion;
                dañado = false;
            }
            else
            {
                SonidoDaño.StopSound();
                tiempoParaRegeneracion = Mathf.Clamp(tiempoParaRegeneracion - Time.deltaTime, 0f, tiempoEsperaParaRegeneracion);

                if (tiempoParaRegeneracion <= 0)
                {
                    tiempoaux += Time.deltaTime;
                    if (tiempoaux >= 0.01f)
                    {
                        curarVida(vidaRecuperadaPorDecima);
                        tiempoaux = 0;
                    }

                }
            }
        }
        else {  Debug.Log("Muerto wey"); }

        enrojecido.CrossFadeAlpha(1 - vida/100, 0, false);

    }

    void outWorld() 
    { 
        
        //if(gameObject.CompareTag("Player"))
    
    }

    /*private void OnCollisionEnter2D(Collision2D outMap)
    {

        if (outMap.collider.CompareTag("fueraMapa"))
        {
            Debug.Log("Me muerooo");
            //script.setHP(0f);
        }
    }*/
}
