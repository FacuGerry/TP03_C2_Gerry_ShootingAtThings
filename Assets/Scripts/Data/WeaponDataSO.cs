using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Weapons/WeaponData")]
public class WeaponDataSO : ScriptableObject
{
    [Header("Shooting settings")]
    public float shootingDistance;
    public int shootingDamage;
    public float shootingSpeed;
}