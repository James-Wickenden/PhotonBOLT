﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon;
using System.Linq;

public class Scoreboard : Bolt.GlobalEventListener
{
    private bool isScoreboardOpen = false;
    public GameObject modelScoreboard;
    private GameObject scoreboard;
    private Text playerScores;
    
    private void Start() {
        scoreboard = Instantiate(modelScoreboard, GameObject.FindGameObjectWithTag("Canvas").transform);
        playerScores = getScoreField();

        Button scoreboardButton = GameObject.FindGameObjectWithTag("ScoreboardButton").GetComponent<Button>();
        scoreboardButton.onClick.AddListener(() => ToggleScoreboard());
        scoreboard.SetActive(false);
        //Debug.Log("Initialised scoreboard listener");
    }

    private void ToggleScoreboard() {
        if (!isScoreboardOpen) {
            isScoreboardOpen = true;
            refreshScoreboard();
            scoreboard.SetActive(true);
            //Debug.Log("Opened scoreboard");
        }
        else {
            isScoreboardOpen = false;
            scoreboard.SetActive(false);
            //Debug.Log("Closed scoreboard");
        }
    }

    private Text getScoreField() {
        Text[] textboxes = scoreboard.GetComponentsInChildren<Text>();
        foreach (Text textbox in textboxes) if (textbox.name == "Scores Text") return textbox;
        return null;
    }

    private void refreshScoreboard() {
        Dictionary<string, int> scoreMap = new Dictionary<string, int>();
        scoreMap.Clear();

        // TODO: Fix this!
        // BoltNetwork.Connections and BoltNetwork.Clients both returns empty lists!
        // PlayerScores are not implemented and must be connected to a player/tank instance!

        string playerID;
        int playerScore;
        foreach (var player in BoltNetwork.Entities)
        {
            
            // Placeholder values
            playerScore = player.GetComponent<Scoring>().GetScore();
            playerID = player.GetComponent<Username>().getUsername();
            scoreMap.Add(playerID, playerScore);
        }

        Debug.Log(scoreMap.Count + " players found in server and parsed into scoreboard");
        parseScoreMap(scoreMap);
    }

    private void parseScoreMap(Dictionary<string, int> scoreMap) {
        playerScores.text = "";

        // Sorts the player-score map by scores;
        // then writes the player-score pairs to the playerScores text field.

        // this iterates through key-value pairs sorted by value
        foreach (KeyValuePair<string, int> score in scoreMap.OrderBy(key => -key.Value))
        {
            playerScores.text += score.Key;
            playerScores.text += " : ";
            playerScores.text += score.Value;
            playerScores.text += '\n';
        }
    }
}