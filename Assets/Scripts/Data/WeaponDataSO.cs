using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Weapons/WeaponData")]
public class WeaponDataSO : ScriptableObject
{
    public GameObject prefab;
    public CustomIkController[] controller;

    [Header("Shooting")]
    public float shootingDistance;
    public int shootingDamage;
    public float shootingSpeed;
}