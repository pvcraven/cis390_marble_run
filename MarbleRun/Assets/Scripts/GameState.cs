using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameState : MonoBehaviour
{
    public GameObject [] sphere_list;
    public List<GameObject> sphereList;

    // public Transform playerObject;
    public float distanceFromObject = 6f;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start of GameState script");

        // Get the button, and hook the TaskOnClick method to it

        // Set timeScale to zero, which pauses -everything-.

        // Create a bunch of random marbles.
        sphereList = new List<GameObject>();

            for (int x = 0; x < 3; x++)
            {
                // Create the marble
                GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                sphere.transform.position = new Vector3(0f + x, 0f, 0f + x);

                // Add the rigidbody for physics.
                Rigidbody gameObjectsRigidBody = sphere.AddComponent<Rigidbody>(); 
                gameObjectsRigidBody.mass = 5;
                 
                sphereList.Add(sphere);
            }
    }
    



    // Update is called once per frame
    void Update()
    {
        // Camera code to follow one of the marbles

        // Just grab the first one we created
        GameObject sphere = sphereList[0];

        Transform playerObject = sphere.transform;
        Vector3 lookOnObject = playerObject.position - transform.position;
        lookOnObject = playerObject.position - transform.position;
        transform.forward = lookOnObject.normalized;
        Vector3 playerLastPosition;
        playerLastPosition = playerObject.position - lookOnObject.normalized * distanceFromObject;
        playerLastPosition.y = playerObject.position.y + distanceFromObject / 2;
        transform.position = playerLastPosition;
    }
}
