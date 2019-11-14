using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.UI;

public class Scoreboard
{
    private List<GameObject[]> scoreboardDisplay;
    private List<GameObject> marbles;
    private List<GameObject> disqualifiedMarbles;
    private List<float[]> marbleStats;
    private float startDistance;
    private float startTime;
    private static int NUM_STATS = 2;
    private Vector2 panel_size;
    private static Color PANEL_COLOR = Color.white;

    public Scoreboard(float startDistance, float startTime)
    {
        scoreboardDisplay = new List<GameObject[]>();
        marbles = new List<GameObject>();
        marbleStats = new List<float[]>();
        disqualifiedMarbles = new List<GameObject>();
        this.startDistance = startDistance;
        this.startTime = startTime;
    }

    public void AddMarble(GameObject marble)
    {
        // Add marble stats to list
        float[] marbleStat = { startDistance - marble.transform.position.y, Time.time - startTime };
        marbleStats.Add(marbleStat);

        // Add marble to list
        marbles.Add(marble);
    }

    // Creates the canvas, panel, and text GameObjects for the scoreboard display
    public void Create()
    {
        panel_size = new Vector2(290, 15 * (marbles.Count + 1) + 3);

        // Create the canvas we draw the scoreboard on
        GameObject canvasObject = new GameObject("Canvas");
        Canvas canvas = canvasObject.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceCamera;

        // Create the panel for the scoreboard
        GameObject panel = new GameObject("Scoreboard Panel");
        panel.transform.SetParent(canvasObject.transform);
        RectTransform panelRect = panel.AddComponent<RectTransform>();
        panelRect.pivot = Vector2.up;
        panelRect.anchorMin = Vector2.up;
        panelRect.anchorMax = Vector2.up;
        panelRect.anchoredPosition = Vector2.zero;
        panelRect.sizeDelta = panel_size;
        panel.AddComponent<CanvasRenderer>();
        Image panelImage = panel.AddComponent<Image>();
        panelImage.color = PANEL_COLOR;

        // Create the scoreboard rows
        for (int r = 0; r < marbles.Count + 1; r++)
        {
            GameObject[] scoreboardRow = new GameObject[NUM_STATS + 1];
            for (int c = 0; c < NUM_STATS + 1; c++)
            {     
                GameObject scoreboardCell = new GameObject("Scoreboard Cell (" + r + ", " + c + ")");
                scoreboardCell.transform.SetParent(panel.transform);
                RectTransform cellRect = scoreboardCell.AddComponent<RectTransform>();
                cellRect.pivot = Vector2.up;
                cellRect.anchorMin = Vector2.up;
                cellRect.anchorMax = Vector2.up;
                cellRect.sizeDelta = panel_size;
                scoreboardCell.transform.localPosition = new Vector2(120 * c + 5, -15 * r);
                Text cellText = scoreboardCell.AddComponent<Text>();
                cellText.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
                cellText.color = Color.black;
                scoreboardRow[c] = scoreboardCell;
            }
            scoreboardDisplay.Add(scoreboardRow);
        }
    }

    public void Display()
    {
        // Display the header row
        Text headerName = scoreboardDisplay[0][0].GetComponent<Text>();
        headerName.text = "Marble Name";
        headerName.fontStyle = FontStyle.BoldAndItalic;
        Text headerDistance = scoreboardDisplay[0][1].GetComponent<Text>();
        headerDistance.text = "Distance";
        headerDistance.fontStyle = FontStyle.BoldAndItalic;
        Text headerTime = scoreboardDisplay[0][2].GetComponent<Text>();
        headerTime.text = "Time";
        headerTime.fontStyle = FontStyle.BoldAndItalic;

        // Display the marbles that are not disqualified
        for (int r = 0; r < marbles.Count; r++)
        {
            scoreboardDisplay[r + 1][0].GetComponent<Text>().text = marbles[r].name;

            for(int c = 1; c <= NUM_STATS; c++)
            {
                scoreboardDisplay[r + 1][c].GetComponent<Text>().text = marbleStats[r][c - 1].ToString("0.00");
            }
        }

        // Display the marbles that are disqualified
        for (int r = 0; r < disqualifiedMarbles.Count; r++)
        {
            scoreboardDisplay[r + marbles.Count + 1][0].GetComponent<Text>().text = disqualifiedMarbles[r].name;

            for (int c = 1; c <= NUM_STATS; c++)
            {
                scoreboardDisplay[r + marbles.Count + 1][c].GetComponent<Text>().text = "DQ";
            }
        }
    }

    public void Update()
    {
        for(int i = 0; i < marbles.Count; i++)
        {
            // Disqualify marble if it falls below the tray
            if (marbles[i].transform.position.y < GameObject.Find("tray").transform.position.y)
            {
                disqualifiedMarbles.Add(marbles[i]);
                marbles.RemoveAt(i);
            }

            // Get the new distances and times of the marbles
            float newDistance = startDistance - marbles[i].transform.position.y;
            float newTime = Time.time - startTime;
            float[] newStat = { newDistance, newTime };
            marbleStats[i] = newStat;
        }

        SortByDistance();
    }

    private void SortByDistance()
    {
        for (int i = 0; i < marbles.Count; i++)
        {
            for (int j = 0; j < marbles.Count - 1; j++)
            {
                if (marbleStats[j][0] < marbleStats[j + 1][0])
                {
                    // Sorts the marble stats
                    for(int k = 0; k < NUM_STATS; k++)
                    {
                        float temp = marbleStats[j + 1][k];
                        marbleStats[j + 1][k] = marbleStats[j][k];
                        marbleStats[j][k] = temp;
                    }

                    // Sorts the marbles
                    GameObject marbleTemp = marbles[j + 1];
                    marbles[j + 1] = marbles[j];
                    marbles[j] = marbleTemp;
                }
            }
        }
    }
}
