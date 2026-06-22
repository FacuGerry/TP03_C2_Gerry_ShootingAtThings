using System;
using UnityEngine;

public class EnemyHealthSystem : MonoBehaviour, IDamageable
{
    public event Action OnEnemyDamaged;
    public event Action OnEnemyDie;

    public event Action<int> OnEnemyFirstDamage;
    public event Action<int> OnEnemyUpdateHealth;

    [SerializeField] private int _maxHealth = 100;
    [SerializeField] private GameObject _canvasLife;
    private int _health = 0;
    private bool _hasDied = false;

    private NpcController _controller;

    private void Awake()
    {
        _controller = GetComponent<NpcController>();
    }

    private void OnEnable()
    {
        _controller.OnEnemyDie += RestartHealth;
    }

    private void Start()
    {
        _health = _maxHealth;
    }

    private void OnDisable()
    {
        _controller.OnEnemyDie -= RestartHealth;
    }

    public void TakeDamage(int damage)
    {
        if (_health == _maxHealth)
        {
            _canvasLife.SetActive(true);
            OnEnemyFirstDamage?.Invoke(_maxHealth);
        }

        _health -= damage;

        if (_health <= 0 && !_hasDied)
        {
            _health = 0;
            OnEnemyUpdateHealth?.Invoke(_health);
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
            OnEnemyUpdateHealth?.Invoke(_health);

            if (GameBootstrapper.Instance == null) return;
            GameBootstrapper.Instance.SfxManager.OnEnemyDamaged_PlayClip();
        }
    }

    private void RestartHealth()
    {
        _health = _maxHealth;
    }
}