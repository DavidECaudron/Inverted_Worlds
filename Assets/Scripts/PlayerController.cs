using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] AudioClip[] _footstepSounds;
    [SerializeField] AudioClip _grabSound;
    [SerializeField] CharacterController _characterController;
    [SerializeField] float _moveSpeed;
    [SerializeField] LayerMask _groundMask;
    public GameObject _groundCheckLeft;
    public GameObject _groundCheckRight;

    [HideInInspector] public bool _isRotating;
    [HideInInspector] public bool CarryObject = false;
    [HideInInspector] public bool canMove = false;
    [HideInInspector] public bool _canMoveRight = true;
    [HideInInspector] public bool _canMoveLeft = true;
    [HideInInspector] public float _xMov;
    [HideInInspector] public Vector3 _velocity;

    [SerializeField] private Animator _animator;
    private string _animatorMoveSpeedParamater = "MoveSpeed";
    private string _animatorGrabParamater = "Grab";
    private string _animatorTrigGrabParamater = "TrigGrab";

    private Rigidbody _platformRb;
    PickableObject _pickableObject;

    public bool CameraGrounded;
    bool _isPickable;

    private float _remainingTimeForNextFootstepSound = 0;

    private void Start()
    {
        _pickableObject = FindObjectOfType<PickableObject>();
        LevelLoader.instance.OnLoadLevelCompleted += EnableMove;
        LevelLoader.instance.OnStartLoadLevel += DisableVoid;

#if UNITY_EDITOR
        canMove = true;
#endif
    }

    void Update()
    {
        _isPickable = _pickableObject != null ? _pickableObject.IsObjectPickable() : false;
        if (!canMove) return;
        GetPlayerInput();
        MovePlayer();
        Grab();

        if (!_isRotating)
        {
            Vector3 _forceZ = new Vector3(transform.position.x, transform.position.y, -0.5f);
            transform.position = _forceZ;
        }
    }
    private void EnableMove()
    {
        canMove = true;
    }

    private void DisableVoid()
    {
        canMove = false;
    }

    public bool IsGrounded()
    {
        return _characterController.isGrounded;
    }

    public void TriggerAnimation(string triggerName)
    {
        _animator.SetTrigger(triggerName);
    }

    private void GetPlayerInput()
    {
        _velocity = Vector3.zero;

        if (_isRotating) return;
        if (!IsGrounded()) return;

        _xMov = Input.GetAxisRaw("Horizontal");
        if (Input.GetKey(KeyCode.E))
        { 
            if (!_canMoveLeft && _xMov < 0) return;
            if (!_canMoveRight && _xMov > 0) return;
        }

        Vector3 moveHorizontal = transform.right * _xMov;

        _velocity = (moveHorizontal).normalized * _moveSpeed;

        Animate(moveHorizontal);

        if(_xMov != 0)
        {
            _remainingTimeForNextFootstepSound -= Time.deltaTime;

            if (_remainingTimeForNextFootstepSound <= 0)
            {
                AudioClip clip = _footstepSounds[Random.Range(0, _footstepSounds.Length - 1)];
                _remainingTimeForNextFootstepSound = clip.length;
                AudioManager.instance.PlayClipAt(clip, transform.position);
            }
        }

    }

    private void MovePlayer()
    {
        if (_isRotating) return;

        Vector3 _otherVelocity = _platformRb != null ? _platformRb.velocity : Vector3.zero;

        if (!IsGrounded())
        {
            _characterController.Move(new Vector3(0f, -9.81f * Time.deltaTime, 0f));
            return;
        }

        _characterController.SimpleMove(_velocity + _otherVelocity);
    }

    void Animate(Vector3 direction)
    {
        _animator.SetFloat(_animatorMoveSpeedParamater, Mathf.Abs(direction.normalized.x));
    }

    void Grab()
    {
        if (Input.GetKeyDown(KeyCode.E) && _isPickable)
        {
            AudioManager.instance.PlayClipAt(_grabSound, transform.position);
            _animator.SetBool(_animatorGrabParamater, true);
            _animator.SetTrigger(_animatorTrigGrabParamater);
        }
        else
        {
            if (Input.GetKeyUp(KeyCode.E) && _isPickable)
            {
                _animator.SetBool(_animatorGrabParamater, false);
                _animator.Play("Idle");
            }
        }
    }


    public bool PlayerIsGrounded()
    {
        bool leftGround = Physics.CheckSphere(_groundCheckLeft.transform.position, .1f, _groundMask);
        bool rightGround = Physics.CheckSphere(_groundCheckRight.transform.position, .1f, _groundMask);

        return leftGround && rightGround;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(_groundCheckLeft.transform.position, .1f);
        Gizmos.DrawWireSphere(_groundCheckRight.transform.position, .1f);
    }

    private void OnTriggerEnter(Collider other)
    {
        InteractableObject interact = other.gameObject.GetComponent<InteractableObject>();
        if(interact != null)
        {
            interact.InstantiateUI(transform);
            return;
        }


        MovableObject moveObj = other.gameObject.GetComponent<MovableObject>();
        if (moveObj != null)
        {
            _platformRb = moveObj.GetComponent<Rigidbody>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        InteractableObject interact = other.gameObject.GetComponent<InteractableObject>();
        if (interact != null)
        {
            interact.DestroyInstance();
            return;
        }

        MovableObject moveObj = other.gameObject.GetComponent<MovableObject>();
        if (moveObj != null)
        {
            _platformRb = null;
        }
    }
}
