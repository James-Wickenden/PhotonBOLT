using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollision : MonoBehaviour
{

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Hit!");
        // TODO: destory if touching other tank.
        // Only 

        //Destroy(gameObject);
    }
}
