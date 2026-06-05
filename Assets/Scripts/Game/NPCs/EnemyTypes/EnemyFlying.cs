using UnityEngine;

public class EnemyFlying : MonoBehaviour, IPooleable
{
    public bool IsActive { get; set; }

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