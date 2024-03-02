using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public GameObject Jhon;

    void Update()
    {
        if (Jhon != null)
        {
            Vector3 position = transform.position;
            position.x = Jhon.transform.position.x;
            transform.position = position;
        }
    }
}
