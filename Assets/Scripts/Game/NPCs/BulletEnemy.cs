using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.XR;

[RequireComponent(typeof(Collider))]

public class BulletEnemy : MonoBehaviour, IPooleable
{
    [SerializeField] private EnemyDataSO _data;

    public bool IsActive { get; set; }

    private IEnumerator _coroutineThrowing;

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.TryGetComponent(out IDamageable damage))
            damage.TakeDamage(_data.shootingDamage);
        DeActivate();
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    public IEnumerator Moving(Vector3 startPos, Vector3 endPos, float speed, float height)
    {
        float time = 0f;
        while (time < speed)
        {
            float t = time / speed;

            Vector3 pos = Vector3.Lerp(startPos, endPos, t);

            float yOffset = height * t * 4f * (1f - t);
            pos.y += yOffset;

            transform.position = pos;

            time += Time.deltaTime;
            yield return null;
        }


        // GameBootstrapper.Instance.SfxManager.OnEnemyThrow_PlayClip();

        _coroutineThrowing = null;
        DeActivate();
        yield return null;
    }

    public void Move(Vector3 startPos, Vector3 endPos, float speed, float height)
    {
        if (_coroutineThrowing == null)
        {
            _coroutineThrowing = Moving(startPos, endPos, speed, height);
            StartCoroutine(_coroutineThrowing);
        }
    }

    public void Activate()
    {
        IsActive = true;
        gameObject.SetActive(IsActive);
    }

    public void DeActivate()
    {
        IsActive = false;
        gameObject.SetActive(IsActive);
    }
}