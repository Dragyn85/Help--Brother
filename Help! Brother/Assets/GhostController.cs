using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    [SerializeField] private Vector2 _maxMovingBounds;
    [SerializeField] private Vector2 _minMovingBounds;
    [SerializeField] private float _speed= 2f;
    [SerializeField] private float _accelerationTime =1f;
    private int _movingTimer;
    


    // Update is called once per frame
    void Update()
    {
        var horizontal = Input.GetAxis("Horizontal") *Time.deltaTime * _speed;
        var vertical = Input.GetAxis("Vertical") *Time.deltaTime * _speed;
        var direction = new Vector3(horizontal, 0, vertical);
        var magnitude = direction.magnitude;
        if (magnitude <= 0.0005f)
            _movingTimer = 0;
        var multiplier = Mathf.Lerp(0.1f, 1f, _movingTimer / _accelerationTime);

        var newPosition = new Vector3(
            transform.position.x + horizontal*multiplier, 
            transform.position.y,
            transform.position.z + vertical*multiplier);

        //Debug.Log(Vector3.Dot(transform.forward,Vector3.left) + "is the DOT product of the vectors");
        
        newPosition.x = Mathf.Clamp(newPosition.x, _minMovingBounds.x, _maxMovingBounds.x);
        newPosition.z = Mathf.Clamp(newPosition.z, _minMovingBounds.y, _maxMovingBounds.y);
        transform.position = newPosition;
        if (direction.magnitude != 0)
            transform.forward = direction.normalized;
            //transform.rotation = Quaternion.LookRotation(direction.normalized);
            
    }
}
