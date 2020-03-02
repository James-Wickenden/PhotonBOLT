using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon;

public class Scoreboard : Bolt.GlobalEventListener
{
    private bool isScoreboardOpen = false;
    public GameObject scoreboard;
    private Dictionary<string, string> scoreMap = new Dictionary<string, string>();

    private void Start() {
        Button scoreboardButton = GameObject.FindGameObjectWithTag("ScoreboardButton").GetComponent<Button>();
        scoreboardButton.onClick.AddListener(() => ToggleScoreboard());
        scoreboard.SetActive(false);
        Debug.Log("Initialised scoreboard listener");
    }

    private void ToggleScoreboard() {
        if (!isScoreboardOpen) {
            isScoreboardOpen = true;
            refreshScoreboard();
            scoreboard.SetActive(true);
            Debug.Log("Opened scoreboard; " + scoreMap.Count + " players");

        }
        else {
            isScoreboardOpen = false;
            scoreboard.SetActive(false);
            Debug.Log("Closed scoreboard");

        }
    }

    private void refreshScoreboard() {
        scoreMap.Clear();

        string playerID, playerScore;
        foreach (var player in BoltNetwork.Connections)
        {
            playerID = player.UserData.ToString();
            playerScore = GetInstanceID().ToString();
            scoreMap.Add(playerID, playerScore);
        }
        scoreMap.Add("1", "100");
        scoreMap.Add("2", "200");
        scoreMap.Add("3", "300");
    }
}
