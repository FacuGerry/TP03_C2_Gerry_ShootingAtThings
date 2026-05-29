using UnityEngine;

public abstract class EnemyStates
{
    public StateTypeEnemy state;
    protected EnemyClasses _enemyClass;
    protected Animator _anim;
    protected Rigidbody _rb;
    protected NpcController _controller;

    protected static readonly int _state = Animator.StringToHash("State");

    public virtual void Initialize(Animator animator, Rigidbody rigidbody, NpcController controller)
    {
        _anim = animator;
        _rb = rigidbody;
        _controller = controller;
    }

    public virtual void OnEnter()
    {
        Debug.Log("Enter to " + state);
    }

    public virtual void OnUpdate() { }

    public virtual void OnExit()
    {
        Debug.Log("Exit from " + state);
    }
}
