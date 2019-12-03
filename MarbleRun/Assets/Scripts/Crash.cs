using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Crash : MonoBehaviour
{
    GameObject[] marblesNotFinished = new GameObject[1];
    bool end = false;

    void OnCollisionEnter(Collision hit)
    {
        GameObject marble = hit.gameObject;
        marble.transform.GetChild(0).tag = "marbleFinished";
    }

    private void Update()
    {
        marblesNotFinished = GameObject.FindGameObjectsWithTag("marbleNotFinished");
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
        GameObject scoreboardCanvas = GameObject.Find("Scoreboard Canvas");
        DontDestroyOnLoad(scoreboardCanvas);
        SceneManager.LoadScene("EndMenu");
    }
}