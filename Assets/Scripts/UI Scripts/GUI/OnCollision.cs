using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollision : MonoBehaviour {

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Enter");
    }

    private void OnCollisionExit(Collision collision)
    {
        Debug.Log("Exit");
    }
}
