using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Crash : MonoBehaviour
{ 
    int colliderCount;      void OnCollisionEnter(Collision hit)     {         Debug.Log("HIT!");

        if (colliderCount == 1)
        {
            SceneManager.LoadScene("EndMenu");
        }
        else
        {
            colliderCount++;
        }
    }
}