using UnityEngine;

public class PlayerHealthSystem : MonoBehaviour, IDamageable
{
    [SerializeField] private int _maxLife;
    private int _currentLife;
    private PlayerController _controller;

    private void Start()
    {
        _currentLife = _maxLife;
    }

    public void TakeDamage(int damage)
    {
        _currentLife -= damage;
        if (_currentLife <= 0)
        {
            _currentLife = 0;
            _controller.KillPlayer();

            if (GameBootstrapper.Instance == null) return;
            GameBootstrapper.Instance.SfxManager.OnPlayerDie_PlayClip();
        }
        else
        {
            if (GameBootstrapper.Instance == null) return;
            GameBootstrapper.Instance.SfxManager.OnPlayerDamaged_PlayClip();
        }
    }
}