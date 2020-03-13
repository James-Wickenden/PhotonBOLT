using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    public float damage;

    [SerializeField]
    public float speed;

    private int sourceID;
    private Bolt.NetworkId networkId;

    public void setSourceID(int sourceID)
    {
        this.sourceID = sourceID;
    }

    public void setSourceNetworkID(Bolt.NetworkId networkId)
    {
        this.networkId = networkId;
    }

    public int getSourceID()
    {
        return sourceID;
    }

    public Bolt.NetworkId getSourceNetworkID()
    {
        return networkId;
    }

    public float getSpeed()
    {
        return speed;
    }


    public void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject);
    }
}