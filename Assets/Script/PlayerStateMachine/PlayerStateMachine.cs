using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    CharacterController _characterController;
    Animator _animator;
    Camera _cam;
    [SerializeField] private Joystick _joystick;

    [Header("Input")]
    bool _isRunInput;
    [HideInInspector]
    public static bool _isMoving;
    bool _isInteracting;

    Vector3 _currentMovement;
    public Vector3 _desiredMoveDirection;

    public float m_speed;
    public float _rotationSpeed = 1.0f;
    float groundedGravity = -.05f;
    float gravity = -7f;

    [Header("Animation")]
    float a_speed;
    [Range(0, 1f)]
    public float _startAnimTime = 0.3f;
    [Range(0, 1f)]
    public float _stopAnimTime = 0f;
    public float allowPlayerRotation = 0.1f;

    PlayerBaseState _currentState; 
    PlayerStateFactory _states;

    [Header("Tracker")]
    //Tracker
    public Transform _targetTracker;
    Vector3 resetPos;
    [SerializeField]
    private float _smoothTime = .5f;
    private Vector3 _velocity = Vector3.zero;

    #region Singleton
    public Joystick Joystick { get { return _joystick; } }
    public CharacterController CharacterController { get { return _characterController;} }
    public Vector3 DesiredMoveDirection { get { return _desiredMoveDirection; } }
    public bool IsMoving { get { return _isMoving; }  }
    public bool IsInteracting { get { return _isInteracting; } }
    public Animator Animator { get { return _animator; } }
    public float A_Speed { get { a_speed = new Vector2(_currentMovement.x, _currentMovement.z).sqrMagnitude; return a_speed; } }
    public float M_Speed { get { return m_speed; } set { m_speed = value; } }
    public float StartAnimTime { get { return _startAnimTime; }  set { _startAnimTime = value; } }
    public float StopAnimTime { get { return _stopAnimTime; } set { _stopAnimTime = value; } }
    public PlayerBaseState CurrentState { get { return _currentState; } set { _currentState = value; } }
    public bool IsRunInput { get { return _isRunInput; } }
    #endregion

    public void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        _cam = Camera.main;

        // state setup
        _states = new PlayerStateFactory(this);
        _currentState = _states.Grounded();
        _currentState.EnterState();
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        OnMovement();
        handleRotation();
        _currentState.UpdateStates();
        handleGravity();
    }

    void OnMovement()
    {
        _isMoving = _joystick.Horizontal != 0 || _joystick.Vertical != 0;

        var forward = _cam.transform.forward;
        var right = _cam.transform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        _desiredMoveDirection = forward * _joystick.Vertical + right * _joystick.Horizontal;
    }


    void handleRotation()
    {
        Vector3 positionToLookAt;

        positionToLookAt.x = _desiredMoveDirection.x;
        positionToLookAt.y = .0f;
        positionToLookAt.z = _desiredMoveDirection.z;

        Quaternion currentRotation = transform.rotation;


        if (_isMoving)
        {
            Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, _rotationSpeed * Time.deltaTime);

            //Ghost
            Vector3 ghostSpeed;

            ghostSpeed.x = _desiredMoveDirection.x + (_joystick.Horizontal * 2.2f);
            ghostSpeed.y = _desiredMoveDirection.y + .5f;
            ghostSpeed.z = _desiredMoveDirection.z + (_joystick.Vertical * 3.5f);

            var nextPos = transform.position + ghostSpeed;
            _targetTracker.transform.rotation = transform.rotation;
            _targetTracker.transform.position = Vector3.SmoothDamp(_targetTracker.transform.position, nextPos, ref _velocity, Time.fixedDeltaTime * (M_Speed * 1.5f));
        }
        else
            _targetTracker.transform.position = Vector3.SmoothDamp(_targetTracker.transform.position, transform.position, ref _velocity, _smoothTime);
    }


    void handleGravity()
    {
        if (_characterController.isGrounded)
        {
            _currentMovement.y = groundedGravity;
        }
        else
        {
            _currentMovement.y += gravity * Time.deltaTime;
            Vector3 falling = new Vector3(0f, _currentMovement.y,0f);
            _characterController.Move (falling);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out IInteractable interact))
        {
            interact.onEnter();
            _isInteracting = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out IInteractable interact))
        {
            interact.onExit();
            _isInteracting = false;
        }
    }

}
