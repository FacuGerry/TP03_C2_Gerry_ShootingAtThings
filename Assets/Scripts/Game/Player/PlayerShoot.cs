using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ChangeWeapon))]

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private Transform _shootingPos;
    [SerializeField] private List<WeaponDataSO> _weaponsDataList;

    private ChangeWeapon _changeWeapon;
    private bool _isShooting = false;
    private bool _startedShooting = false;

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
        while (_isShooting)
        {
            if (Physics.Raycast(_shootingPos.position, _shootingPos.forward, out RaycastHit ray, _weaponsDataList[_changeWeapon.Index].shootingDistance))
            {
                if (ray.collider != null && ray.collider.TryGetComponent(out IDamageable damage))
                {
                    damage.TakeDamage(_weaponsDataList[_changeWeapon.Index].shootingDamage);
                    Debug.Log("Shot " + ray.collider.gameObject.name, ray.collider.gameObject);
                }

                if (GameBootstrapper.Instance != null)
                {
                    if (_weaponsDataList[_changeWeapon.Index].prefab.name == "AK-47")
                        GameBootstrapper.Instance.SfxManager.OnPlayerShootRifle_PlayClip();
                    else if (_weaponsDataList[_changeWeapon.Index].prefab.name == "Pistol")
                        GameBootstrapper.Instance.SfxManager.OnPlayerShootPistol_PlayClip();
                }
            }
            yield return new WaitForSeconds(_weaponsDataList[_changeWeapon.Index].shootingSpeed);
        }
        Debug.Log("Stopped shooting");
        _startedShooting = false;
    }

    public void ToogleShooting(bool isShooting) => _isShooting = isShooting;
}
