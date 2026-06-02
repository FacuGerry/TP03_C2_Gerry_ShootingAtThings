using UnityEngine;

[CreateAssetMenu(fileName = "ScoreData", menuName = "Settings/ScoreData")]

public class ScoreDataSO : ScriptableObject
{
    public int maxScore = 99999999;
    public int scoreForKillingEnemy = 150;
}