using UnityEngine;

public class CheckFloorCollision : MonoBehaviour
{
    [SerializeField] private PlayerController _controller;

    private void OnCollisionEnter(Collision collision)
    {
        _controller.ChangeCanJump(true);
    }
}