using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour, IPooleable
{
    public bool IsActive { get; set; }

    public List<CustomIkController> controller;

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