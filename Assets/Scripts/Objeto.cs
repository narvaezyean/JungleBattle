using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objeto : MonoBehaviour
{
    public int objetvalue;
    public GameObject testObject;
    // Start is called before the first frame update
    void Start()
    {
        objetvalue = testObject.GetComponent<text>().textvalue;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
