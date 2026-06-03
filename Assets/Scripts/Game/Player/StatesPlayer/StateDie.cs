using UnityEngine;

public class StateDie : PlayerStates
{
    public override void Initialize(Animator animator, Rigidbody rigidbody, PlayerController controller)
    {
        base.Initialize(animator, rigidbody, controller);
        state = StateTypePlayer.Die;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        _anim.SetInteger(_state, (int)state);
    }

    public override void OnUpdate()
    {
        AnimatorStateInfo info = _anim.GetCurrentAnimatorStateInfo(_anim.GetLayerIndex("Base Layer"));

        if (info.IsName("Die") && info.normalizedTime >= 1f)
            _controller.gameObject.SetActive(false);
    }
}
