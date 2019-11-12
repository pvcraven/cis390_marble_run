using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera startingCamera;
    public Camera newCamera;

    private void OnTriggerEnter(UnityEngine.Collider collider)
    {
        if (collider.tag.Equals("Player"))
        {
            startingCamera.enabled = false;
            newCamera.enabled = true;
        }
    }
}
