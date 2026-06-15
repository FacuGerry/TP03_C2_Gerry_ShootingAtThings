using UnityEngine;

public class ParticleShoot : MonoBehaviour, IPooleable
{
    public bool IsActive { get; set; }

    public ParticleSystem ParticleSystem { get; private set; }

    private void Awake()
    {
        ParticleSystem = GetComponent<ParticleSystem>();
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