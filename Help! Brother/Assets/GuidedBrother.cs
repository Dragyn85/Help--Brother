using System;
using UnityEngine;
using UnityEngine.AI;

public class GuidedBrother : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    private Vector3 _basePos;
    public Vector3 BasePos => _basePos;
    public event Action RecivedNewDestination; 

    public void SetDestionation(Vector3 target, bool setBasePos = true)
    {
        if (setBasePos)
            _basePos = target;
            
        _agent.SetDestination(target);
        RecivedNewDestination?.Invoke();
    }
    
}