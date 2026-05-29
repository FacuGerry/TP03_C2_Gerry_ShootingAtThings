using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "NPCs/EnemyData")]

public class EnemyDataSO : ScriptableObject
{
    public EnemyClasses enemyClass;
    public EnemyAttackType attackType;
    public float distanceToShoot;
    public bool canMove;
    public GameObject objectToThrow;
}