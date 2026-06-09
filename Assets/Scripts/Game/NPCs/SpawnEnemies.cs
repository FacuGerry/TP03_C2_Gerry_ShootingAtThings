using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    [Header("Enemy spawners")]
    [SerializeField] private BoxCollider[] _spawnPlaces = new BoxCollider[0];
    [SerializeField] private AnchorsSO _anchors;

    private MyPoolManager _mng;

    private void Start()
    {
        if (GameBootstrapper.Instance == null) return;

        _mng = GameBootstrapper.Instance.PoolManager;
        SpawnEnemy<EnemyLaser>();
        SpawnEnemy<EnemyGrenade>();
        SpawnEnemy<EnemyFloor>();
        SpawnEnemy<EnemyFlying>();
    }

    private void SpawnEnemy<T>() where T : MonoBehaviour, IPooleable
    {
        if (_anchors.playerTransform == null) return;

        int size = _mng.GetPoolSize<T>(); // get enemy list size

        for (int j = 0; j < size; j++)
        {
            T enemy = _mng.GetInstanceFromPool<T>(); // take new enemy
            NpcController controller = enemy.GetComponent<NpcController>();

            BoxCollider coll = null;
            for (int i = 0; i < _spawnPlaces.Length; i++) // set which collider it will use to spawn
            {
                float rand = Random.value;
                if (rand <= (i / 10f))
                {
                    coll = _spawnPlaces[i];
                    break;
                }
            }
            if (coll == null)
                coll = _spawnPlaces[0];

            Bounds bounds = coll.bounds; // select position in collider to spawn
            Vector3 randomOffset = new(Random.Range(-bounds.extents.x, bounds.extents.x), bounds.extents.y, Random.Range(-bounds.extents.z, bounds.extents.z));
            Vector3 pos = bounds.center + randomOffset;

            enemy.Activate();
            controller.Initialize(_anchors.playerTransform);
            enemy.transform.SetPositionAndRotation(pos, coll.gameObject.transform.rotation);
        }
    }
}