using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(EnemyShoot), typeof(EnemyHealthSystem))]

public class NpcController : MonoBehaviour
{
    [SerializeField] private EnemyDataSO _data;
    [SerializeField] private Animator _anim;
    [SerializeField] private GameObject _player; // this should be received from a SO of the game settings

    public EnemyAttackType AttackType => _data.attackType;
    public EnemyClasses EnemyClass => _data.enemyClass;
    public bool CanMove => _data.canMove;
    public bool IsAlive { get; private set; } = true;

    public EnemyShoot Shoot { get; private set; }
    public EnemyHealthSystem _healthSystem;

    private List<EnemyStates> _states = new();
    private EnemyStates _currentState;

    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        Shoot = GetComponent<EnemyShoot>();
        _healthSystem = GetComponent<EnemyHealthSystem>();

        SetStatesForFSM();
    }

    private void OnEnable()
    {
        _healthSystem.OnEnemyDie += OnEnemyDie_ChangeState;
    }

    private void Update()
    {
        _currentState.OnUpdate();
    }

    private void OnDisable()
    {
        _healthSystem.OnEnemyDie -= OnEnemyDie_ChangeState;
    }

    private void SetStatesForFSM()
    {
        _states.Add(new EnemyStateIdle());

        if (CanMove)
        {
            _states.Add(new EnemyStateRoam());
            _states.Add(new EnemyStateFollow());
        }

        _states.Add(new EnemyStateAttack());
        // _states.Add(new EnemyStateDie());

        foreach (EnemyStates state in _states)
            state.Initialize(_anim, _rb, this);

        _currentState = FindState(StateTypeEnemy.Idle);
        _currentState.OnEnter();
    }

    public void SwitchState(EnemyStates newState)
    {
        if (_currentState == newState)
            return;

        _currentState.OnExit();
        _currentState = newState;
        _currentState.OnEnter();
    }

    public EnemyStates FindState(StateTypeEnemy stateToFind)
    {
        foreach (EnemyStates state in _states)
            if (state.state == stateToFind)
                return state;

        return null;
    }

    public bool CheckForNearPlayer() => Vector3.Distance(transform.position, _player.transform.position) < _data.distanceToShoot;

    private void OnEnemyDie_ChangeState() => IsAlive = false;
}