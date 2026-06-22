using UnityEngine;

public class EnemyGrenade : MonoBehaviour, IPooleable
{
    private NpcController _controller;

    public bool IsActive { get; set; }

    private void Awake()
    {
        _controller = GetComponent<NpcController>();
    }

    private void OnEnable()
    {
        _controller.OnEnemyDie += DeActivate;
    }

    private void OnDisable()
    {
        _controller.OnEnemyDie -= DeActivate;
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
