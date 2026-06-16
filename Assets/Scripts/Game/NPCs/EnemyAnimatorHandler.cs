using UnityEngine;

public class EnemyAnimatorHandler : MonoBehaviour
{
    [SerializeField] private NpcController _controller;
    [SerializeField] private Transform _startPos;

    public void OnThrowing()
    {
        BulletEnemy bullet = GameBootstrapper.Instance.PoolManager.GetInstanceFromPool<BulletEnemy>();
        if (bullet == null)
        {
            Debug.LogError("NO MORE BULLETS");
            return; 
        }
        bullet.Activate();

        bullet.transform.position = _startPos.position;

        Vector3 playerVelocity = _controller.PlayerRb.linearVelocity;
        playerVelocity.y = 0f;
        Vector3 targetFuturePosition = _controller.Player.position + playerVelocity;

        bullet.Move(_startPos.position, targetFuturePosition, _controller.Data.throwingDuration, _controller.Data.shootingHeight);
    }
}