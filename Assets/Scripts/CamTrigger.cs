using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamTrigger : MonoBehaviour
{
    PlayerCamera _playerCamera;
    PlayerController _playerController;
    bool _isTriggered = false;

    // Start is called before the first frame update
    void Start()
    {
        _playerCamera = GetComponent<PlayerCamera>();
        _playerController = GameObject.FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PickableSphere") && _isTriggered == false)
        {
            _isTriggered = true;
            _playerController.canMove = false;
            _playerCamera._playerTransform = GameObject.FindWithTag("PickableSphere").transform;
        }
        if (other.CompareTag("PickableSphere") && _isTriggered == true)
        {
            _isTriggered = false;
            _playerController.canMove = true;
            _playerCamera._playerTransform = GameObject.FindWithTag("Player").transform;
        }
    }
}
