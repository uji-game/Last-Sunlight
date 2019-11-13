using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextImporter : MonoBehaviour
{
    // Start is called before the first frame update
    public TextAsset textFile;
    public string[] textLines;

    void Start()
    {
        if (textFile != null)
        {
            textLines = (textFile.text.Split('\n'));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
