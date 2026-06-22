using System.Collections.Generic;
using UnityEngine;

public class EnemyDieVfx : MonoBehaviour
{
    private List<NpcController> _enemies = new();

    private void Start()
    {
        if (!GameBootstrapper.Instance) return;
        InitEvents();

        foreach (NpcController item in _enemies)
            item.OnEnemyDieSendPosition += OnEnemyDie_SendToVfx;
    }

    private void OnDestroy()
    {
        if (!GameBootstrapper.Instance) return;

        foreach (NpcController item in _enemies)
            item.OnEnemyDieSendPosition -= OnEnemyDie_SendToVfx;
    }

    private void InitEvents()
    {
        foreach (EnemyLaser item in GameBootstrapper.Instance.PoolManager.GetPool<EnemyLaser>())
            _enemies.Add(item.gameObject.GetComponent<NpcController>());

        foreach (EnemyFloor item in GameBootstrapper.Instance.PoolManager.GetPool<EnemyFloor>())
            _enemies.Add(item.gameObject.GetComponent<NpcController>());

        foreach (EnemyFlying item in GameBootstrapper.Instance.PoolManager.GetPool<EnemyFlying>())
            _enemies.Add(item.gameObject.GetComponent<NpcController>());

        foreach (EnemyGrenade item in GameBootstrapper.Instance.PoolManager.GetPool<EnemyGrenade>())
            _enemies.Add(item.gameObject.GetComponent<NpcController>());
    }

    private void OnEnemyDie_SendToVfx(Vector3 position)
    {
        UiScoreAdded score = GameBootstrapper.Instance.PoolManager.GetInstanceFromPool<UiScoreAdded>();
        score.Activate();
        score.StartShowOnScreen(position);
    }
}