using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class WalkRandomly : MonoBehaviour
{
    [SerializeField] private GuidedBrother _brother;

    [SerializeField] private float _timeBetweenWalks;
    private bool _gotDestinationRecently;
    private float _timer;
    [SerializeField] float _wanderRadius = 2f;
    private bool _reciveNewOrigin = true;
    
    

    private void OnEnable()
    {
        _brother.RecivedNewDestination += HandleMoved;
    }

    private void OnDisable()
    {
        _brother.RecivedNewDestination -= HandleMoved;
    }

    private void HandleMoved()
    {
        _gotDestinationRecently = true;
        _timer = 0;
    }
    

    

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= _timeBetweenWalks)
        {
            var randomPos = Random.insideUnitSphere * _wanderRadius;
            randomPos += _brother.BasePos;
            
            NavMeshHit navMeshHit;
            do
            {
                NavMesh.SamplePosition(randomPos, out navMeshHit, _wanderRadius, -1);    
            } while (!navMeshHit.hit);
            
            _brother.SetDestionation(navMeshHit.position, false);
            _timer = 0;
        }


    }
}