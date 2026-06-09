using UnityEngine;

public class EnemyStateFollow : EnemyStates
{
    public override void Initialize(Animator animator, Rigidbody rigidbody, NpcController controller)
    {
        base.Initialize(animator, rigidbody, controller);
        state = StateTypeEnemy.Follow;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        _anim.SetInteger(_state, (int)state);
        _controller.FollowPlayer.FollowPlayer(_controller);
    }

    public override void OnUpdate()
    {
        if (_controller.CheckForNearPlayerToAttack())
            _controller.SwitchState(_controller.FindState(StateTypeEnemy.Attack));

        if (!_controller.CheckForNearPlayerToFollow())
            _controller.SwitchState(_controller.FindState(StateTypeEnemy.Roam));
    }

    public override void OnExit()
    {
        base.OnExit();
        _controller.FollowPlayer.StopFollowing();
    }
}