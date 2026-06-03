using System;
using UnityEngine;

public class EnemyHealthSystem : MonoBehaviour, IDamageable
{
    public event Action OnEnemyDamaged;
    public event Action OnEnemyDie;

    [SerializeField] private int _maxHealth;
    private int _health;
    private bool _hasDied = false;

    private void Start()
    {
        _health = _maxHealth;
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
        if (_health <= 0 && !_hasDied)
        {
            _health = 0;
            _hasDied = true;
            OnEnemyDie?.Invoke();

            if (GameBootstrapper.Instance == null) return;
            GameBootstrapper.Instance.SfxManager.OnEnemyDie_PlayClip();
            GameBootstrapper.Instance.ScoreManager.AddScoreForKillingEnemy();
        }
        else if (_health <= 0 && _hasDied)
        {
            _health = 0;
        }
        else
        {
            OnEnemyDamaged?.Invoke();

            if (GameBootstrapper.Instance == null) return;
            GameBootstrapper.Instance.SfxManager.OnEnemyDamaged_PlayClip();
        }
    }
}