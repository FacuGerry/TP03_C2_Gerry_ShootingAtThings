using UnityEngine;

public class PlayerHealthSystem : MonoBehaviour, IDamageable
{
    [SerializeField] private int _maxLife;
    private int _currentLife;

    private void Start()
    {
        _currentLife = _maxLife;
    }

    public void TakeDamage(int damage)
    {
        // sfx & vfx of player damaged
        _currentLife -= damage;
        if (_currentLife < 0)
        {
            _currentLife = 0;
            // sfx & vfx of player die
            gameObject.SetActive(false);
        }
    }
}