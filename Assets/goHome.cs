using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class goHome : MonoBehaviour
{

    public Text myText;

    public float coolDown = 5;
    public float coolDowntimer;

    public GameObject casa;

    // Start is called before the first frame update
    void Start()
    {
        myText.text = "Deberia de volver a casa";
        coolDowntimer = coolDown;
    }

    private void Update()
    {
        if (coolDowntimer > 0)
        {
            coolDowntimer -= Time.deltaTime;
        }

        if (coolDowntimer < 0)
        {
            coolDowntimer = 0;
        }

        if (coolDowntimer == 0)
        {
            casa.SetActive(false);
            myText.text = "";
        }
    }

}
