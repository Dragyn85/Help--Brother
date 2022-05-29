
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    [SerializeField] private int nextLevel;

    private void Awake()
    {
        nextLevel = SceneManager.GetActiveScene().buildIndex+1;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<GuidedBrother>() != null)
        {
            GameManager.Instance.ReachedGoal(nextLevel);
        }
    }
}
