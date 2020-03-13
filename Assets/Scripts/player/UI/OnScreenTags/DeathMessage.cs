using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathMessage : MonoBehaviour
{
    private Text respawnMessage;

    private void Awake()
    {
        RespawnTimer.OnTimerTick += updateTimerLabel;
        respawnMessage = GetComponent<Text>();
    }

    private void updateTimerLabel(float remainingRespawnTime)
    {
        respawnMessage.text = "Respawning in " + Mathf.Round(remainingRespawnTime).ToString();
        if (remainingRespawnTime < 0)
        {
            Debug.Log("Hide Label");
            gameObject.SetActive(false);
        }
    }

    private void hideTimerLabel()
    {
        Debug.Log("Hide Label'");
        //Destroy(gameObject);
    }
}