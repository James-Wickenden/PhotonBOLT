using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XPBar : MonoBehaviour
{
    [SerializeField]
    private Image forgroundImage;

    [SerializeField]
    private float updateSpeedSeconds = 0.5f;

    [SerializeField]
    private float positionOffset;

    private XP XP;

    public void SetXP(XP XP)
    {
        Debug.Log("setting xp");
        this.XP = XP;
        XP.OnXPPctChanged += HandleXPChanged;
    }

    private void HandleXPChanged(float pct)
    {
        StartCoroutine(ChangeToPct(pct));
    }

    private IEnumerator ChangeToPct(float pct)
    {
        float preChangePct = forgroundImage.fillAmount;
        float elapsed = 0f;

        while (elapsed < updateSpeedSeconds)
        {
            elapsed += Time.deltaTime;
            forgroundImage.fillAmount = Mathf.Lerp(preChangePct, pct, elapsed / updateSpeedSeconds);
            yield return null;
        }

        forgroundImage.fillAmount = pct;
    }

    private void LateUpdate()
    {
        transform.position = Camera.main.WorldToScreenPoint(XP.transform.position + Vector3.up * positionOffset);
    }

    private void OnDestroy()
    {
        XP.OnXPPctChanged -= HandleXPChanged;
    }
}
