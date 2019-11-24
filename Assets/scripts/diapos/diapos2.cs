using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class diapos2 : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite sprite1;
    public Sprite sprite2;
    public Sprite sprite3;
    public Sprite sprite4;
    public Sprite sprite5;
    public Sprite sprite6;
    public Sprite sprite7;

    private float nextActionTime;
    private float period;

    private int cont;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        period = 2.0f;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite1;
        cont = 2;
        nextActionTime = Time.time + 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextActionTime)
        {
            if (cont == 8)
                SceneManager.LoadScene("Scenes/diapos3");
            else
            {
                spriteRenderer.sprite = Resources.Load<Sprite>("2/" + cont);
                cont++;
            }
            period = 0.4f;
            nextActionTime += period;
        }
    }
}
