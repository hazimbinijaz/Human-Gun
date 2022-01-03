using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;

public class PlatformManager : MonoBehaviour
{

    [SerializeField] private List<Platform> Platforms;
    private GameObject m_Player;
    private int CurrentPlatform;
    private HumanGun m_PlayerGun;
    // Start is called before the first frame update
    void Start()
    {
        CurrentPlatform = 0;
        m_Player = TheGameManager.Instance.Player;
        m_PlayerGun = m_Player.GetComponentInChildren<HumanGun>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    [Button("Next Platform")]
    public void NextPlatform()
    {
        CurrentPlatform++;
        Platforms[CurrentPlatform].gameObject.SetActive(true);
        m_Player.transform.DOMove(Platforms[CurrentPlatform].PlatformViewpoint.transform.position, 1f);
        m_Player.transform.DORotateQuaternion(Platforms[CurrentPlatform ].PlatformViewpoint.transform.rotation, 1f);
    }

    public void OnEnemyDeath(int NoOfDeaths)
    {
        Platforms[CurrentPlatform].CurrentNoOfEnemies -= NoOfDeaths;
        if(Platforms[CurrentPlatform].CurrentNoOfEnemies<=0)
            NextPlatform();
    }
}
