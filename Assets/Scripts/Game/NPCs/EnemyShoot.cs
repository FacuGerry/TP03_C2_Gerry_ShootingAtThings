using System.Collections;
using UnityEngine;
using static UnityEngine.ParticleSystem;

[RequireComponent(typeof(NpcController))]

public class EnemyShoot : MonoBehaviour
{
    private static readonly int _state = Animator.StringToHash("State");

    [SerializeField] private Transform _shootingPos;
    [SerializeField] private GameObject _laser;

    private NpcController _controller;

    public Transform ShootingPos => _shootingPos;
    private IEnumerator _coroutineAiming = null;
    private IEnumerator _coroutineShooting = null;
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
        while (_isShooting)
        {
            if (_laser != null)
                _laser.SetActive(true);
            Debug.Log("Aiming...");
            GameBootstrapper.Instance.SfxManager.OnEnemyAim_PlayClip();

            _controller.GetAnim().SetInteger(_state, (int)StateTypeEnemy.Aim);

            yield return new WaitForSeconds(_controller.Data.shootingSpeed * 0.75f);

            _controller.GetAnim().SetInteger(_state, (int)StateTypeEnemy.Attack);

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

                ParticleShoot particle = GameBootstrapper.Instance.PoolManager.GetInstanceFromPool<ParticleShoot>();
                particle.Activate();
                particle.transform.position = ShootingPos.position;
                particle.ParticleSystem.Play();

                yield return new WaitForSeconds(_controller.Data.shootingSpeed);

                if (_laser != null)
                    _laser.SetActive(false);

                if (!particle.ParticleSystem.isStopped)
                    particle.ParticleSystem.Stop();

                particle.DeActivate();
            }

            yield return null;
        }
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

                ParticleShoot particle = GameBootstrapper.Instance.PoolManager.GetInstanceFromPool<ParticleShoot>();
                if (particle != null)
                {
                    particle.Activate();
                    particle.transform.position = ShootingPos.position;
                    particle.ParticleSystem.Play();
                }

                yield return new WaitForSeconds(_controller.Data.shootingSpeed);

                if (particle != null)
                {
                    if (!particle.ParticleSystem.isStopped)
                        particle.ParticleSystem.Stop();

                    particle.DeActivate();
                }
            }
            yield return null;
        }
        _coroutineShooting = null;
        yield return null;
    }

    public void AimAndShoot(bool isShooting)
    {
        _isShooting = isShooting;
        if (_coroutineAiming != null)
        {
            if (_laser != null && _laser.activeInHierarchy)
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
}