using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody), typeof(EnemyShoot))]

public class NpcController : MonoBehaviour
{
    [SerializeField] private EnemyDataSO _data;
    [SerializeField] private WaypointsDataSO _waypoints;
    [SerializeField] private Animator _anim;

    // PLAYER
    public Transform Player { get; private set; }
    public Rigidbody PlayerRb { get; private set; }

    // ENEMY
    public EnemyDataSO Data { get; private set; }
    public WaypointsDataSO Waypoints { get; private set; }
    public EnemyAttackType AttackType => Data.attackType;
    public EnemyClasses EnemyClass => Data.enemyClass;
    public bool CanMove => Data.canMove;
    public EnemyShoot Shoot { get; private set; }
    public NavMeshAgent Agent { get; private set; }
    public EnemyFollowPlayer FollowPlayer { get; private set; }

    private EnemyHealthSystem _hs;
    private List<EnemyStates> _states = new();
    private EnemyStates _currentState;
    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        Shoot = GetComponent<EnemyShoot>();
        Data = _data;
        Waypoints = _waypoints;

        _hs = GetComponent<EnemyHealthSystem>();
        if (_hs == null)
            gameObject.AddComponent<EnemyHealthSystem>();
    }

    private void OnEnable()
    {
        if (_hs)
            _hs.OnEnemyDie += OnEnemyDie_ChangeState;
    }

    private void Update()
    {
        _currentState.OnUpdate();
    }

    private void OnDisable()
    {
        if (_hs)
            _hs.OnEnemyDie -= OnEnemyDie_ChangeState;
    }

    public void Initialize(Transform player)
    {
        Player = player;
        PlayerRb = Player.gameObject.GetComponent<Rigidbody>();

        if (CanMove)
        {
            Agent = GetComponent<NavMeshAgent>();
            if (Agent == null)
                gameObject.AddComponent<NavMeshAgent>();

            Agent.enabled = true;

            FollowPlayer = GetComponent<EnemyFollowPlayer>();
            if (FollowPlayer == null)
                gameObject.AddComponent<EnemyFollowPlayer>();
        }

        SetStatesForFSM();
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
        _states.Add(new EnemyStateDie());

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

    public bool CheckForNearPlayerToAttack()
    {
        if (Player.gameObject.activeInHierarchy && Mathf.Abs(Vector3.Distance(transform.position, Player.transform.position)) < Data.distanceToShoot)
            return true;

        return false;
    }

    public bool CheckForNearPlayerToFollow()
    {
        if (Player.gameObject.activeInHierarchy && Mathf.Abs(Vector3.Distance(transform.position, Player.transform.position)) < Data.distanceToFollow)
            return true;

        return false;
    }

    private void OnEnemyDie_ChangeState() => SwitchState(FindState(StateTypeEnemy.Die));

    public Animator GetAnim() => _anim;
}