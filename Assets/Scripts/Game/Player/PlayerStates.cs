using UnityEngine;

public abstract class PlayerStates
{
    public StateTypePlayer state;
    protected Animator _anim;
    protected Rigidbody _rb;
    protected GameObject _player;

    protected static readonly int _state = Animator.StringToHash("State");

    public virtual void Initialize(Animator animator, Rigidbody rigidbody, GameObject player)
    {
        _anim = animator;
        _rb = rigidbody;
        _player = player;
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

public class StateIdle : PlayerStates
{
    public override void Initialize(Animator animator, Rigidbody rigidbody, GameObject player)
    {
        base.Initialize(animator, rigidbody, player);
        state = StateTypePlayer.Idle;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        _anim.SetInteger(_state, (int)state);
    }
}

public class StateWalk : PlayerStates
{
    public override void Initialize(Animator animator, Rigidbody rigidbody, GameObject player)
    {
        base.Initialize(animator, rigidbody, player);
        state = StateTypePlayer.WalkFront;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        _anim.SetInteger(_state, (int)state);
    }
}