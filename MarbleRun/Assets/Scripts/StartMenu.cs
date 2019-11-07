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
        // Close game if the game is built
        Application.Quit();
        // Close Unity game if it's in a Unity Editor
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
