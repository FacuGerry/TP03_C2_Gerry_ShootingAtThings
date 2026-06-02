using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Player/PlayerData")]
public class PlayerDataSO : ScriptableObject
{
    public GameObject prefab;
    public Transform cameraPos;
    public GameObject weapon;

    [Header("Movement")]
    public float movementSpeed;
    public float jumpForce;

    [Header("Camera")]
    public float cameraFollowSpeed;
    public float rotationSpeedHor;
    public float rotationSpeedVer;
    public float rotationMinVer;
    public float rotationMaxVer;
}