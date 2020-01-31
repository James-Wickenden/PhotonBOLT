using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : Bolt.EntityBehaviour<ICustomCubeState>
{
    [SerializeField]
    public float damage;

    private int sourceID;

    public void setSourceID(int sourceID)
    {
        this.sourceID = sourceID;
    }

    public int getSourceID()
    {
        return sourceID;
    }

    public void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject);
    }
}
