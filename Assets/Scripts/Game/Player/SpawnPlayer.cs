using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    [SerializeField] private GameDataSO _gameData;

    private void Awake()
    {
        SetPlayer();
    }

    private void SetPlayer() => Instantiate(_gameData.playerData.prefab, transform.position, transform.rotation);
}