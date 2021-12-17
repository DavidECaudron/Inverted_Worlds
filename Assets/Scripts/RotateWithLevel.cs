using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWithLevel : MonoBehaviour
{
    private LevelRotation _lr;
    void Start()
    {
        _lr = FindObjectOfType<LevelRotation>();
        _lr.OnRotationEnd += Rotate;
    }

    void Rotate()
    {
        transform.rotation = _lr.transform.rotation;
    }

    private void OnDestroy()
    {
        _lr.OnRotationEnd -= Rotate;
    }
}
