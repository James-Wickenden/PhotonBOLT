using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMessagesController : MonoBehaviour 
{
    [SerializeField]
    private DeathMessage deathMessagePrefab;

    public static event System.Action OnRespawn = delegate { };
    private void Awake(){
        Health.OnDeathOccured += DisplayDeathMessage;
        DeathMessage.OnRespawn  += InitiateRespawn;
    }
    private void DisplayDeathMessage() {
        var deathMessage = Instantiate(deathMessagePrefab, transform);

    }

    private void InitiateRespawn() {
        OnRespawn();
    }

    private void LateUpdate() {

    }


    // private void LateUpdate()
    // {
    //     transform.position = Camera.main.WorldToScreenPoint(Vector3.up);
    // }
    // private Text deathMessage;
 
    // void Update () {
    //     deathMessage.text = "You died!";
    // }
}