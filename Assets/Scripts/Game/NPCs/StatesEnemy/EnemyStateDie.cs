using UnityEngine;

public class EnemyStateDie : EnemyStates
{
    private float _timeToMove = 2f;
    private float _clock = 0f;
    private Vector3 _startPosition;
    private Vector3 _endPosition;

    public override void Initialize(Animator animator, Rigidbody rigidbody, NpcController controller)
    {
        base.Initialize(animator, rigidbody, controller);
        state = StateTypeEnemy.Die;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        _anim.SetInteger(_state, (int)state);
        _startPosition = _controller.transform.position;
        _endPosition = new(_startPosition.x, 0f, _startPosition.z);
    }

    public override void OnUpdate()
    {
        if (_controller.EnemyClass != EnemyClasses.Flying)
        {
            AnimatorStateInfo info = _anim.GetCurrentAnimatorStateInfo(_anim.GetLayerIndex("Base Layer"));

            if (info.IsName("DieToFloor") && info.normalizedTime >= 1f)
                _controller.gameObject.SetActive(false);
        }
        else
        {
            _clock += Time.deltaTime;
            float lerp = _clock / _timeToMove;
            _controller.gameObject.transform.position = Vector3.Lerp(_startPosition, _endPosition, lerp);
            if (Vector3.Distance(_controller.transform.position, _endPosition) <= 0.1f )
                _controller.gameObject.SetActive(false);
        }
    }
}
