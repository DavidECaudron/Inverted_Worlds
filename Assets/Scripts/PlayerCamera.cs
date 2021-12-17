using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private float _cameraUp = 10.0f;
    [SerializeField] private float _cameraDown = 5.0f;
    [SerializeField] public float _cameraMoveSpeed = 10f;

    public Transform _playerTransform;
    private PlayerController _playerController;
    private Vector3 _tempPos;
    Camera _playerCamera;

    private Vector3 _pivotPos;
    private Vector3 _levelPos;

    private Vector3 _topPos;
    private Vector3 _botPos;
    private Vector3 _leftPos;
    private Vector3 _rightPos;

    // Start is called before the first frame update
    void Start()
    {
        _playerController = GameObject.FindObjectOfType<PlayerController>();
        _playerTransform = GameObject.FindWithTag("Player").transform;

        LevelRotation _levelRota = FindObjectOfType<LevelRotation>();
        _playerCamera = GetComponent<Camera>();

        _levelPos = _levelRota.GetLevelPos();
        _pivotPos = _levelRota.GetPivotPos();

        _botPos = _levelPos;
        _topPos = _botPos + ((_botPos + _pivotPos) * 2);

        _leftPos = _levelPos;
        _rightPos = _botPos + ((_leftPos + _pivotPos) * 2);

        _lookMovement = transform.position;
        _lookMovement.x = _playerTransform.position.x;
        _lookMovement.y = _playerTransform.position.y;
    }

    private bool _cameraMoveTop;
    private bool _cameraMoveBot;
    private Vector3 _lookMovement;

    private void CameraLookUp()
    {
        _lookMovement.y += _cameraMoveSpeed * Time.deltaTime;
        _lookMovement.y = Mathf.Clamp(_lookMovement.y, _playerTransform.position.y - _cameraDown, _playerTransform.position.y + _cameraUp);
    }

    private void CameraLookDown()
    {
        _lookMovement.y -= _cameraMoveSpeed * Time.deltaTime;
        _lookMovement.y = Mathf.Clamp(_lookMovement.y, _playerTransform.position.y - _cameraDown, _playerTransform.position.y + _cameraUp);
    }


    void LateUpdate()
    {
        if (!_playerTransform)
        {
            return;
        }

        _tempPos = transform.position;
        _tempPos.x = _playerTransform.position.x;
        _tempPos.y = _playerTransform.position.y;

        if (Input.GetKey(KeyCode.DownArrow) && !_cameraMoveTop)
        {
            _cameraMoveBot = true;
            _playerController.BoolAnimation("LookDown", true);
        }
        if (Input.GetKeyUp(KeyCode.DownArrow) && !_cameraMoveTop)
        {
            _cameraMoveBot = false;
            _playerController.BoolAnimation("LookDown", false);
        }

        if (Input.GetKey(KeyCode.UpArrow) && !_cameraMoveBot)
        {
            _cameraMoveTop = true;
            _playerController.BoolAnimation("LookUp", true);
        }
        if (Input.GetKeyUp(KeyCode.UpArrow) && !_cameraMoveBot)
        {
            _cameraMoveTop = false;
            _playerController.BoolAnimation("LookUp", false);
        }

        if (_cameraMoveTop)
        {
            CameraLookUp();
        }
        else if (_cameraMoveBot)
        {
            CameraLookDown();
        }
        else
        {
            //Reset camera vers position du joueur
            float distance = _lookMovement.y - _playerTransform.position.y;

            if (distance > 0 && !(distance < 0.05f))
            {
                CameraLookDown();
            }
            else if (distance < 0 && !(distance > -0.05f))
            {
                CameraLookUp();
            }
        }

        if (!_playerController.PlayerIsGrounded())
        {
            _lookMovement.y = _playerTransform.position.y;
        }



        CheckCameraWithLevelLimit();

        _tempPos.y = _lookMovement.y;
        transform.position = _tempPos;
    }



    private void CheckCameraWithLevelLimit()
    {
        //Empecher camera de sortir de la map

        if (_lookMovement.y < _botPos.y + _playerCamera.orthographicSize)
        {
            _lookMovement.y = _botPos.y + _playerCamera.orthographicSize;
        }
        if (_lookMovement.y > _topPos.y - _playerCamera.orthographicSize)
        {
            _lookMovement.y = _topPos.y - _playerCamera.orthographicSize;
        }

        if (_tempPos.x < _leftPos.x + (_playerCamera.orthographicSize * 1.8f))
        {
            _tempPos.x = _leftPos.x + (_playerCamera.orthographicSize * 1.8f);
        }
        if (_tempPos.x > _rightPos.x - (_playerCamera.orthographicSize * 1.8f))
        {
            _tempPos.x = _rightPos.x - (_playerCamera.orthographicSize * 1.8f);
        }
    }
}
