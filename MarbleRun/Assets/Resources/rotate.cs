using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour
{
    public float rotationSpeed = 2f;
    private System.Random randNum = new System.Random();
    private float randomX;
    private float randomY;
    private float randomZ;

    public Transform startZ;
    public Transform endZ;
    public Transform startY;
    public Transform endY;
    public Transform startX;
    public Transform endX;

    private float movementSpeed = 20f;

    void Start()
    {
        startZ = GameObject.Find("StartZ").transform;
        endZ = GameObject.Find("EndZ").transform;
        startY = GameObject.Find("StartY").transform;
        endY = GameObject.Find("EndY").transform;
        startX = GameObject.Find("StartX").transform;
        endX = GameObject.Find("EndX").transform;

        gameObject.transform.position = new Vector3(randNum.Next((int)endX.position.x, (int)startX.position.x), randNum.Next((int)endY.position.y, (int)startY.position.y), startZ.position.z);

        int sign = 0;

        sign = updateSign(sign);
        Debug.Log(sign);
        randomX = (float)randNum.NextDouble() * sign;
        sign = updateSign(sign);
        randomY = (float)randNum.NextDouble() * sign;
        sign = updateSign(sign);
        randomZ = (float)randNum.NextDouble() * sign;

        Debug.Log(randomX + ", " + randomY + ", " + randomZ);
    }

    int updateSign(int sign)
    {
        sign = 0;

        bool done = false;
        while (!done)
        {
            if (sign == 0)
            {
                sign = randNum.Next(-1, 2);
            }
            else
            {
                done = true;
            }
        }

        return sign;
    }

    void Update()
    {
        transform.Rotate(new Vector3(randomX, randomY, randomZ));
        transform.Translate(new Vector3(0, -0.1f, 1) * movementSpeed * Time.deltaTime, relativeTo: Space.World);
    }
}
