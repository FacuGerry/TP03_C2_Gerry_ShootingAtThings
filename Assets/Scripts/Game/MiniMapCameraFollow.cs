using System;
using UnityEngine;

public class MiniMapCameraFollow : MonoBehaviour
{
    [SerializeField] private AnchorsSO _anchors;
    private Transform _player;

    private void Start()
    {
        _player = _anchors.playerTransform;
    }

    private void LateUpdate()
    {
        FollowPlayer();
        RotateWithPlayer();
    }

    private void FollowPlayer()
    {
        Vector3 position = _player.position;
        position.y = transform.position.y;

        transform.position = position;
    }

    private void RotateWithPlayer()
    {
        Vector3 rotation = _player.localEulerAngles;
        rotation.x = 90f;
        rotation.z = 0f;

        transform.localEulerAngles = rotation;
    }
}