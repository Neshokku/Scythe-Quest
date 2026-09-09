using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    [SerializeField] private int currentLevel = 0;

    [SerializeField] private string titleScreen;
    [SerializeField] private string[] levels;

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        if (PlayerPrefs.HasKey("CurrentLevel"))
        {
            currentLevel = PlayerPrefs.GetInt("CurrentLevel");
        }
        else
        {
            currentLevel = 0;
            PlayerPrefs.SetInt("CurrentLevel", currentLevel);
        }
    }

    [ContextMenu("Restart Prefs")]
    void RestartPrefs()
    {
        PlayerPrefs.SetInt("CurrentLevel", 0);
    }

    [ContextMenu("Set Prefs")]
    void SetPrefs()
    {
        PlayerPrefs.SetInt("CurrentLevel", currentLevel);
    }

    public void LoadCurrentLevel()
    {
        LoadLevel(currentLevel);
    }

    public bool LoadLevel(int id)
    {
        if (LevelExists(id))
        {
            string levelName = levels[id];
            SceneManager.LoadScene(levelName);
            currentLevel = id;
            Debug.Log("Specified level id: " + id + " found in the level array. " + "Level Count: " + levels.Length);
            return true;
        }
        else
        {
            Debug.Log("Specified level id: " + id + " is not in the level array. " + "Level Count: " + levels.Length);
            return false;
        }
    }

    public bool LevelExists(int id)
    {
        if (id >= 0 && id < levels.Length)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void LoadNextLevel()
    {
        if (!LoadLevel(currentLevel + 1))
        {
            Debug.Log("Redirecting to Title Screen");
            LoadTitleScreen();
        }
    }

    public void LoadTitleScreen()
    {
        SceneManager.LoadScene(titleScreen);
    }
}
