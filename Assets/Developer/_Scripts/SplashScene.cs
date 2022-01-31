using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int levelToLoad = PlayerPrefs.GetInt("Level", 1);
        levelToLoad = levelToLoad % SceneManager.sceneCountInBuildSettings;
        if (levelToLoad == 0)
            levelToLoad = 1;
        SceneManager.LoadScene(levelToLoad);
    }
  
}
