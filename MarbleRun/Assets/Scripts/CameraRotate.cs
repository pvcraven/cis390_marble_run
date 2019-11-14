using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    public GameObject target;
    private Transform targetTransform;
    private Transform camera;
    // Start is called before the first frame update
    void Start()
    {
        targetTransform = target.transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(targetTransform);
        if (gameObject.GetComponent<Camera>().enabled)
        {
            transform.Translate(Vector3.right * Time.deltaTime * 3);
        }
    }
}
