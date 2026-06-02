using UnityEngine;

public class StateJump : PlayerStates
{
    public override void Initialize(Animator animator, Rigidbody rigidbody, PlayerController controller)
    {
        base.Initialize(animator, rigidbody, controller);
        state = StateTypePlayer.Jump;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        _anim.SetInteger(_state, (int)state);
        _controller.Jump();
    }

    public override void OnUpdate()
    {
        if (_controller.CanJump)
            _controller.SwitchState(_controller.FindState(StateTypePlayer.Idle));
    }
}