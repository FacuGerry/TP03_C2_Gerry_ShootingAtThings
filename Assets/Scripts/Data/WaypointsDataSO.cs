using UnityEngine;

[CreateAssetMenu(fileName = "Waypoints", menuName = "Settings/Waypoints")]

public class WaypointsDataSO : ScriptableObject
{
    [Header("Waypoints")]
    public float minX;
    public float maxX;
    public float minZ;
    public float maxZ;

    [Header("Flying")]
    public float heightForFlyingEnemies;
}