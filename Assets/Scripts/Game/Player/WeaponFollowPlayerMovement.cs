using Unity.VisualScripting;
using UnityEngine;

public class WeaponFollowPlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform _follow;

    private void LateUpdate()
    {
        Vector3 position = transform.position;
        position.y = _follow.position.y;
        transform.position = position;
    }
}