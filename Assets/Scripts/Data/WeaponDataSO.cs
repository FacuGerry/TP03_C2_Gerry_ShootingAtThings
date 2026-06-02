using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Weapons/WeaponData")]
public class WeaponDataSO : ScriptableObject
{
    public GameObject prefab;

    [Header("Shooting settings")]
    public float shootingDistance;
    public int shootingDamage;
    public float shootingSpeed;
}