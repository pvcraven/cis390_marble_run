using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    GameObject scoreboardCanvas;

    // Start is called before the first frame update
    private void Start()
    {
        // Get the name of the winning marble
        scoreboardCanvas = GameObject.Find("Scoreboard Canvas");
        GameObject firstPlaceCell = scoreboardCanvas.transform.GetChild(0).transform.GetChild(3).gameObject;
        string winningMarble = firstPlaceCell.GetComponent<Text>().text;

        // Display the name of the winning marble
        GameObject endGameMessage = this.transform.GetChild(1).gameObject;
        endGameMessage.GetComponent<Text>().text = winningMarble + " won!";
    }

    public void StartButtonPress()
    {
        Destroy(scoreboardCanvas);
        SceneManager.LoadScene("Scene Selection");
    }

    public void ExitGame()
    {

        Application.Quit();
       
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
