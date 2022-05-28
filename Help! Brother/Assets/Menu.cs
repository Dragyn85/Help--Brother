using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject LoadGameButton;
    private int _currentLevel;

    private void Awake()
    {
        var loadGame = "";
        loadGame = PlayerPrefs.GetString("CurrentLevel");
        if (loadGame == "")
        {
            _currentLevel = 0;
            LoadGameButton.SetActive(false);
        }
        else
        {
            _currentLevel = int.Parse(loadGame);
            LoadGameButton.SetActive(true);
        }
    }

  

    public void LoadGame()
    {
        SceneManager.LoadScene(_currentLevel);
    }

    public void NewGame()
    {
        SceneManager.LoadScene("Intro");
    }
}