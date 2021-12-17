using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezRotation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PickableSphere"))
        {
            Rigidbody _rb = other.GetComponent<Rigidbody>();
            _rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotationZ | 
                              RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationY |
                              RigidbodyConstraints.FreezeRotationX;

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PickableSphere"))
        {
            Rigidbody _rb = other.GetComponent<Rigidbody>();
            _rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationY;
            //_rb.constraints = RigidbodyConstraints.FreezeRotationZ;

        }
    }
}
