using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    PlayerController _playerController;
    bool _isRotating;
    LevelRotation _levelRotation;

    // Start is called before the first frame update
    void Start()
    {
        _playerController = GetComponentInParent<PlayerController>();
        _levelRotation = FindObjectOfType<LevelRotation>();
        _levelRotation.OnRotationStart += IsRotating;
        _levelRotation.OnRotationEnd += IsNotRotating;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isRotating)
        {
            LookDirection(_playerController._xMov);
        }
    }
    void LookDirection(float XMov)
    {
        if (XMov == 0)
        {
            gameObject.transform.LookAt(transform.position + Vector3.back);
        }
        if (XMov > 0)
        {
            gameObject.transform.LookAt(transform.position + Vector3.right);
        }
        if (XMov < 0)
        {
            gameObject.transform.LookAt(transform.position + Vector3.left);
        }
    }
    void IsRotating()
    {
        _isRotating = true;
        _playerController._isRotating = true;
        _playerController._velocity = Vector3.zero;

    }
    void IsNotRotating()
    {
        _playerController._groundCheckLeft.transform.localPosition = FlipObject(_playerController._groundCheckLeft);
        _playerController._groundCheckRight.transform.localPosition = FlipObject(_playerController._groundCheckRight);
        _playerController._isRotating = false;


        //flip du mesh
        _isRotating = false;
        Vector3 _flip = transform.localPosition;
        _flip.y = -_flip.y;
        transform.localPosition = _flip;
    }

    Vector3 FlipObject(GameObject go)
    {
        Vector3 _flip = go.transform.localPosition;
        _flip.y = -_flip.y;
        return _flip;
    }
}
