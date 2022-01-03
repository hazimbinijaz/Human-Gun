using UnityEngine;
using UnityEngine.SceneManagement;

public class TheGameManager : MonoBehaviour
{
    public static TheGameManager Instance=null;
    public int _level;
    public GameObject Player;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if(Instance!=this)
            DestroyImmediate(Instance);
        DontDestroyOnLoad(Instance);
        Application.targetFrameRate = 60;
        _level = PlayerPrefs.GetInt("Level",1);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
}
