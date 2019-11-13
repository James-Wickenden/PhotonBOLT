﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkCallbacks : Bolt.GlobalEventListener
{
    public override void SceneLoadLocalDone(string scene) {
        var spawnPosition = new Vector3(Random.range(-8,8),0,Random.range(-8,8));
        BoltNetwork.Instantiate(BoltPrefabs.Cube, spawnPosition, Quaternion.identity);

    }
}
