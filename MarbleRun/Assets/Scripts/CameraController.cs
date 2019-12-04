using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera startingCamera;
    public Camera newCamera;

    private void OnTriggerEnter(UnityEngine.Collider collider)
    {
        if (collider.tag.Equals("LeadBall"))
        {
            startingCamera.enabled = false;
            startingCamera.gameObject.GetComponent<AudioListener>().enabled = false;
            newCamera.enabled = true;
            newCamera.gameObject.GetComponent<AudioListener>().enabled = true;
        }
    }
}
