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

        if (_controller.CheckForNearPlayerToFollow())
            _controller.SwitchState(_controller.FindState(StateTypeEnemy.Follow));
    }

    private Vector3 CalculateNewDestination()
    {
        float randomX = Random.Range(_controller.Waypoints.minX, _controller.Waypoints.maxX);
        float randomZ = Random.Range(_controller.Waypoints.minZ, _controller.Waypoints.maxZ);
        Vector3 destination = new(randomX, _controller.transform.position.y, randomZ);

        return destination;
    }
}
