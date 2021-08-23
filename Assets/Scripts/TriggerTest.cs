using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTest : MonoBehaviour
{
    public bool move = false;


    public void OnTriggerEnter(Collider fish)
    {
        Debug.Log("move=true");
        move = true;
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("move=true");
        move = true;
    }
}
