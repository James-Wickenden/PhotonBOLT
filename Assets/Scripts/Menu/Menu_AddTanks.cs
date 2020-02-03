using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_AddTanks : MonoBehaviour
{
    public GameObject tank;
    private int menuMode = 2;
    private int noTanks = 1;
    void Start()
    {
        if (menuMode == 2) noTanks = 10;
        for (int i = 0; i < noTanks; i++)
        {
            Instantiate(tank);
            tank.GetComponent<MenuTank_Transform>().transformMode = menuMode;

        }

    }
}
