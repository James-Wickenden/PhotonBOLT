using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public GameObject controlPanel;

    public void Awake()
    {
        Health.OnDeathOccured += hideUI;
        RespawnTimer.OnRespawn += showUI;
    }

    private void hideUI()
    {
        controlPanel.SetActive(false);
    }

    private void showUI()
    {
        controlPanel.SetActive(true);
    }

}