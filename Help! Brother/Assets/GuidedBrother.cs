using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class GuidedBrother : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    private Vector3 _basePos;
    public Vector3 BasePos => _basePos;
    private Vector3 _latestDirection;
    public event Action RecivedNewDestination; 

    public void SetDestionation(Vector3 target, bool setBasePos = true)
    {
        if (setBasePos)
            _basePos = target;
            
        _agent.SetDestination(target);
        RecivedNewDestination?.Invoke();
    }

    
    private void Awake()
    {
        _basePos = new Vector3(
            transform.position.x,
            transform.position.y,
            transform.position.z);
    }

    private void Update()
    {
        if (_agent.velocity.magnitude > 0.05f)
            _latestDirection = _agent.velocity.normalized;
    }

    public Vector3 GetDirection()
    {
        return _latestDirection;
    }
}