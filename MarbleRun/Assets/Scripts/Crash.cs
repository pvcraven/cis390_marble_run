using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Crash : MonoBehaviour
{
    void Start()
    {
        Debug.Log("Start!");
    }      void OnCollisionEnter(Collision hit)     {         Debug.Log("HIT!");
    }
}