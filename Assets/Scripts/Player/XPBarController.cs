using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPBarController : MonoBehaviour
{
    [SerializeField]
    private XPBar xpBarPrefab;

    private Dictionary<XP, XPBar> XPBars = new Dictionary<XP, XPBar>();

    private void Awake()
    {
        XP.OnXPAdded += AddXPBar;
        XP.OnXPRemoved += RemoveXPBar;
    }

    private void AddXPBar(XP XP)
    {
        if (!XPBars.ContainsKey(XP))
        {
            var XPBar = Instantiate(xpBarPrefab, transform);
            XPBar.SetXP(XP);
            XPBars.Add(XP, XPBar);
        }
    }

    private void RemoveXPBar(XP XP)
    {
        if (XPBars.ContainsKey(XP))
        {
            Destroy(XPBars[XP].gameObject);
            XPBars.Remove(XP);
        }
    }
}
