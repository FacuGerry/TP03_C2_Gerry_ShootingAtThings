using UnityEngine;

[RequireComponent(typeof(Collider))]

public class BulletEnemy : MonoBehaviour, IPooleable
{
    [SerializeField] private EnemyDataSO _data;

    public bool IsActive { get; set; }

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.TryGetComponent(out IDamageable damage))
            damage.TakeDamage(_data.shootingDamage);
        DeActivate();
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
}