using UnityEngine;

public class StateShoot : PlayerStates
{
    public override void Initialize(Animator animator, Rigidbody rigidbody, PlayerController controller)
    {
        base.Initialize(animator, rigidbody, controller);
        state = StateTypePlayer.Shoot;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        _anim.SetInteger(_state, (int)state);
        _controller.Shoot.ToogleShooting(true);
    }

    public override void OnUpdate()
    {
        if (!_controller.WantsShoot)
            _controller.SwitchState(_controller.FindState(StateTypePlayer.Idle));
    }

    public override void OnExit()
    {
        _anim.speed = 1f;
        _controller.Shoot.ToogleShooting(false);
    }
}