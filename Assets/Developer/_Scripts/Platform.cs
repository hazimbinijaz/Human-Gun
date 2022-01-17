using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public Transform PlatformViewpoint;
    public List<Enemies> AllEnemies;
    public int CurrentNoOfEnemies;

    private void Start()
    {
        CurrentNoOfEnemies = AllEnemies.Count;
    }

    public void EnableAllEnemies()
    {
        foreach (Enemies enemy in AllEnemies)
        {
            enemy.StartPlayer();
        }
    }
    
}
