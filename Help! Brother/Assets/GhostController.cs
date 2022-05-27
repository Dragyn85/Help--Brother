using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    [SerializeField] private Vector2 _maxMovingBounds;
    [SerializeField] private Vector2 _minMovingBounds;
    

    // Update is called once per frame
    void Update()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        var direction = new Vector3(vertical, horizontal);

        var newPosition = new Vector3(
            transform.position.x + vertical, 
            transform.position.y,
            transform.position.z + horizontal);

        newPosition.x = Mathf.Clamp(newPosition.x, _minMovingBounds.x, _maxMovingBounds.x);
        newPosition.y = Mathf.Clamp(newPosition.z, _minMovingBounds.y, _maxMovingBounds.y);
        transform.position = newPosition;

    }
}
