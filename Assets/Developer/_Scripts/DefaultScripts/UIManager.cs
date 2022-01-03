using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject startUI;
    public GameObject inGameUI;
    public GameObject failUI;
    public GameObject completeUI;
    public TMP_Text levelText;
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
        // UpdateLevelText();
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
        TheGameManager.Instance._level++;
        PlayerPrefs.SetInt("Level",TheGameManager.Instance._level);
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