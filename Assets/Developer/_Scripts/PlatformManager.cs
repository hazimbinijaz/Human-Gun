using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine.SceneManagement;

public class PlatformManager : MonoBehaviour
{

    [SerializeField] private List<Platform> Platforms;
    private GameObject m_Player;
    private int CurrentPlatform;
    private HumanGun m_PlayerGun;
    private int NoOfPlatforms;
    [SerializeField] private MyCrosshair m_Crosshair;
    // Start is called before the first frame update
    void Start()
    {
        CurrentPlatform = 0;
        m_Player = TheGameManager.Instance.Player;
        m_PlayerGun = m_Player.GetComponentInChildren<HumanGun>();
        Platforms[CurrentPlatform].EnableAllEnemies();
        NoOfPlatforms = Platforms.Count;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [Button("Next Platform")]
    public void NextPlatform()
    {
        
        m_Crosshair.IsShootable = false;
        CurrentPlatform++;
        if (CurrentPlatform > Platforms.Count - 1)
        {
            TheGameManager.Instance.LevelWin();
        }
        else
        {
            Platforms[CurrentPlatform].gameObject.SetActive(true);
            float animDuration = 1;
            m_PlayerGun.IsBonusLevel = Platforms[CurrentPlatform].IsBonusLevel;
            m_PlayerGun.MagLimit=Platforms[CurrentPlatform].MagLimit;
            m_Player.transform.DOMove(Platforms[CurrentPlatform].PlatformViewpoint.transform.position, animDuration)
                .SetEase(Ease.Linear);
            m_Player.transform
                .DORotateQuaternion(Platforms[CurrentPlatform].PlatformViewpoint.transform.rotation, animDuration)
                .SetEase(Ease.Linear).OnComplete(() =>
                {
                    m_Crosshair.IsShootable = true;
                    Platforms[CurrentPlatform].EnableAllEnemies();
                });
        }
    }

    public void OnEnemyDeath(int NoOfDeaths)
    {
        Platforms[CurrentPlatform].CurrentNoOfEnemies -= NoOfDeaths;
        if (Platforms[CurrentPlatform].CurrentNoOfEnemies <= 0)
        {
            if (CurrentPlatform + 1 > Platforms.Count - 1)
            {
                UIManager.Instance.ProgressBarFill.fillAmount += (1f / SceneManager.sceneCountInBuildSettings);
                PlayerPrefs.SetFloat("Fill",UIManager.Instance.ProgressBarFill.fillAmount);
            }
            Invoke(nameof(NextPlatform), 2f);
        }
    }
}
