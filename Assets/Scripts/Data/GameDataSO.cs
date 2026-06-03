using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "Settings/GameData")]

public class GameDataSO : ScriptableObject
{
    public PlayerDataSO playerData;
}