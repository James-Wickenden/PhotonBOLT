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