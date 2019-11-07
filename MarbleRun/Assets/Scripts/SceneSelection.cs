﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSelection : MonoBehaviour
{
    private GameObject[] sceneList;
    private int index;

    // Start is called before the first frame update
    void Start()
    {
        sceneList = new GameObject[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            sceneList[i] = transform.GetChild(i).gameObject;
        }

        foreach (GameObject go in sceneList)
        {
            go.SetActive(false);
        }

        if(sceneList[0])
        {
            sceneList[0].SetActive(true);
        }
    }

    public void LeftButton()
    {
        sceneList[index].SetActive(false);

        index -= 1;
        if (index < 0)
            index = sceneList.Length - 1;

        sceneList[index].SetActive(true);
    }

    public void RightButton()
    {
        sceneList[index].SetActive(false);

        index += 1;
        if (index == sceneList.Length)
            index = 0;

        sceneList[index].SetActive(true);
    }

    public void confirmButton()
    {
        if (index == 0)
        {
            SceneManager.LoadScene("Scene01");
        }
        else if(index == 1)
        {
            SceneManager.LoadScene("Scene02");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}