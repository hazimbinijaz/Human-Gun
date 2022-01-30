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
        levelText.text = "Level " + PlayerPrefs.GetInt("Level", 1);
        ProgressBarFill.fillAmount = PlayerPrefs.GetFloat("Fill",0);
        int OB = PlayerPrefs.GetInt("FirstTime",0);
        if(OB==0)
            OnBoarding.SetActive(true);
        OB++;
        PlayerPrefs.SetInt("FirstTime",OB);
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