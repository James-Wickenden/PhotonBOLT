using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Bolt.EntityBehaviour<ICustomCubeState>
{
    public int health = 3;

    public override void Attached()
    {
        state.Health = health;
        state.AddCallback("Health", HealthCallback);
    }

    private void HealthCallback()
    {
        health = state.Health;

        if (health <= 0)
        {
            BoltNetwork.Destroy(gameObject);
        }
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            state.Health -= 1;
        }
    }
}
