using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ChangeWeapon))]

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private Transform _shootingPos;
    [SerializeField] private GameObject _laser;
    [SerializeField] private Animator _anim;
    [SerializeField] private List<WeaponDataSO> _weaponsDataList;
    [SerializeField] private float _animationDuration = 1.167f;

    private ChangeWeapon _changeWeapon;
    private bool _isShooting = false;
    private bool _startedShooting = false;
    private float _fireRate = 1f;

    private IEnumerator _coroutineShoot;

    private void Awake()
    {
        _changeWeapon = GetComponent<ChangeWeapon>();
    }

    private void Update()
    {
        if (_isShooting && !_startedShooting)
        {
            _startedShooting = true;
            if (_coroutineShoot != null)
                StopCoroutine(_coroutineShoot);

            _coroutineShoot = Shooting();
            StartCoroutine(_coroutineShoot);
        }
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    private IEnumerator Shooting()
    {
        Debug.Log("Shooting");
        _laser.SetActive(true);
        CalculateSpeedForAnim();

        while (_isShooting)
        {
            if (Physics.Raycast(_shootingPos.position, _shootingPos.forward, out RaycastHit ray, _weaponsDataList[_changeWeapon.Index].shootingDistance))
            {
                if (ray.collider && ray.collider.TryGetComponent(out IDamageable damage))
                {
                    damage.TakeDamage(_weaponsDataList[_changeWeapon.Index].shootingDamage);
                    Debug.Log("Shot " + ray.collider.gameObject.name, ray.collider.gameObject);
                }
            }

            if (GameBootstrapper.Instance)
            {
                if (_weaponsDataList[_changeWeapon.Index].prefab.name == "AK-47")
                    GameBootstrapper.Instance.SfxManager.OnPlayerShootRifle_PlayClip();
                else if (_weaponsDataList[_changeWeapon.Index].prefab.name == "Pistol")
                    GameBootstrapper.Instance.SfxManager.OnPlayerShootPistol_PlayClip();
            }
            yield return new WaitForSeconds(_weaponsDataList[_changeWeapon.Index].shootingSpeed);

            if (_fireRate != _weaponsDataList[_changeWeapon.Index].shootingSpeed) // Check if weapon was changed
                CalculateSpeedForAnim();

            yield return null;
        }

        _laser.SetActive(false);
        Debug.Log("Stopped shooting");
        _startedShooting = false;
        yield return null;
    }

    private void CalculateSpeedForAnim() // set speed for animator shooting anim
    {
        _fireRate = _weaponsDataList[_changeWeapon.Index].shootingSpeed;
        float animSpeed = _animationDuration / _fireRate;
        _anim.speed = animSpeed;
    }

    public void ToogleShooting(bool isShooting) => _isShooting = isShooting;
}
