using UnityEngine;

public abstract class PlayerStates
{
    public StateTypePlayer state;
    protected Animator _anim;
    protected Rigidbody _rb;
    protected PlayerController _controller;

    protected static readonly int _state = Animator.StringToHash("State");

    public virtual void Initialize(Animator animator, Rigidbody rigidbody, PlayerController controller)
    {
        _anim = animator;
        _rb = rigidbody;
        _controller = controller;
    }

    public virtual void OnEnter() { }

    public virtual void OnUpdate() { }

    public virtual void OnExit() { }
}
