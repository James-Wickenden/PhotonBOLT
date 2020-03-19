using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scoring : Bolt.EntityBehaviour<ICustomCubeState>
{

    private int currentScore = 0;
    public event System.Action<int> OnStoreScore = delegate { };
    

    private void Awake()
    {
        GetComponentInParent<TankListener>().OnXP += ModifyScore;
        GetComponentInParent<TankListener>().OnPlayerAdded += setScore;
    }

    private void ModifyScore(Bolt.NetworkId networkID, int amount)
    {
        if (entity.IsOwner && entity.NetworkId.Equals(networkID))
        {
            state.Score += amount;
        }
    }

    public override void Attached()
    {
        if (entity.IsOwner)
        {
            state.Score = currentScore;
        }

        state.AddCallback("Score", ScoreCallback);

    }

    public int GetScore()
    {
        return currentScore;
    }

    public void setScore(string username, int score)
    {
        if (entity.IsOwner && GetComponentInParent<Username>().getUsername().Equals(username)) {
            currentScore = score;
            state.Score = score;
            Debug.Log("Score set : " + score);
        }
    }

    private void ScoreCallback()
    {
        currentScore = state.Score;

        //Notify every player's scoreboard
        PlayerScoreEvent evnt = PlayerScoreEvent.Create();
        evnt.username = GetComponent<Username>().getUsername();
        evnt.score = currentScore;
        evnt.Send();
    }
}