using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scoreboard : MonoBehaviour
{
    private bool isScoreboardOpen = false;

    private void Start() {
        Button scoreboardButton = GameObject.FindGameObjectWithTag("ScoreboardButton").GetComponent<Button>();
        scoreboardButton.onClick.AddListener(() => ToggleScoreboard());
        Debug.Log("Initialised scoreboard listener");
    }

    private void ToggleScoreboard() {
        Debug.Log("Detected scoreboard toggle");
        if (!isScoreboardOpen) {
            isScoreboardOpen = true;
            Debug.Log("Opened scoreboard");
        }
        else {
            isScoreboardOpen = false;
            Debug.Log("Closed scoreboard");
        }
    }

    private void Update() {
        
    }
}
