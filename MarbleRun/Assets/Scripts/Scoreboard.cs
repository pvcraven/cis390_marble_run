using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class Scoreboard : MonoBehaviour
{

    private List<GameObject> marbles;
    private List<float[]> marbleStats;
    private float startDistance;
    private float startTime;

    Scoreboard()
    {
        marbles = new List<GameObject>();
        marbleStats = new List<float[]>();
    }

    public void AddMarble(GameObject marble, float distance, float time)
    {
        float[] marbleStat = { distance, time };
        marbleStats.Add(marbleStat);
        marbles.Add(marble);
    }

    void Display()
    {

    }

    void Update()
    {
        for(int i = 0; i < marbles.Count; i++)
        {
            float newDistance = startDistance - marbles[i].transform.position.y;
            float newTime = Time.time - startTime;
            float[] newStat = { newDistance, newTime };
            marbleStats[i] = newStat;
        }
    }

    void sortByDistance()
    {
        for (int i = 0; i < marbles.Count; i++)
        {
            for (int j = 0; j < marbles.Count - 1; j++)
            {
                if (marbleStats[j][0] > marbleStats[j + 1][0])
                {
                    // Sorts the distance and time
                    for(int k = 0; k < marbleStats[j].Length; k++)
                    {
                        float temp = marbleStats[j + 1][k];
                        marbleStats[j + 1][k] = marbleStats[j][k];
                        marbleStats[j][k] = temp;
                    }

                    // Sorts the marbles
                    GameObject marbleTemp = marbles[j + 1];
                    marbles[j + 1] = marbles[j];
                    marbles[j] = marbleTemp;
                }
            }
        }
    }
}
