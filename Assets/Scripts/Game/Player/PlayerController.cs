using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(PlayerShoot))]

public class PlayerController : MonoBehaviour
{
    // INPUTS
    private readonly string _inputX = "Input X";
    private readonly string _inputY = "Input Y";
    private readonly string _hor = "Horizontal";
    private readonly string _ver = "Vertical";
    private readonly string _crouch = "Crouch";
    private readonly string _jump = "Jump";
    private readonly string _shoot = "Shoot";
    private readonly string _walk = "Walk";

    [SerializeField] private PlayerDataSO _data;
    [SerializeField] private Animator _anim;

    private Rigidbody _rb;

    public PlayerShoot Shoot { get; private set; }

    private List<PlayerStates> _states = new();
    private PlayerStates _currentState;

    private Vector3 _direction = Vector3.zero;

    private float _speedChanger = 1f;

    private bool _isAlive = true;
    public Vector2 MoveInput { get; private set; }
    public bool IsWalking { get; private set; }
    public bool WantsCrouch { get; private set; }
    public bool WantsShoot { get; private set; }
    public bool WantsJump { get; private set; }
    public bool IsAlive => _isAlive;
    public bool IsCrouching { get; private set; }
    public bool IsOnFloor { get; private set; }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        Shoot = GetComponent<PlayerShoot>();

        _states.Clear();

        _states.Add(new StateIdle());
        _states.Add(new StateWalk());
        _states.Add(new StateShoot()); // aparte
        _states.Add(new StateJump());
        _states.Add(new StateStandUpToCrouch());
        _states.Add(new StateCrouchToStandUp());
        _states.Add(new StateCrouchIdle());
        _states.Add(new StateCrouchMove());
        _states.Add(new StateCrouchShoot());
        _states.Add(new StateDie());

        foreach (PlayerStates state in _states)
            state.Initialize(_anim, _rb, this);
    }

    private void Start()
    {
        _currentState = FindState(StateTypePlayer.Idle);
        _currentState.OnEnter();
    }

    private void Update()
    {
        MoveInput = new Vector2(Input.GetAxisRaw(_hor), Input.GetAxisRaw(_ver));

        IsWalking = Input.GetAxisRaw(_walk) != 0f;

        WantsCrouch = Input.GetAxisRaw(_crouch) != 0f;
        WantsJump = Input.GetAxisRaw(_jump) != 0f;

        WantsShoot = Input.GetAxisRaw(_shoot) != 0f;

        _currentState.OnUpdate();

        CalculatePlayerSpeed();
        UpdateAnimatorInputs();
    }

    private void FixedUpdate()
    {
        _rb.AddForce(_direction * (_data.movementSpeed * _speedChanger), ForceMode.Force);
    }

    private void UpdateAnimatorInputs()
    {
        _anim.SetFloat(_inputX, MoveInput.x);
        _anim.SetFloat(_inputY, MoveInput.y);
    }

    public void CalculatePlayerSpeed()
    {
        Vector3 direction = transform.forward * MoveInput.y + transform.right * MoveInput.x;
        _direction = direction.normalized;

        if (IsCrouching)
            _speedChanger = 0.75f;
        else
        {
            if (IsWalking)
                _speedChanger = 1f;
            else
                _speedChanger = 1.5f;
        }
    }

    public void Jump() => _rb.AddForce(Vector3.up * _data.jumpForce, ForceMode.Impulse);

    public void SwitchState(PlayerStates newState)
    {
        if (_currentState == newState)
            return;

        _currentState.OnExit();
        _currentState = newState;
        _currentState.OnEnter();
    }

    public PlayerStates FindState(StateTypePlayer stateToFind)
    {
        foreach (PlayerStates state in _states)
            if (state.state == stateToFind)
                return state;

        return null;
    }

    public void ChangeCrouching(bool isCrouching) => IsCrouching = isCrouching;
}