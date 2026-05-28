using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Player/PlayerData")]
public class PlayerDataSO : ScriptableObject
{
    [Header("Movement")]
    public float movementSpeed;
    public float jumpForce;

    [Header("Camera")]
    public float cameraFollowSpeed;
    public float rotationSpeedHor;
    public float rotationSpeedVer;
    public float rotationMinVer;
    public float rotationMaxVer;

    [Header("Shooting (hay que sacar esto, va a las weapons)")]
    public float shootingDistance;
    public int shootingDamage;
    public float shootingSpeed;
}