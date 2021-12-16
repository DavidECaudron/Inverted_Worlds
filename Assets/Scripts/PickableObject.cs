using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableObject : MonoBehaviour
{
    [HideInInspector]
    GameObject _player;
    [HideInInspector]
    PlayerController _playerController;
    Transform _lvlTransform;

    [HideInInspector] public int _limit = 0;

    // Start is called before the first frame update
    void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerController = _player.GetComponent<PlayerController>();
        _lvlTransform = transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        ManagePickObject();
        //Debug.Log("Limit = "+_limit+"Xmov = "+_playerController._xMov);
    }
    private void ManagePickObject()
    {
        if ( Input.GetKeyDown(KeyCode.E))
        {
            PickObject();
        }
        else if (Input.GetKeyUp(KeyCode.E))
        {
            DropObject();
        }
    }
    float CheckDistanceFromPlayer()
    {
        float distance = Vector3.Distance(gameObject.transform.position, _player.transform.position);
        return distance;
    }

    bool IsObjectPickable(float Distance)
    {
        if (Distance <= (transform.localScale.x + 0.1))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsObjectPickable()
    {
        return IsObjectPickable(CheckDistanceFromPlayer());
    }

    void PickObject()
    {
        if (IsObjectPickable(CheckDistanceFromPlayer()) && !_playerController.CarryObject)
        {
            //GetComponent<Rigidbody>().isKinematic = true;

            //if (_limit == -1 && _playerController._xMov < 0)
            //{
            //    DropObject();
            //    return;
            //}
            //if (_limit == 1 && _playerController._xMov > 0)
            //{
            //    DropObject();
            //    return;
            //}
            if (_limit == 1)
            {
                _playerController._canMoveRight = false;
            }
            if (_limit == -1)
            {
                _playerController._canMoveLeft = false;
            }
            transform.parent = _player.transform;

            //_cubeMove.x = 1000000000.0f;
            //_rb.AddForce(_cubeMove);
            _playerController.CarryObject = true;
            //Debug.Log("Picked");
        }

    }
    void DropObject()
    {
        //GetComponent<Rigidbody>().isKinematic = false;
        _playerController.CarryObject = false;
        _playerController._canMoveLeft = true;
        _playerController._canMoveRight = true;
        transform.parent = _lvlTransform;

        //Debug.Log("Droped");
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
    //    {
    //        DropObject();
    //        Debug.Log("Crash");
    //        if (_playerController._xMov < 0)
    //        {
    //            _limit = -1;

    //        }
    //        else if (_playerController._xMov > 0)
    //        {
    //            _limit = 1;
    //        }
    //    }
    //}
    //private void OnCollisionExit(Collision collision)
    //{
    //    if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
    //    {
    //        _limit = 0;
    //        Debug.Log("Exit");
    //    }
    //}
    //private void OnCollisionStay(Collision collision)
    //{
    //    if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
    //    {
    //        if (_limit == -1 && _playerController._xMov < 0)
    //        {
    //            DropObject();
    //            return;
    //        }
    //        if (_limit == 1 && _playerController._xMov > 0)
    //        {
    //            DropObject();
    //            return;
    //        }
    //    }
    //}

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("WallLimitR"))
        {
            _playerController._canMoveRight = false ;
            _limit = 1;
        }
        else if (other.CompareTag("WallLimitL"))
        {
            _playerController._canMoveLeft = false;
            _limit = -1;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("WallLimitR"))
        {
            _playerController._canMoveRight = true;
            _limit = 0;
        }
        else if (other.CompareTag("WallLimitL"))
        {
            _playerController._canMoveLeft = true;
            _limit = 0;
        }
    }
}
