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
        _controller.ChangeCanJump(false);
    }

    public override void OnUpdate()
    {
        if (_rb.linearVelocity.y <= 0.01f && IsOnFloor())
            _controller.SwitchState(_controller.FindState(StateTypePlayer.Idle));
    }

    private bool IsOnFloor() => Physics.Raycast(_controller.transform.position, Vector3.down, 1.1f);
}