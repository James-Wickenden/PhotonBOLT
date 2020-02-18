using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public GameObject shootButton;
    public GameObject joystick;

    public void Awake()
    {
        shootButton = GameObject.FindGameObjectWithTag("ShootButton");
        joystick = GameObject.FindGameObjectWithTag("joystick");
        Health.OnDeathOccured += hideUI;
        DeathMessage.OnRespawn += showUI;
    }

    private void hideUI()
    {
        shootButton.SetActive(false);
        joystick.SetActive(false);
    }

    private void showUI()
    {
        shootButton.SetActive(true);
        joystick.SetActive(true);
    }



}