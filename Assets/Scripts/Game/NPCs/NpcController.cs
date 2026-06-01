using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(EnemyShoot), typeof(EnemyHealthSystem))]

public class NpcController : MonoBehaviour, IPooleable
{
    [SerializeField] private EnemyDataSO _data;
    [SerializeField] private Animator _anim;
    [SerializeField] private GameDataSO _gameData;
    
    private GameObject _player;
    public GameObject Player => _player;
    public float ThrowingHeight { get; private set; } = 5f;
    public float ThrowingDuration { get; private set; } = 5f;

    public EnemyAttackType AttackType => _data.attackType;
    public EnemyClasses EnemyClass => _data.enemyClass;
    public bool CanMove => _data.canMove;
    public bool IsAlive { get; private set; } = true;
    public bool IsActive { get; set; }

    public EnemyShoot Shoot { get; private set; }
    private EnemyHealthSystem _healthSystem;

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

    private void Start()
    {
        _player = _gameData.player;
    }

    private void Update()
    {
        _currentState.OnUpdate();
    }

    private void OnDisable()
    {
        _healthSystem.OnEnemyDie -= OnEnemyDie_ChangeState;
    }

    public void Activate()
    {
        IsActive = true;
        gameObject.SetActive(IsActive);
    }

    public void DeActivate()
    {
        IsActive = false;
        gameObject.SetActive(IsActive);
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