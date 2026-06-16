using System;
using System.Collections;
using UnityEngine;

public class PlayerHealthSystem : MonoBehaviour, IDamageable
{
    public event Action<int> OnLifeUpdated;
    public event Action OnPlayerDie;

    [SerializeField] private int _maxLife;
    private int _currentLife;
    private PlayerController _controller;

    private IEnumerator _coroutineWaiting;

    private void Awake()
    {
        _controller = GetComponent<PlayerController>();
    }

    private void Start()
    {
        _currentLife = _maxLife;
        OnLifeUpdated?.Invoke(_currentLife);
        _coroutineWaiting = null;
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    private IEnumerator WaitingForDie()
    {
        float clock = 3f;
        while (clock >= 0f)
        {
            clock -= Time.deltaTime;
            yield return null;
        }
        OnPlayerDie?.Invoke();
        yield return null;
    }

    public void TakeDamage(int damage)
    {
        if (_currentLife <= 0) return;

        _currentLife -= damage;
        if (_currentLife <= 0)
        {
            _currentLife = 0;
            _controller.KillPlayer();

            if (_coroutineWaiting == null)
            {
                _coroutineWaiting = WaitingForDie();
                StartCoroutine(_coroutineWaiting);
            }

            if (GameBootstrapper.Instance == null) return;
            GameBootstrapper.Instance.SfxManager.OnPlayerDie_PlayClip();
        }
        else
        {
            if (GameBootstrapper.Instance == null) return;
            GameBootstrapper.Instance.SfxManager.OnPlayerDamaged_PlayClip();
        }
        OnLifeUpdated?.Invoke(_currentLife);
    }
}