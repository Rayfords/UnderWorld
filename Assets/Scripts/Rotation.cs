using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    void Start()
    {

    }

    void FixedUpdate()
    {
        Quaternion rotationY = Quaternion.AngleAxis(1, Vector3.up/*(0, 1, 0)*/);
        transform.rotation *= rotationY;

        //transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
    }
}
