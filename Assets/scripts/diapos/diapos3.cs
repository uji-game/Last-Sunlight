using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class diapos3 : MonoBehaviour
{
    // Start is called before the first frame update

    public Sprite sprite1;
    public Sprite sprite2;
    public Sprite sprite3;
    public Sprite sprite4;
    public Sprite sprite5;
    public Sprite sprite6;
    public Sprite sprite7;
    public Sprite sprite8;
    public Sprite sprite9;
    public Sprite sprite10;
    public Sprite sprite11;
    public Sprite sprite12;
    public Sprite sprite13;
    public Sprite sprite14;
    public Sprite sprite15;

    private int cont;

    private SpriteRenderer spriteRenderer;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer.sprite == null)
            spriteRenderer.sprite = sprite1;
        cont = 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (cont == 16)
                SceneManager.LoadScene("Scenes/diapos4");
            else
            {
                spriteRenderer.sprite = Resources.Load<Sprite>("3/" + cont);
                cont++;
            }
        }
    }
}
