using UnityEngine;

public class EnemyStateAttack : EnemyStates
{
    public override void Initialize(Animator animator, Rigidbody rigidbody, NpcController controller)
    {
        base.Initialize(animator, rigidbody, controller);
        state = StateTypeEnemy.Attack;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        _anim.SetInteger(_state, (int)state);

        switch (_controller.AttackType)
        {
            case EnemyAttackType.None:
                Debug.LogError("THERE WAS NO ATTACK TYPE ASSIGNED TO ENEMY", _controller.gameObject);
                if (_controller.CanMove)
                    _controller.SwitchState(_controller.FindState(StateTypeEnemy.Roam));
                else
                    _controller.SwitchState(_controller.FindState(StateTypeEnemy.Idle));
                break;

            case EnemyAttackType.AimAndShoot:
                _controller.Shoot.AimAndShoot(true);
                break;

            case EnemyAttackType.Shoot:
                _controller.Shoot.NormalShoot(true);
                break;

            case EnemyAttackType.ThrowObject:
                _controller.Shoot.ThrowObject(true, _controller.Shoot.ShootingPos);
                break;
        }
    }

    public override void OnUpdate()
    {
        if (!_controller.CheckForNearPlayerToAttack())
            _controller.SwitchState(_controller.FindState(StateTypeEnemy.Idle));
    }

    public override void OnExit()
    {
        base.OnExit();
        switch (_controller.AttackType)
        {
            case EnemyAttackType.None:
                Debug.LogError("THERE WAS NO ATTACK TYPE ASSIGNED TO ENEMY", _controller.gameObject);
                break;

            case EnemyAttackType.AimAndShoot:
                _controller.Shoot.AimAndShoot(false);
                break;

            case EnemyAttackType.Shoot:
                _controller.Shoot.NormalShoot(false);
                break;

            case EnemyAttackType.ThrowObject:
                _controller.Shoot.ThrowObject(false, _controller.Shoot.ShootingPos);
                break;
        }
    }
}
