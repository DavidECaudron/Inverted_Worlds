using System.Collections;
using UnityEngine;

public class LevelRotation : MonoBehaviour
{
    [SerializeField] private GameObject _pivot;
    [SerializeField] private float _rotateTime;
    [SerializeField] private float _rotationCooldown;
    [HideInInspector] private PlayerController _playerController;

    private bool _isTop = false;
    private bool _canRotate = true;

    private float _degres;

    private Vector3 _botPos;
    private Vector3 _topPos;

    public delegate void RotationChanged();
    public RotationChanged OnRotationStart;
    public RotationChanged OnRotationEnd;

    void Start()
    {
        _degres = 180 / _rotateTime;
        _botPos = transform.position;
        _topPos = _botPos + ((_botPos + _pivot.transform.position) * 2);
        _topPos.x = 0.0f;
        _playerController = gameObject.GetComponentInChildren<PlayerController>();
    }

    void Update()
    {
        Rotation();
    }

    void Rotation()
    {

        if (Input.GetKeyDown(KeyCode.A))
        {
            StartCoroutine(RotateCoroutine());
        }

            if (Input.GetKeyDown(KeyCode.A) && _canRotate && _playerController.PlayerIsGrounded()) 
        {
            StartCoroutine(RotateCoroutine());
        }
    }
    IEnumerator RotateCoroutine()
    {
        _canRotate = false;
        _isTop = !_isTop;

        OnRotationStart?.Invoke();
        float i = 0;
        while (i < 180.0f)
        {
            transform.RotateAround(_pivot.transform.position, Vector3.right, _degres*Time.deltaTime);
            i += _degres*Time.deltaTime;
            yield return new WaitForSeconds(_rotateTime/_degres*Time.deltaTime);
        }

        if (_isTop)
        {
            transform.position = _topPos;
            transform.rotation = Quaternion.Euler(180.0f, 0.0f, 0.0f);
        }
        else
        {
            transform.position = _botPos;
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        }

        OnRotationEnd?.Invoke();

        StartCoroutine(RotationCooldown());
    }


    IEnumerator RotationCooldown()
    {
        yield return new WaitForSeconds(_rotationCooldown);
        _canRotate = true;
    }

    public Vector3 GetLevelPos()
    {
        return transform.position;
    }

    public Vector3 GetPivotPos()
    {
        return _pivot.transform.position;
    }
}
