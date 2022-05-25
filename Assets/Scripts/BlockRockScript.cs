using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockRockScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PickableSphere"))
        {
            _rockTranform = other.transform;
            FindObjectOfType<LevelRotation>().OnRotationStart += MoveRock;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PickableSphere"))
        {
            _rockTranform = null;
            FindObjectOfType<LevelRotation>().OnRotationStart -= MoveRock;
        }
    }

    private void MoveRock()
    {
        _rockTranform.position = new Vector3(10.492f, 1.429f, -0.5f);
    }

    private Transform _rockTranform;
}
