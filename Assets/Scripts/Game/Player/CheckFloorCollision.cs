using UnityEngine;

public class CheckFloorCollision : MonoBehaviour
{
    [SerializeField] private PlayerController _controller;

    private void OnTriggerEnter(Collider coll)
    {
        _controller.ChangeCanJump(true);
    }
}