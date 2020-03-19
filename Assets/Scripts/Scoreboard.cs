using System.Collections;
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
    private Dictionary<string, int> scoreMap;
    
    private void Start() {
        scoreboard = Instantiate(modelScoreboard, GameObject.FindGameObjectWithTag("Canvas").transform);
        playerScores = getScoreField();

        Button scoreboardButton = GameObject.FindGameObjectWithTag("ScoreboardButton").GetComponent<Button>();
        scoreboardButton.onClick.AddListener(() => ToggleScoreboard());
        scoreboard.SetActive(false);
        //Debug.Log("Initialised scoreboard listener");

        scoreMap = new Dictionary<string, int>();
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
        //Debug.Log(scoreMap.Count + " players found in server and parsed into scoreboard");
        parseScoreMap(scoreMap);
    }

    private void parseScoreMap(Dictionary<string, int> scoreMap) {
        playerScores.text = "";

        // Sorts the player-score map by scores;
        // then writes the player-score pairs to the playerScores text field.

        // this iterates through key-value pairs sorted by value, ties are sorted by key
        foreach (KeyValuePair<string, int> score in scoreMap.OrderByDescending(x => x.Value).ThenBy(x => x.Key))
        { 
            playerScores.text += score.Key;
            playerScores.text += " : ";
            playerScores.text += score.Value;
            playerScores.text += '\n';
        }
    }

    public override void OnEvent(PlayerScoreEvent evnt)
    {
        scoreMap[evnt.username] = evnt.score;
        Debug.Log("score for " + evnt.username + " set to " + evnt.score);

        // refresh the scoreboard if it's open
        if (isScoreboardOpen)
        {
            refreshScoreboard();
        }
    }

    public override void OnEvent(SetScoreEvent evnt)
    {
        if (!scoreMap.Keys.Contains(evnt.username)) scoreMap.Add(evnt.username, 0);
    }
}
