using UnityEngine;

public class StateIdle : PlayerStates
{
    public override void Initialize(Animator animator, Rigidbody rigidbody, PlayerController controller)
    {
        base.Initialize(animator, rigidbody, controller);
        state = StateTypePlayer.Idle;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        _anim.SetInteger(_state, (int)state);
    }

    public override void OnUpdate()
    {
        if (_controller.MoveInput.magnitude > 0.01f)
        {
            _controller.SwitchState(_controller.FindState(StateTypePlayer.Walk));
            return;
        }

        if (_controller.WantsJump)
        {
            _controller.SwitchState(_controller.FindState(StateTypePlayer.Jump));
            return;
        }

        if (_controller.WantsCrouch)
        {
            _controller.SwitchState(_controller.FindState(StateTypePlayer.StandUpToCrouch));
            return;
        }

        if (_controller.WantsShoot)
        {
            _controller.SwitchState(_controller.FindState(StateTypePlayer.Shoot));
            return;
        }
    }
}
