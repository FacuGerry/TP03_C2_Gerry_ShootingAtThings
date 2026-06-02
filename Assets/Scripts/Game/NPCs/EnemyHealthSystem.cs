using System;
using UnityEngine;

public class EnemyHealthSystem : MonoBehaviour, IDamageable
{
    public event Action OnEnemyDamaged;
    public event Action OnEnemyDie;

    [SerializeField] private int _maxHealth;
    private int _health;

    private void Start()
    {
        _health = _maxHealth;
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
        if (_health <= 0)
        {
            _health = 0;

            if (GameBootstrapper.Instance != null)
                GameBootstrapper.Instance.SfxManager.OnEnemyDie_PlayClip();

            OnEnemyDie?.Invoke();
        }
        else
        {
            OnEnemyDamaged?.Invoke();

            if (GameBootstrapper.Instance != null)
                GameBootstrapper.Instance.SfxManager.OnEnemyDamaged_PlayClip();
        }
    }
}