using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetoRecojer : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject me;
    public GameObject character;

    public float wait = 100;
    private float ejecutar = 0;

    void Start()
    {
        //movcont.recojido = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            character.SendMessage("recogerObj", true);
            //movcont.recojido = true;
            Destroy(me);
            ejecutar = Time.time + wait;
            
        }
        character.SendMessage("recogerObj", false);
    }

    
}
