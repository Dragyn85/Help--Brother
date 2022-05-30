using UnityEngine;

internal class MovableObject : MonoBehaviour
{
    private Animator _animator;
    private int _moveHash = Animator.StringToHash("Move");
    public void Move()
    {
        _animator.SetTrigger(_moveHash);
    }
}