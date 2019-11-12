using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene2Camera : MonoBehaviour
{
	public Camera startCamera;
	public Camera pegBoardCamera;
	public Camera endCamera;

	// Start is called before the first frame update
	void Start()
	{
		ShowStartCamera();
	}

	void Update()
	{

	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag.Equals("pegBoard"))
		{
			ShowPegBoardCamera();
		}

		if (collision.gameObject.tag.Equals("Collision_P"))
		{
			ShowEndCamera();
		}
	}
	public void ShowStartCamera()
	{
		pegBoardCamera.enabled = false;
		endCamera.enabled = false;
		startCamera.enabled = true;
	}

	public void ShowPegBoardCamera()
	{
		startCamera.enabled = false;
		endCamera.enabled = false;
		pegBoardCamera.enabled = true;
	}

	public void ShowEndCamera()
	{
		startCamera.enabled = false;
		pegBoardCamera.enabled = false;
		endCamera.enabled = true;
	}

}

