using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    private Rigidbody rb;
    private float speed = 5000.0f;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag.Equals("Player") || other.gameObject.tag.Equals("LeadBall"))
        {
            Debug.Log("hit");
            rb = other.gameObject.GetComponent<Rigidbody>();
            Vector3 dir = new Vector3(100f, 0f, 0f);
            dir.Normalize();
            rb.AddForce(dir * speed);
        }
    }
}
