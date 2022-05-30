using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool IsInputStoped;
    private float _levelTimer;
    private bool _timerActive;
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
        if(_timerActive)
            _levelTimer += Time.deltaTime;
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PausePressed?.Invoke();
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= HandleNewScene;
    }

    private void HandleNewScene(Scene scene, LoadSceneMode arg1)
    {
        if (scene.name != "Intro" && scene.name != "MainMenu")
        {
            PlayerPrefs.SetString("CurrentLevel",scene.buildIndex.ToString());
            RestetTimer();
        }
    }

    private void RestetTimer()
    {
        _levelTimer = 0f;
        _timerActive = true;
    }

    public void ReachedGoal(int nextLevelIndex, TMP_Text timeField = null)
    {
        HandleTimer(timeField);
        LoadCorrectScene(nextLevelIndex);
    }

    private void HandleTimer(TMP_Text timeField)
    {
        _timerActive = false;
        if (timeField != null)
            timeField.SetText(_levelTimer.ToString("0.#"));
    }

    private void LoadCorrectScene(int nextLevelIndex)
    {
        if (nextLevelIndex == -1)
            StartCoroutine(LoadLevelAfterSeconds(2f, "MainMenu"));
        else if (SceneManager.sceneCountInBuildSettings - 2 > nextLevelIndex)
            StartCoroutine(LoadLevelAfterSeconds(4f, nextLevelIndex));
        else
            StartCoroutine(LoadLevelAfterSeconds(2f, "End"));
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