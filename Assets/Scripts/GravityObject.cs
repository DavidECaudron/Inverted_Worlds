using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GravityObject : MonoBehaviour
{
    private Rigidbody _rb;

    private bool _gravityDisable;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.useGravity = true;

        LevelRotation lr = FindObjectOfType<LevelRotation>();
        lr.OnRotationStart += DisableGravity;
        lr.OnRotationEnd += EnableGravity;
    }

    void EnableGravity()
    {
        if (_gravityDisable) return;
        _rb.useGravity = true;
    }

    void DisableGravity()
    {
        if (_gravityDisable) return;
        _rb.useGravity = false;
        _rb.velocity = Vector3.zero;
    }

    public void ForceDisableGravity()
    {
        _rb.useGravity = false;
        _gravityDisable = true;
    }

}
