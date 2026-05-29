using System.Collections;
using UnityEngine;

public class EnemyShoot : MonoBehaviour, IPooleable
{
    public bool IsActive { get; set; }

    private IEnumerator _coroutineAiming = null;
    private IEnumerator _coroutineShooting = null;
    private IEnumerator _coroutineThrowing = null;

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    private IEnumerator AimingAndShooting()
    {
        Debug.Log("Aiming...");
        yield return new WaitForSeconds(1f);
        Debug.Log("Aimed and shot");

        _coroutineAiming = null;
        yield return null;
    }

    private IEnumerator NormalShooting()
    {
        Debug.Log("Shot normally");
        _coroutineShooting = null;
        yield return null;
    }

    private IEnumerator Throwing()
    {
        Debug.Log("Threw something");
        _coroutineThrowing = null;
        yield return null;
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

    public void ThrowObject(bool isShooting)
    {
        if (_coroutineThrowing != null)
        {
            StopCoroutine(_coroutineThrowing);
            _coroutineThrowing = null;
        }
        if (isShooting)
        {
            _coroutineThrowing = Throwing();
            StartCoroutine(_coroutineThrowing);
        }
    }
}