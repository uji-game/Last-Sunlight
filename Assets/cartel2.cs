using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cartel2 : MonoBehaviour
{
    public GameObject wEscudo;
    public GameObject wOjo;
    public GameObject wAtaque;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D obj)
    {
        if (obj.CompareTag("cartelEscudo"))
        {
            wEscudo.SetActive(true);
        }

        else if (obj.CompareTag("cartelOjo"))
        {
            wOjo.SetActive(true);
        }

        else if (obj.CompareTag("cartelAtaque"))
        {
            wAtaque.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D obj)
    {
        if (obj.CompareTag("cartelEscudo"))
        {
            wEscudo.SetActive(false);
        }

        else if (obj.CompareTag("cartelOjo"))
        {
            wOjo.SetActive(false);
        }

        else if (obj.CompareTag("cartelAtaque"))
        {
            wAtaque.SetActive(false);
        }
    }
}
