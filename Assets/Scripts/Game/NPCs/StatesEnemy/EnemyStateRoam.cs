using UnityEngine;

public class EnemyStateRoam : EnemyStates
{
    public override void Initialize(Animator animator, Rigidbody rigidbody, NpcController controller)
    {
        base.Initialize(animator, rigidbody, controller);
        state = StateTypeEnemy.Roam;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        if (_controller.CanMove)
            _controller.SwitchState(_controller.FindState(StateTypeEnemy.Roam));
    }

    public override void OnUpdate()
    {
        // NAVMESH WORK HERE OR IN OnEnter()

        if (_controller.CheckForNearPlayer())
            _controller.SwitchState(_controller.FindState(StateTypeEnemy.Attack));
    }
}
