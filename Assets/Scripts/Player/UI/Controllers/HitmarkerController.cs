using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitmarkerController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject hitmarkerPrefab;

    GameObject spawned;

    float endTime;

    [SerializeField]
    float hitmarkerDuration = 0.025f;

    void Awake()
    {
        HitDetection.OnPlayerHitByMe += StartHitmarkerAnimation;
    }

    // Update is called once per frame
    void Update()
    {
        if (spawned != null && Time.time > endTime)
        {
            Destroy(spawned);
        }
    }

    void StartHitmarkerAnimation()
    {
        spawned = Instantiate(hitmarkerPrefab, transform);
        endTime = Time.time + hitmarkerDuration;
    }

}
