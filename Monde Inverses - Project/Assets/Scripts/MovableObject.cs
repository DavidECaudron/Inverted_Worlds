using UnityEngine;

public class MovableObject : MonoBehaviour
{
    [SerializeField] float _moveSpeed;
    [SerializeField] float _distanceForNextPoint = .5f;
    [SerializeField] PlatformPath _path;
    [SerializeField] Rigidbody _rb;

    bool _canMove = true;
    Vector3 _nextPoint;

    void Start()
    {
        LevelRotation lr = FindObjectOfType<LevelRotation>();
        lr.OnRotationStart += DisableMove;
        lr.OnRotationEnd += EnableMove;

        _nextPoint = _path.GetNextPoint();
    }

    void FixedUpdate()
    {
        if (!_canMove) return;

        if (Vector3.Distance(transform.position, _nextPoint) <= _distanceForNextPoint)
        {
            _nextPoint = _path.GetNextPoint();
        }

        MovePlatform();
    }

    void MovePlatform()
    {
        Vector3 dir = _nextPoint - _rb.position;
        dir.Normalize();

        _rb.MovePosition(_rb.position + dir * Time.fixedDeltaTime * _moveSpeed);
    }

    void EnableMove()
    {
        _nextPoint = _path.GetActualPoint();

        _canMove = true;        
    }

    void DisableMove()
    {
        _canMove = false;
    }
}
