using System.Collections;
using UnityEngine;

public class EnemyFollowPlayer : MonoBehaviour
{
    private IEnumerator _coroutineFollow;
    private bool _isFollowing = false;

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    private IEnumerator Following(NpcController controller)
    {
        while (_isFollowing)
        {
            controller.Agent.SetDestination(controller.Player.position);
            yield return new WaitForSeconds(0.3f);
            yield return null;
        }
        _coroutineFollow = null;
        yield return null;
    }

    public void FollowPlayer(NpcController controller)
    {
        _isFollowing = true;
        if (_coroutineFollow != null)
        {
            StopCoroutine(_coroutineFollow);
            _coroutineFollow = null;
        }
        _coroutineFollow = Following(controller);
        StartCoroutine(_coroutineFollow);
    }

    public void StopFollowing()
    {
        _isFollowing = false;
        StopCoroutine(_coroutineFollow);
        _coroutineFollow = null;
    }
}