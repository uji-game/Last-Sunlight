using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraDeVida : MonoBehaviour
{
    // Start is called before the first frame update
    public Scrollbar barraDeVida;
    public GameObject bVida;

    public float vida, vidaMax, tiempoEsperaParaRegeneracion,vidaRecuperadaPorDecima, despVida;
    float tiempoParaRegeneracion, tiempoaux = 0;
    float vidaRecuperable;
    bool dañado = false;


    void Start()
    {
        vida = vidaMax;
        vidaRecuperable = vidaMax;
        //despVida = 0.133f;

        //bVida = GetComponentInChildren<GameObject>();
    }

    public void recibirDaño(float daño)
    {
        dañado = true;
        vida = Mathf.Clamp(vida - daño, 0f, vidaRecuperable);
        barraDeVida.size = vida/vidaMax;
        //bVida.transform.position += new Vector3 (despVida, 0f, 0f);
        

        if (vida <= vidaMax / 2)
            vidaRecuperable = vidaMax / 2;
    }

    public void curarVida(float vidacurada)
    {
        vida = Mathf.Clamp(vida + vidacurada, 0f, vidaRecuperable);
        barraDeVida.size = vida / vidaMax;
    }

    void Update()
    {
        if (dañado)
        {
            tiempoParaRegeneracion = tiempoEsperaParaRegeneracion;
            dañado = false;
        }
        else
        {
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
}
