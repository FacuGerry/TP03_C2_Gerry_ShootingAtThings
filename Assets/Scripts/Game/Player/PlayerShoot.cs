using System.Collections;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private PlayerDataSO _data;
    [SerializeField] private Transform _shootingPos;

    private bool _isShooting = false;
    private bool _startedShooting = false;

    private IEnumerator _coroutineShoot;

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
            if (Physics.Raycast(_shootingPos.position, _shootingPos.forward, out RaycastHit ray, _data.shootingDistance))
            {
                if (ray.collider != null && ray.collider.TryGetComponent(out IDamageable damage))
                {
                    damage.TakeDamage(_data.shootingDamage);
                    Debug.Log("Shot " + ray.collider.gameObject.name, ray.collider.gameObject);
                }

                // sfx & vfx of shooting
            }
            yield return new WaitForSeconds(_data.shootingSpeed);
        }
        Debug.Log("Stopped shooting");
        _startedShooting = false;
    }

    public void ToogleShooting(bool isShooting) => _isShooting = isShooting;
}
