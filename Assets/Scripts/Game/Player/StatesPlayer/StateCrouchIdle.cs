using UnityEngine;

public class StateCrouchIdle : PlayerStates
{
    public override void Initialize(Animator animator, Rigidbody rigidbody, PlayerController controller)
    {
        base.Initialize(animator, rigidbody, controller);
        state = StateTypePlayer.CrouchIdle;
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
            _controller.SwitchState(_controller.FindState(StateTypePlayer.CrouchMove));
            return;
        }

        if (_controller.WantsCrouch)
        {
            _controller.SwitchState(_controller.FindState(StateTypePlayer.CrouchToStandUp));
            return;
        }

        if (_controller.WantsShoot)
        {
            _controller.SwitchState(_controller.FindState(StateTypePlayer.CrouchShoot));
            return;
        }
    }
}
