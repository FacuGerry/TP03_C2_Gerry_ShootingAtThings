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

        switch (_controller.EnemyClass)
        {
            case EnemyClasses.None:
                Debug.LogError("NO CLASS WAS ASSIGNED FOR THIS ENEMY", _controller.gameObject);
                break;

            case EnemyClasses.Laser:
                _controller.SwitchState(_controller.FindState(StateTypeEnemy.Idle));
                break;

            case EnemyClasses.Grenade:
                _controller.SwitchState(_controller.FindState(StateTypeEnemy.Idle));
                break;

            case EnemyClasses.Flying:

                break;

            case EnemyClasses.Floor:

                break;
        }
    }

    public override void OnUpdate()
    {
        // NAVMESH WORK HERE OR IN OnEnter()

        if (_controller.CheckForNearPlayer())
            _controller.SwitchState(_controller.FindState(StateTypeEnemy.Attack));
    }
}