using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "NPC/EnemyData")]

public class EnemyDataSO : ScriptableObject
{
    public EnemyClasses enemyClass;
    public EnemyAttackType attackType;
    public float distanceToShoot;
    public int shootingDamage;
    public int shootingSpeed;
    public int shootingHeight;
    public int throwingDuration;
    public bool canMove;
}