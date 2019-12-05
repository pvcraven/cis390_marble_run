using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GameState : MonoBehaviour
{
    public GameObject prefab;
	public GameObject startGate;
	public Button startButton;
    private List<GameObject> marbles;
    private List<GameObject> marbleLabels;
    private List<GameObject> meteors;
    const int marbleCount = 4;
    private Scoreboard scoreboard;
    private int colorSelection;
    private GameObject darkMap;
    private GameObject spawnMeteors;


    void Start()
    {
        darkMap = GameObject.Find("DarkMap");
        spawnMeteors = GameObject.Find("SpawnMeteors");
        colorSelection = SceneSelection.MateralSelection;
        Material[] materials = new Material[4];
        switch (colorSelection)
        {
            case 0:
                materials[0] = Resources.Load<Material>("Light Blue");
                materials[1] = Resources.Load<Material>("Purple");
                materials[2] = Resources.Load<Material>("Red");
                materials[3] = Resources.Load<Material>("Yellow");
                break;
            case 1:
                materials[0] = Resources.Load<Material>("Pink");
                materials[1] = Resources.Load<Material>("Purple");
                materials[2] = Resources.Load<Material>("Green");
                materials[3] = Resources.Load<Material>("Orange");
                break;
            case 2:
                materials[0] = Resources.Load<Material>("Dark Blue");
                materials[1] = Resources.Load<Material>("Blue");
                materials[2] = Resources.Load<Material>("Orange");
                materials[3] = Resources.Load<Material>("Red");
                break;
            case 3:
                materials[0] = Resources.Load<Material>("Earth");
                materials[1] = Resources.Load<Material>("Moon");
                materials[2] = Resources.Load<Material>("Mars");
                materials[3] = Resources.Load<Material>("Pluto");
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
            string marbleName = materials[x].name;
            
            // Create the marble
            prefab.GetComponent<Renderer>().material = materials[x];
            Vector3 spawnVector = new Vector3(Random.Range(-2f, 2f), x, Random.Range(-2f, 2f));
            marbles.Add(Instantiate(prefab, this.transform.position + spawnVector, Quaternion.identity));
            marbles[x].name = marbleName;

            // Add trails to marbles
            TrailRenderer trail = marbles[x].AddComponent<TrailRenderer>();
            trail.time = 0.25f;
            trail.material = marbles[x].GetComponent<MeshRenderer>().material;
            AnimationCurve curve = new AnimationCurve();
            curve.AddKey(0.0f, 0.75f);
            curve.AddKey(0.6f, 0.25f);
            curve.AddKey(1.0f, 0.0f);
            trail.widthCurve = curve;

            // If the map is dark, make the marbles glow
            // In order to use this, an empty GameObject named "DarkMap" must be added to the scene.
            if (darkMap != null)
            {
                Light marbleGlow = marbles[x].AddComponent<Light>();
                marbleGlow.color = materials[x].color;
                marbleGlow.intensity *= 5;
            }

            // Rolling sound for marbles (only plays if they are touching a surface)
            // Add external script to each marble for collisions
            marbles[x].AddComponent<RollingSound>();
            marbles[x].AddComponent<AudioSource>();
            AudioSource audio = marbles[x].GetComponent<AudioSource>();
            AudioClip newClip = Resources.Load<AudioClip>("Roll");
            Debug.Log(newClip);
            audio.clip = newClip;
            audio.playOnAwake = true;
            audio.loop = true;
            audio.spatialBlend = 1;
            audio.rolloffMode = AudioRolloffMode.Linear;
            audio.maxDistance = 50;
            audio.volume = 0.75f;

            // Create the marble label
            GameObject labelPrefab = Resources.Load<GameObject>("Label");
            GameObject label = Instantiate(labelPrefab, marbles[x].transform.position, Quaternion.identity, marbles[x].transform) as GameObject;
            label.name = marbleName + " Label";
            label.tag = "marbleNotFinished";

            // Set the label text
            TextMeshPro labelText = label.GetComponent<TextMeshPro>();
            labelText.text = marbleName;
            labelText.fontSize = 14;
            labelText.color = Color.white;
            labelText.alignment = TextAlignmentOptions.Center;
            labelText.outlineColor = Color.black;
            labelText.outlineWidth = 0.2f;
            labelText.fontStyle = FontStyles.Bold;
            

            //label.transform.position = marbles[x].transform.position;
            marbleLabels.Add(label);
        }

        // This spawns meteors, but it DESTROYS the framerate
        /*if (spawnMeteors != null)
        {
            meteors = new List<GameObject>();

            for (int x = 0; x < 20; x++)
            {
                GameObject meteor = (GameObject) Resources.Load("meteor");
                meteors.Add(Instantiate(meteor));
            }
        }*/

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

        // Update audio volume for each marble based on its speed (caps out at 20)
        foreach (GameObject marble in marbles)
        { 
            AudioSource audio = marble.GetComponent<AudioSource>();
            Rigidbody rb = marble.GetComponent<Rigidbody>();
            float velocityX = Mathf.Abs(rb.velocity.x);
            float velocityZ = Mathf.Abs(rb.velocity.z);
            float maxVelocity = Mathf.Max(velocityX, velocityZ);
            audio.volume = maxVelocity/10;
        }
    }

    private bool CheckMarbleStop()
	{
		foreach (GameObject marble in marbles)
		{
			Rigidbody marblerb = marble.GetComponent<Rigidbody>();
			Vector3 v3Velocity = marblerb.velocity;
			if ((v3Velocity.x == 0 && v3Velocity.y == 0 && v3Velocity.z == 0) || Time.timeSinceLevelLoad > 8)
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
