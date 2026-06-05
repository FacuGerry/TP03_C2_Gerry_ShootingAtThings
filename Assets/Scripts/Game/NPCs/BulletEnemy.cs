using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider))]

public class BulletEnemy : MonoBehaviour, IPooleable
{
    [SerializeField] private LayerMask _layer;
    [SerializeField] private EnemyDataSO _data;

    public bool IsActive { get; set; }

    private IEnumerator _coroutineThrowing;

    private void OnTriggerEnter(Collider coll)
    {
        if (_layer == coll.gameObject.layer && coll.TryGetComponent(out IDamageable damage))
            damage.TakeDamage(_data.shootingDamage);
        DeActivate();
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    public IEnumerator Moving(Vector3 startPos, Vector3 endPos, float speed, float height)
    {
        if (speed == 0f) speed = 1f; // check because speed MUST NOT BE 0
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

        if (GameBootstrapper.Instance != null)
            GameBootstrapper.Instance.SfxManager.OnEnemyThrow_PlayClip();

        Debug.Log("Threw something");
        _coroutineThrowing = null;
        DeActivate();
    }

    public void Move(Vector3 startPos, Vector3 endPos, float speed, float height)
    {
        if (_coroutineThrowing != null)
            StopCoroutine(_coroutineThrowing);

        _coroutineThrowing = Moving(startPos, endPos, speed, height);
        StartCoroutine(_coroutineThrowing);
    }

    public void Activate()
    {
        IsActive = true;
        gameObject.SetActive(IsActive);
    }

    public void DeActivate()
    {
        IsActive = false;

        if (_coroutineThrowing != null)
        {
            StopCoroutine(_coroutineThrowing);
            _coroutineThrowing = null;
        }

        gameObject.SetActive(IsActive);
    }
}