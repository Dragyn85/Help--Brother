
using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField] private int nextLevel;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<GuidedBrother>() != null)
        {
            GameManager.Instance.ReachedGoal(nextLevel);
        }
    }
}
