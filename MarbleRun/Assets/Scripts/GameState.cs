using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameState : MonoBehaviour
{
    public GameObject prefab;
	public GameObject startGate;
	public Button startButton;
    private List<GameObject> marbles;
    private List<GameObject> marbleLabels;
    const int marbleCount = 4;
    private Scoreboard scoreboard;
    private int colorSelection;


    void Start()
    {

        colorSelection = SceneSelection.MateralSelection;
        Material[] materials = new Material[4];
        switch (colorSelection)
        {
            case 0:
                materials[0] = Resources.Load<Material>("Blue");
                materials[1] = Resources.Load<Material>("Purple");
                materials[2] = Resources.Load<Material>("Red");
                materials[3] = Resources.Load<Material>("Yellow");
                break;
            case 1:
                materials[0] = Resources.Load<Material>("Pink");
                materials[1] = Resources.Load<Material>("Blue");
                materials[2] = Resources.Load<Material>("Green");
                materials[3] = Resources.Load<Material>("Orange");
                break;
            default:
                materials[0] = Resources.Load<Material>("Blue");
                materials[1] = Resources.Load<Material>("Purple");
                materials[2] = Resources.Load<Material>("Red");
                materials[3] = Resources.Load<Material>("Yellow");
                break;
        }
        // Create a bunch of random marbles with labels.
        marbles = new List<GameObject>();
        marbleLabels = new List<GameObject>();
        for (int x = 0; x < marbleCount; x++)
        {
            string marbleName = materials[x].name + " Marble";

            // Create the marble
            prefab.GetComponent<Renderer>().material = materials[x];
		    marbles.Add(Instantiate(prefab, new Vector3(Random.Range(10f, 16f), 2.5f, -14f + x), Quaternion.identity));
            marbles[x].name = marbleName;
            GameObject marbleState = new GameObject("Marble State");
            marbleState.transform.SetParent(marbles[x].transform);
            marbleState.tag = "marbleNotFinished";

            // Create the marble label
            GameObject label = new GameObject(marbleName + " Label");

            // Set the label text
            TextMesh labelText = label.AddComponent<TextMesh>();
            labelText.text = marbleName;
            labelText.characterSize =0.75f;
            labelText.color = Color.white;
            labelText.anchor = TextAnchor.UpperCenter;
            labelText.alignment = TextAlignment.Center;
            Outline labelOutline = label.AddComponent<Outline>();

            label.transform.position = marbles[x].transform.position;
            marbleLabels.Add(label);
        }

        // Initialize scoreboard
        scoreboard = new Scoreboard();
        foreach (GameObject marble in marbles)
        {
            scoreboard.AddMarble(marble);
        }
		scoreboard.Create();
    }

    // Update is called once per frame
    void Update()
    {
		scoreboard.Display();
		scoreboard.Update();
		LabelsFollow();

		if (startGate.activeSelf)
		{
			if (CheckMarbleStop())
			{
				Button btn = startButton.GetComponent<Button>();
				btn.gameObject.SetActive(true);
				btn.onClick.AddListener(TaskOnClick);
			}
		}
    }

	private bool CheckMarbleStop()
	{
		foreach (GameObject marble in marbles)
		{
			Rigidbody marblerb = marble.GetComponent<Rigidbody>();
			Vector3 v3Velocity = marblerb.velocity;
			if ((v3Velocity.x == 0 && v3Velocity.y == 0 && v3Velocity.z == 0) || Time.time > 10)
			{
				return true;
			}
		}
		return false;
	}

	void TaskOnClick()
	{
		Button btn = startButton.GetComponent<Button>();
        scoreboard.StartRace();
		btn.gameObject.SetActive(false);
		startGate.SetActive(false);
	}

    void LabelsFollow()
    {
        // Disables main camera, which is not in use
        if (GameObject.Find("Main Camera"))
        {
            GameObject.Find("Main Camera").GetComponent<Camera>().enabled = false;
        }

        for (int marbleNum = 0; marbleNum < marbleLabels.Count; marbleNum++)
        {
            GameObject marble = marbles[marbleNum];
            GameObject label = marbleLabels[marbleNum];
            // SOURCE: https://answers.unity.com/questions/132592/lookat-in-opposite-direction.html
            List<Camera> cameras = new List<Camera>();
            cameras.AddRange(GameObject.FindObjectsOfType<Camera>());
            foreach (Camera camera in cameras)
            {
                if (camera.enabled)
                {
                    label.transform.rotation = Quaternion.LookRotation(label.transform.position - camera.transform.position);
                    label.transform.position = marble.transform.position + new Vector3(0, 2, 0);
                }
            }
        }
    }
}
