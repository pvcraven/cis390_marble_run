using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public void StartButtonPress()
    {
        SceneManager.LoadScene("Scene Selection");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
