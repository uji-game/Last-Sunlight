using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocksBeha : MonoBehaviour
{
    public GameObject roca;
    // Start is called before the first frame update
    void Start()
    {
        //roca = this.GetComponent<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.tag == "muerteRocas") {
            print("palmo");
            roca.SetActive(false);
        }
    }
}
