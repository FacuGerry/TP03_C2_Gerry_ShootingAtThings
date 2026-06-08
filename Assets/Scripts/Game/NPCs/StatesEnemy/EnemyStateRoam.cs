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
        _anim.SetInteger(_state, (int)state);
        _controller.Agent.SetDestination(CalculateNewDestination());
    }

    public override void OnUpdate()
    {
        float distance = Vector3.Distance(_controller.transform.position, _controller.Agent.destination);
        if (distance < 0.5f)
            _controller.Agent.SetDestination(CalculateNewDestination());

        if (_controller.CheckForNearPlayer())
        {
            _controller.Agent.isStopped = true;
            _controller.SwitchState(_controller.FindState(StateTypeEnemy.Follow));
        }
    }

    private Vector3 CalculateNewDestination()
    {
        float randomX = Random.Range(_controller.Waypoints.minX, _controller.Waypoints.maxX);
        float randomZ = Random.Range(_controller.Waypoints.minZ, _controller.Waypoints.maxZ);
        float positionY = 0f;

        switch (_controller.EnemyClass)
        {
            case EnemyClasses.Flying:
                positionY = _controller.Waypoints.heightForFlyingEnemies;
                break;

            case EnemyClasses.Floor:
                positionY = 0f;
                break;
        }

        Vector3 destination = new(randomX, positionY, randomZ);

        return destination;
    }
}
