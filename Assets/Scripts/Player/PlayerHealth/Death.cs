using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Death : Bolt.EntityEventListener<ICustomCubeState>
{
    public void TriggerDeathEvent()
    {
        var death = DeathEvent.Create(entity);
        death.Send();
    }

    public override void OnEvent(DeathEvent evnt)
    {
        ScoreboardEvent scoreboardEvent = ScoreboardEvent.Create();
        scoreboardEvent.username = state.Username;
        scoreboardEvent.score = state.Score;
        scoreboardEvent.Send();

        BoltNetwork.Destroy(gameObject);
    }
}