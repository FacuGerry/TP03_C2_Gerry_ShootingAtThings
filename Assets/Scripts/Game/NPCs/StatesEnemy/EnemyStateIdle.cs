using UnityEngine;

public class EnemyStateIdle : EnemyStates
{
    public override void Initialize(Animator animator, Rigidbody rigidbody, NpcController controller)
    {
        base.Initialize(animator, rigidbody, controller);
        state = StateTypeEnemy.Idle;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        if (_controller.CanMove)
            _controller.SwitchState(_controller.FindState(StateTypeEnemy.Roam));
    }

    public override void OnUpdate()
    {
        if (_controller.CheckForNearPlayer())
            _controller.SwitchState(_controller.FindState(StateTypeEnemy.Attack));
    }
}
