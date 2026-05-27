using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerDataSO _data;

    private Animator _anim;
    private Rigidbody _rb;

    public PlayerShoot Shoot { get; private set; }

    private List<PlayerStates> _states = new();
    private PlayerStates _currentState;

    private Vector3 _direction = Vector3.zero;

    private float _speedChanger = 1f;

    private Vector2 _moveInput = Vector2.zero;

    private bool _isAlive = true;
    private bool _isWalking = false;
    private bool _isCrouching = false;
    private bool IsCrouching => _isCrouching;

    private bool _wantsCrouch = false;
    private bool _wantsShoot = false;
    private bool _wantsJump = false;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
        Shoot = GetComponent<PlayerShoot>();

        _states.Clear();

        // IDLE
        _states.Add(new StateIdle());

        // RUN
        _states.Add(new StateRunForward());
        _states.Add(new StateRunLeft());
        _states.Add(new StateRunBackwards());
        _states.Add(new StateRunRight());

        // WALK
        _states.Add(new StateWalkForward());
        _states.Add(new StateWalkLeft());
        _states.Add(new StateWalkBackwards());
        _states.Add(new StateWalkRight());

        // SHOOT
        _states.Add(new StateShoot());

        // JUMP
        _states.Add(new StateJump());

        // TOGGLE CROUCH
        _states.Add(new StateStandUpToCrouch());
        _states.Add(new StateCrouchToStandUp());

        // CROUCH MOVEMENT
        _states.Add(new StateCrouchIdle());
        _states.Add(new StateCrouchWalkForward());
        _states.Add(new StateCrouchWalkLeft());
        _states.Add(new StateCrouchWalkBackwards());
        _states.Add(new StateCrouchWalkRight());
        _states.Add(new StateCrouchShoot());

        // DIE
        _states.Add(new StateDie());

        foreach (PlayerStates state in _states)
            state.Initialize(_anim, _rb, this);

        _currentState = FindState(StateTypePlayer.Idle);
        _currentState.OnEnter();
    }

    private void Update()
    {
        _moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        _isWalking = Input.GetKey(KeyCode.LeftShift);

        _wantsCrouch = Input.GetKey(KeyCode.LeftControl);
        _wantsShoot = Input.GetMouseButton(0);
        _wantsJump = Input.GetKeyDown(KeyCode.Space);

        _currentState.OnUpdate();
        EvaluateTransitions();
        MovePlayer();
    }

    private void FixedUpdate()
    {
        _rb.AddForce(_data.movementSpeed * _speedChanger * _direction, ForceMode.Force);
    }

    public void MovePlayer()
    {
        Vector3 direction = transform.forward * _moveInput.y + transform.right * _moveInput.x;
        direction.Normalize();

        _direction = direction;

        if (IsCrouching)
            _speedChanger = 0.5f;
        else
        {
            if (_isWalking)
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

    public StateTypePlayer GetMovementState()
    {
        bool moving = _moveInput.sqrMagnitude > 0.01f;

        if (!moving)
            return IsCrouching ? StateTypePlayer.CrouchIdle : StateTypePlayer.Idle;

        bool walking = _isWalking && !IsCrouching;

        if (_moveInput.y > 0)
        {
            if (IsCrouching)
                return StateTypePlayer.CrouchWalkForward;

            return walking ? StateTypePlayer.WalkForward : StateTypePlayer.RunForward;
        }

        if (_moveInput.y < 0)
        {
            if (IsCrouching)
                return StateTypePlayer.CrouchWalkBackwards;

            return walking ? StateTypePlayer.WalkBackwards : StateTypePlayer.RunBackwards;
        }

        if (_moveInput.x > 0)
        {
            if (IsCrouching)
                return StateTypePlayer.CrouchWalkRight;

            return walking ? StateTypePlayer.WalkRight : StateTypePlayer.RunRight;
        }

        if (_moveInput.x < 0)
        {
            if (IsCrouching)
                return StateTypePlayer.CrouchWalkLeft;

            return walking ? StateTypePlayer.WalkLeft : StateTypePlayer.RunLeft;
        }

        return StateTypePlayer.Idle;
    }

    private void EvaluateTransitions()
    {
        if (_currentState.state == StateTypePlayer.StandUpToCrouch || _currentState.state == StateTypePlayer.CrouchToStandUp
            || _currentState.state == StateTypePlayer.Jump || _currentState.state == StateTypePlayer.Die)
            return;

        if (!_isAlive)
        {
            SwitchState(FindState(StateTypePlayer.Die));
            return;
        }

        if (_wantsJump)
        {
            SwitchState(FindState(StateTypePlayer.Jump));
            return;
        }

        if (_wantsShoot)
        {
            SwitchState(FindState(StateTypePlayer.Shoot));
            return;
        }

        if (_wantsCrouch)
        {
            if (IsCrouching)
                SwitchState(FindState(StateTypePlayer.CrouchToStandUp));
            else
                SwitchState(FindState(StateTypePlayer.StandUpToCrouch));

            return;
        }

        StateTypePlayer movementState = GetMovementState();

        if (movementState != _currentState.state)
        {
            SwitchState(FindState(movementState));
        }
    }

    public void ChangeCrouching(bool isCrouching) => _isCrouching = isCrouching;
}