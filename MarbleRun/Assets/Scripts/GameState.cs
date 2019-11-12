using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameState : MonoBehaviour
{
    public GameObject prefab;
    public Material mat1;
    public Material mat2;
    public Material mat3;
    public Material mat4;
    
    void Start()
    {
        Material[] materials = new Material[4] { mat1, mat2, mat3, mat4 };
        // Create a bunch of random marbles.
            for (int x = 0; x < 4; x++)
            {
            // Create the marble
                prefab.GetComponent<Renderer>().material = materials[x];
                Instantiate(prefab, new Vector3(13f, 2.5f, -14f + x), Quaternion.identity);
            }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
