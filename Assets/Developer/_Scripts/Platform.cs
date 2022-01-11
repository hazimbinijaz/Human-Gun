using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public Transform PlatformViewpoint;
    public int CurrentNoOfEnemies;
    public List<Enemies> AllEnemies;

    public void EnableAllEnemies()
    {
        foreach (Enemies enemy in AllEnemies)
        {
            enemy.StartPlayer();
        }
    }
    
}
