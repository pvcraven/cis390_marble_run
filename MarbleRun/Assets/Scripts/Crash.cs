using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Crash : MonoBehaviour
{
    GameObject[] marblesNotFinished;
    bool end = false;

    void OnCollisionEnter(Collision hit)
    {
        GameObject marble = hit.gameObject;
        marble.transform.GetChild(0).tag = "Finish";
        marblesNotFinished = GameObject.FindGameObjectsWithTag("Respawn");
        //Debug.Log("HIT! " + marblesNotFinished.Length);
    }

    private void Update()
    {
        if (!end && marblesNotFinished.Length == 0)
        {
            end = true;
            StartCoroutine(GoToEnd());
        }
    }

    // Goes to end menu after 5 second delay
    private IEnumerator GoToEnd()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("EndMenu");
    }
}