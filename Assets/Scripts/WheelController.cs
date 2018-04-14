using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelController : MonoBehaviour {

    public GameObject car;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigger " + other.tag);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision " + collision.collider.tag);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Debug.Log("Prueba" + hit.controller.tag);
    }


}
