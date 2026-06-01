using System.Collections;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    [SerializeField] private GameObject _laser;
    [SerializeField] private Transform _player;
    [SerializeField] private Transform _shootingPos;
    [SerializeField] private float _distance;
    [SerializeField] private int _damage;
    [SerializeField] private float _waitingTime;
    public Transform ShootingPos => _shootingPos;
    private IEnumerator _coroutineAiming = null;
    private IEnumerator _coroutineShooting = null;
    private IEnumerator _coroutineThrowing = null;
    private bool _isShooting = false;

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    private IEnumerator AimingAndShooting()
    {
        _laser.SetActive(true);
        Debug.Log("Aiming...");

        float clock = 0f;
        while (clock < _waitingTime)
        {
            clock += Time.deltaTime;
            transform.LookAt(_player);
            yield return null;
        }

        if (Physics.Raycast(ShootingPos.position, ShootingPos.forward, out RaycastHit ray, _distance))
        {
            if (ray.collider != null && ray.collider.TryGetComponent(out IDamageable damage))
            {
                damage.TakeDamage(_damage);
                Debug.Log("Shot " + ray.collider.gameObject.name, ray.collider.gameObject);
            }
            else
                Debug.Log("Shot and missed");

            // sfx & vfx of shooting
        }

        _laser.SetActive(false);
        _coroutineAiming = null;
        yield return null;
    }

    private IEnumerator NormalShooting()
    {
        Debug.Log("Shooting normally");
        while (_isShooting)
        {
            if (Physics.Raycast(ShootingPos.position, ShootingPos.forward, out RaycastHit ray, _distance))
            {
                if (ray.collider != null && ray.collider.TryGetComponent(out IDamageable damage))
                {
                    damage.TakeDamage(_damage);
                    Debug.Log("Shot " + ray.collider.gameObject.name, ray.collider.gameObject);
                }
                // sfx & vfx of shooting
            }
            yield return null;
        }
        _coroutineShooting = null;
        yield return null;
    }

    private IEnumerator Throwing(Transform startPos, float height, float duration, GameObject player)
    {
        Debug.Log("Threw something");

        BulletEnemy bullet = GameBootstrapper.Instance.PoolManager.GetInstanceFromPool<BulletEnemy>();
        GameObject go = bullet.gameObject;
        go.SetActive(true);

        Vector3 start = startPos.position;

        //CarMovement car = player.GetComponent<CarMovement>();
        Vector3 playerVelocity = Vector3.zero;
        playerVelocity.y = 0f;

        Vector3 futurePos = player.transform.position + playerVelocity;

        float time = 0f;
        while (time < duration)
        {
            float t = time / duration;

            Vector3 pos = Vector3.Lerp(start, futurePos, t);

            float yOffset = height * t * 4f * (1f - t);
            pos.y += yOffset;

            go.transform.position = pos;

            time += Time.deltaTime;
            yield return null;
        }

        go.SetActive(false);

        GameBootstrapper.Instance.SfxManager.OnEnemyShoot_PlayClip();

        _coroutineThrowing = null;
        yield return null;
    }

    public void AimAndShoot(bool isShooting)
    {
        if (_coroutineAiming != null)
        {
            StopCoroutine(_coroutineAiming);
            _coroutineAiming = null;
        }
        if (isShooting)
        {
            _coroutineAiming = AimingAndShooting();
            StartCoroutine(_coroutineAiming);
        }
    }

    public void NormalShoot(bool isShooting)
    {
        _isShooting = isShooting;
        if (_coroutineShooting != null)
        {
            StopCoroutine(_coroutineShooting);
            _coroutineShooting = null;
        }
        if (isShooting)
        {
            _coroutineShooting = NormalShooting();
            StartCoroutine(_coroutineShooting);
        }
    }

    public void ThrowObject(Transform startPos, float height, float duration, GameObject player)
    {
        if (_coroutineThrowing != null)
        {
            StopCoroutine(_coroutineThrowing);
            _coroutineThrowing = null;
        }
        _coroutineThrowing = Throwing(startPos, height, duration, player);
        StartCoroutine(_coroutineThrowing);
    }

    public void StopThrowObject()
    {
        StopCoroutine(_coroutineThrowing);
        _coroutineThrowing = null;
    }
}