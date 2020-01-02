using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carteles : MonoBehaviour
{

    public GameObject wLux;
    public GameObject wMinas;
    public GameObject wEmpuja;
    // Start is called before the first frame update
    void Start()
    {
        //wLux = GameObject.FindGameObjectWithTag("wLuz");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D obj)
    {
        if (obj.CompareTag("cartelLuz"))
        {
            wLux.SetActive(true);
        }  
        
        else if (obj.CompareTag("cartelMinas"))
        {
            wMinas.SetActive(true);
        }

        else if (obj.CompareTag("cartelEmpuja"))
        {
            print("2");
            wEmpuja.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D obj)
    {
        if (obj.CompareTag("cartelLuz"))
        {
            wLux.SetActive(false);
        }

        else if (obj.CompareTag("cartelMinas"))
        {
            wMinas.SetActive(false);
        }

        else if (obj.CompareTag("cartelEmpuja"))
        {
            wEmpuja.SetActive(false);
        }
    }
}
