using UnityEngine;

public class EnemyHealthSystem : MonoBehaviour, IDamageable
{
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
            // sfx & vfx for dead
            gameObject.SetActive(false);
        }
        else
        {
            // sfx & vfx for damaged
        }
    }
}