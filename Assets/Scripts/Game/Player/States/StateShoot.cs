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

    public override void OnExit()
    {
        _controller.Shoot.ToogleShooting(false);
    }
}