using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class StoryTeller : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private List<FadeText> storys;
    private int _currentIndex = 0;

    private void Awake()
    {
        storys[_currentIndex].FadeIn();
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if (_currentIndex < storys.Count-1)
        {
            storys[_currentIndex].FadeOut();
            _currentIndex++;
            storys[_currentIndex].FadeIn(); 
        }
        else
        {
            storys[_currentIndex].FadeOut();
            GameManager.Instance.ReachedGoal(1);
        }
    }
}
