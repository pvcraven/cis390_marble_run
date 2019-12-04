using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingSound : MonoBehaviour
{
    private AudioSource audio;

    private void Start()
    {
        audio = gameObject.GetComponent<AudioSource>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (!audio.isPlaying)
        {
            audio.Play();
        }
    }


    private void OnCollisionExit(Collision collision)
    {
        if (audio.isPlaying)
        {
            audio.Stop();
        }
    }
}
