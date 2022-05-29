using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool IsInputStoped;
    public event Action PausePressed;

    private void Awake()
    {
        
        if (Instance == null)
        {
            SceneManager.sceneLoaded += HandleNewScene;
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            PausePressed?.Invoke();
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= HandleNewScene;
    }

    private void HandleNewScene(Scene scene, LoadSceneMode arg1)
    {
        if(scene.name != "Intro" && scene.name != "MainMenu")
            PlayerPrefs.SetString("CurrentLevel",scene.buildIndex.ToString());
    }

    public void ReachedGoal(int nextLevelIndex)
    {
        if(SceneManager.sceneCountInBuildSettings-2 > nextLevelIndex)
            StartCoroutine(LoadLevelAfterSeconds(2f,nextLevelIndex));
        else
        {
            StartCoroutine(LoadLevelAfterSeconds(2f,"End"));
        }
      
    }

    private IEnumerator LoadLevelAfterSeconds(float time, int nextlevel)
    {
        IsInputStoped = true;
        yield return new WaitForSeconds(time);
        IsInputStoped = false;
        SceneManager.LoadScene(nextlevel);
    }
    private IEnumerator LoadLevelAfterSeconds(float time, string levelName)
    {
        IsInputStoped = true;
        yield return new WaitForSeconds(time);
        IsInputStoped = false;
        SceneManager.LoadScene(levelName);
    }
}