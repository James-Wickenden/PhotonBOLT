using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scoring : Bolt.EntityBehaviour<ICustomCubeState>
{

    private int currentScore = 0;
    //public static event System.Action OnResetScore = delegate { };


    private void Awake()
    {
        GetComponentInParent<TankListener>().OnXP += ModifyScore;
        //Health.OnStoreScore += PlayerDeath;
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
            state.Score = 0;
        }

        state.AddCallback("Score", ScoreCallback);

    }

    public int getScore()
    {
        return currentScore;
    }

    public void setScore(int score)
    {
        currentScore = score;
        Debug.Log("Score set : " + score);
        //state.Score = score;
    }


    private void ScoreCallback()
    {
        currentScore = state.Score;
    }

    private void PlayerDeath()
    {
        // we have to tell networkcallbacks to store currentScore
        KeepScoreEvent evnt = KeepScoreEvent.Create(Bolt.GlobalTargets.OnlySelf);
        evnt.username = state.Username;
        evnt.score = currentScore;
        evnt.Send();
    }
}