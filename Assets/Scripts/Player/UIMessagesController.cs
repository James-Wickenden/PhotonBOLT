using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMessagesController : MonoBehaviour 
{
    [SerializeField]
    private DeathMessage deathMessagePrefab;

    private void Awake(){
        Health.OnDeathOccured += DisplayDeathMessage;
    }
    
    private void DisplayDeathMessage() {
        var deathMessage = Instantiate(deathMessagePrefab, transform);

    }
}