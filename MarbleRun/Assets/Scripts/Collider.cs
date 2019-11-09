using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Collider : MonoBehaviour
{
    private int collideCounter;

    private void OnCollisionEnter(Collision hit)
    {
        Debug.Log("HIT!");
        hit.gameObject.SendMessage("Damage");

    }

    private void Damage()
    {
        if (collideCounter != 3)
        {
            collideCounter++;
        }
        else
        {
            SceneManager.LoadScene("EndMenu");
        }
    }

}
