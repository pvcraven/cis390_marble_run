using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckLeadBall : MonoBehaviour
{
    public GameObject currentLeadBall;
    private void OnTriggerEnter(Collider other)
    {
        if (currentLeadBall == null && other.tag.Equals("Player"))
        {
            other.tag = "LeadBall";
            currentLeadBall = other.gameObject;
            Destroy(gameObject);
        }
        else if (currentLeadBall != null && other.tag.Equals("Player"))
        {
            other.tag = "LeadBall";
            currentLeadBall.tag = "Player";
            currentLeadBall = other.gameObject;
            Destroy(gameObject);
        }
        else if (other.tag.Equals("LeadBall"))
        {
            Destroy(gameObject);
        }
    }
}
