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

    public void setSourceID(int sourceID)
    {
        this.sourceID = sourceID;
    }

    public int getSourceID()
    {
        return sourceID;
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