
using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    [SerializeField] private int nextLevel;
    [SerializeField] private GameObject _levelCompleted;
    [SerializeField] private TMP_Text _timeField;

    private void Awake()
    {
        nextLevel = SceneManager.GetActiveScene().buildIndex+1;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<GuidedBrother>() != null)
        {
            _levelCompleted.SetActive(true);
            GameManager.Instance.ReachedGoal(nextLevel,_timeField);
        }
    }
}
