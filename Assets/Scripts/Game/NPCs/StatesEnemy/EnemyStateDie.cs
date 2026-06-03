using UnityEngine;

public class EnemyStateDie : EnemyStates
{
    public override void Initialize(Animator animator, Rigidbody rigidbody, NpcController controller)
    {
        base.Initialize(animator, rigidbody, controller);
        state = StateTypeEnemy.Die;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        _anim.SetInteger(_state, (int)state);
    }

    public override void OnUpdate()
    {
        AnimatorStateInfo info = _anim.GetCurrentAnimatorStateInfo(_anim.GetLayerIndex("Base Layer"));

        if (info.IsName("DieToFloor") && info.normalizedTime >= 1f)
            _controller.gameObject.SetActive(false);
    }
}
