using System;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] CanvasGroup _canvasGroup;
    private bool _isActive;

    private void Awake()
    {
        _canvasGroup.alpha = _isActive ? 1 : 0;
        _canvasGroup.interactable = _isActive;
        _canvasGroup.blocksRaycasts = _isActive;
    }

    private void Start()
    {
        GameManager.Instance.PausePressed += Toggle;
    }

    private void OnDisable()
    {
        GameManager.Instance.PausePressed -= Toggle;
    }

    public void Toggle()
    {
        _isActive = !_isActive;
        _canvasGroup.alpha = _isActive ? 1 : 0;
        _canvasGroup.interactable = _isActive;
        _canvasGroup.blocksRaycasts = _isActive;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}