using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using DG.Tweening;

public class TheGameManager : MonoBehaviour
{
    public static TheGameManager Instance=null;
    public int _level;
    public GameObject Player;
    public event Action OnGameFail,OnGameWin;
    private Camera m_Camera;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if(Instance!=this)
            DestroyImmediate(Instance);
        DontDestroyOnLoad(Instance);
        Application.targetFrameRate = 60;
        _level = PlayerPrefs.GetInt("Level",1);
        m_Camera=Camera.main;
    }

 
    public void LevelFail()
    {
        UIManager.Instance.ShowLevelFailUI();
        OnGameFail?.Invoke();
        m_Camera.DOShakeRotation(1f);
    }

    
    
    public void LevelWin()
    {
        UIManager.Instance.ShowLevelCompleteUI();
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
}
