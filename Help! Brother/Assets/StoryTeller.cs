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
    [SerializeField] bool _clickToContinue = true;
    private float _time;
    [SerializeField] private float _storytime = 2f;

    private void Awake()
    {
        storys[_currentIndex].FadeIn();
    }

    private void Update()
    {
        if(_clickToContinue)
            return;

        _time += Time.deltaTime;
        if (_time > _storytime)
        {
            _time = 0;
            ContinueStory(0);
        }
            
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ContinueStory(1);
    }

    private void ContinueStory(int nextLevel)
    {
        if (_currentIndex < storys.Count - 1)
        {
            storys[_currentIndex].FadeOut();
            _currentIndex++;
            storys[_currentIndex].FadeIn();
        }
        else
        {
            storys[_currentIndex].FadeOut();
            GameManager.Instance.ReachedGoal(nextLevel);
        }
    }
}
