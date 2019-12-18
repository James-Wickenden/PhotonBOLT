using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixToImage : MonoBehaviour
{
    private void Awake()
    {
        transform.parent = GameObject.FindWithTag("image_target").transform;
    }
}
