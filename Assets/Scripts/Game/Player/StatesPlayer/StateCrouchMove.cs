using UnityEngine;

public class StateCrouchMove : PlayerStates
{
    public override void Initialize(Animator animator, Rigidbody rigidbody, PlayerController controller)
    {
        base.Initialize(animator, rigidbody, controller);
        state = StateTypePlayer.CrouchMove;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        _anim.SetInteger(_state, (int)state);
    }

    public override void OnUpdate()
    {
        _controller.CalculatePlayerSpeed();

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

        if (_controller.MoveInput.sqrMagnitude < 0.01f)
        {
            _controller.SwitchState(_controller.FindState(StateTypePlayer.CrouchIdle));
            return;
        }
    }
}
