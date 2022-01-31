using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject startUI;
    public GameObject inGameUI;
    public GameObject failUI;
    public GameObject completeUI;
    public TMP_Text levelText;
    public Image ProgressBarFill;
    public List<Text> ProgressBarTexts,ProgressBarTextsLevelComplete;
    public GameObject OnBoarding;
    // public PlayerMovement PlayerController;
    public InputControls PlayerController;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        startUI.SetActive(true);
        inGameUI.SetActive(false);
        failUI.SetActive(false);
        completeUI.SetActive(false);
        int level = PlayerPrefs.GetInt("Level", 1);
        levelText.text = "Level " + level;
        SetBarCounts();
        ProgressBarFill.fillAmount = PlayerPrefs.GetFloat("Fill",0);
        int OnBoardingCheck = PlayerPrefs.GetInt("FirstTime",0);
        if(OnBoardingCheck==0)
            OnBoarding.SetActive(true);
        OnBoardingCheck++;
        PlayerPrefs.SetInt("FirstTime",OnBoardingCheck);
        
    }

    private void SetBarCounts()
    {
        int Count = PlayerPrefs.GetInt("BarCount", 0);
        foreach (Text text in ProgressBarTexts)
        {
            text.text = (int.Parse(text.text) + Count).ToString();
        }
        foreach (Text text in ProgressBarTextsLevelComplete)
        {
            text.text = (int.Parse(text.text) + Count).ToString();
        }

    }
    

    public void OnClickStartGame()
    {
        // Elephant.LevelStarted(TheGameManager.Instance._level);
        startUI.SetActive(false);
        inGameUI.SetActive(true);
        if(PlayerController)
            PlayerController.enabled = true;
        // PlayerController._Anim.enabled = true;
    }

    public void ShowLevelCompleteUI()
    {
        
        inGameUI.SetActive(false);
        completeUI.SetActive(true);
        
    }
    
    
    public void ShowLevelFailUI()
    {
        inGameUI.SetActive(false);
        failUI.SetActive(true);
    }

    public void OnClickRestartLevelBtn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnClickContinueLevelBtn()
    {
        int sceneToLoad = SceneManager.GetActiveScene().buildIndex;
        sceneToLoad = (sceneToLoad + 1) % SceneManager.sceneCountInBuildSettings;
        if (sceneToLoad == 0)
            sceneToLoad = 1;
        SceneManager.LoadScene(sceneToLoad);
    }
}