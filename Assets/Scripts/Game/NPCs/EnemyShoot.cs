using System.Collections;
using UnityEngine;

[RequireComponent(typeof(NpcController))]

public class EnemyShoot : MonoBehaviour
{
    [SerializeField] private Transform _shootingPos;
    [SerializeField] private GameObject _laser;

    private NpcController _controller;

    public Transform ShootingPos => _shootingPos;
    private IEnumerator _coroutineAiming = null;
    private IEnumerator _coroutineShooting = null;
    private IEnumerator _coroutineThrowing = null;
    private bool _isShooting = false;

    private void Awake()
    {
        _controller = GetComponent<NpcController>();
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    private IEnumerator AimingAndShooting()
    {
        _laser.SetActive(true);
        Debug.Log("Aiming...");
        GameBootstrapper.Instance.SfxManager.OnEnemyAim_PlayClip();

        float clock = 0f;
        while (clock < _controller.Data.shootingSpeed)
        {
            clock += Time.deltaTime;
            transform.LookAt(_controller.Player.transform);
            yield return null;
        }

        if (Physics.Raycast(ShootingPos.position, ShootingPos.forward, out RaycastHit ray, _controller.Data.distanceToShoot))
        {
            if (ray.collider != null && ray.collider.TryGetComponent(out IDamageable damage))
            {
                damage.TakeDamage(_controller.Data.shootingDamage);
                Debug.Log("Shot " + ray.collider.gameObject.name, ray.collider.gameObject);
            }
            else
                Debug.Log("Shot and missed");

            GameBootstrapper.Instance.SfxManager.OnEnemyShootLaser_PlayClip();
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
            if (Physics.Raycast(ShootingPos.position, ShootingPos.forward, out RaycastHit ray, _controller.Data.distanceToShoot))
            {
                if (ray.collider != null && ray.collider.TryGetComponent(out IDamageable damage))
                {
                    damage.TakeDamage(_controller.Data.shootingDamage);
                    Debug.Log("Shot " + ray.collider.gameObject.name, ray.collider.gameObject);
                }
                GameBootstrapper.Instance.SfxManager.OnEnemyShoot_PlayClip();
            }
            yield return new WaitForSeconds(_controller.Data.shootingSpeed);
            yield return null;
        }
        _coroutineShooting = null;
        yield return null;
    }

    private IEnumerator Throwing(Transform startPos)
    {
        if (GameBootstrapper.Instance == null) yield return null;
        while (_isShooting)
        {
            Debug.Log("Threw something");
            BulletEnemy bullet = GameBootstrapper.Instance.PoolManager.GetInstanceFromPool<BulletEnemy>();
            if (bullet == null) yield return null;

            bullet.transform.position = startPos.position;

            Vector3 playerVelocity = _controller.PlayerRb.linearVelocity;
            playerVelocity.y = 0f;
            Vector3 targetFuturePosition = _controller.Player.position + playerVelocity;

            bullet.Activate();
            bullet.Move(startPos.position, targetFuturePosition, _controller.Data.throwingDuration, _controller.Data.shootingHeight);

            yield return new WaitForSeconds(_controller.Data.shootingSpeed);
        }
    }

    public void AimAndShoot(bool isShooting)
    {
        if (_coroutineAiming != null)
        {
            if (_laser.activeInHierarchy)
                _laser.SetActive(false);

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

    public void ThrowObject(bool isShooting, Transform startPos)
    {
        _isShooting = isShooting;
        if (_coroutineThrowing != null)
        {
            StopCoroutine(_coroutineThrowing);
            _coroutineThrowing = null;
        }
        if (isShooting)
        {
            _coroutineThrowing = Throwing(startPos);
            StartCoroutine(_coroutineThrowing);
        }
    }
}