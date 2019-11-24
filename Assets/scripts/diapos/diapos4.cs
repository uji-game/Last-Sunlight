using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class diapos4 : MonoBehaviour
{
    // Start is called before the first frame update

    public Sprite sprite1;
    public Sprite sprite2;
    public Sprite sprite3;
    public Sprite sprite4;
    public Sprite sprite5;
    public Sprite sprite6;
    public Sprite sprite7;

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
            if (cont == 7)
                SceneManager.LoadScene("Scenes/Nivel 2");
            else
            {
                spriteRenderer.sprite = Resources.Load<Sprite>("4/" + cont);
                cont++;
            }
        }
    }
}