using UnityEngine;

public class EnemyGrenade : MonoBehaviour, IPooleable
{
    private EnemyHealthSystem _hs;

    public bool IsActive { get; set; }

    private void Awake()
    {
        _hs = GetComponent<EnemyHealthSystem>();
    }

    private void OnEnable()
    {
        _hs.OnEnemyDie += OnEnemyDie_DeActivateEnemy;
    }

    private void OnDisable()
    {
        _hs.OnEnemyDie -= OnEnemyDie_DeActivateEnemy;
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

    private void OnEnemyDie_DeActivateEnemy() => DeActivate();
}
