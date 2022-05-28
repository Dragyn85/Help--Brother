using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class StoryTeller : MonoBehaviour
{
    [SerializeField] private List<FadeText> storys;
    private int _currentIndex = 0;

    private void Awake()
    {
        storys[_currentIndex].FadeIn();
    }

    private void OnMouseDown()
    {
        if (_currentIndex < storys.Count)
        {
            storys[_currentIndex].FadeOut();
            _currentIndex++;
            storys[_currentIndex].FadeIn();
        }
        else
        {
            GameManager.Instance.ReachedGoal(1);
        }
    }
}
