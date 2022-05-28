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
        SceneManager.sceneLoaded += HandleNewScene;
        if (Instance == null)
        {
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
        StartCoroutine(LoadLevelAfterSeconds(2f,nextLevelIndex));
      
    }

    private IEnumerator LoadLevelAfterSeconds(float time, int nextlevel)
    {
        IsInputStoped = true;
        yield return new WaitForSeconds(time);
        IsInputStoped = false;
        SceneManager.LoadScene(nextlevel);
    }
}