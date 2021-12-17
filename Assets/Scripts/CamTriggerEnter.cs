using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamTriggerEnter : MonoBehaviour
{
    PlayerCamera _playerCamera;
    PlayerController _playerController;
    //bool _isTriggered = false;

    // Start is called before the first frame update
    void Start()
    {
        _playerCamera = GameObject.FindObjectOfType<PlayerCamera>();
        _playerController = GameObject.FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PickableSphere")/* && _isTriggered == false*/)
        {
            Debug.Log("Triggered");
            //_isTriggered = true;
            _playerController.canMove = false;
            _playerCamera._playerTransform = other.transform;
            _playerCamera._cameraMoveSpeed = 20.0f;
       
        }
       
    }
    
}
