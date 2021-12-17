using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamTriggerExit : MonoBehaviour
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
        
        if (other.CompareTag("PickableSphere") /*&& _isTriggered == true*/)
        {
            Debug.Log("UnTriggered");
            _playerController.canMove = true;
            _playerCamera._playerTransform = GameObject.FindWithTag("Player").transform;
            _playerCamera._cameraMoveSpeed = 10.0f;
        }
    }
    
}
