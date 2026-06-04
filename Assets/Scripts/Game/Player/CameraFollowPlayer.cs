using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    [SerializeField] private GameDataSO _gameData;
    [SerializeField] private AnchorsSO _anchors;
    private Transform _camera;
    private Transform _player;
    private Transform _weapon;
    private ChangeWeapon _chngWpn;

    private float _yaw;
    private float _pitch;

    private void Start()
    {
        if (_anchors.playerTransform == null) return;

        _camera = _anchors.cameraTransform;
        _player = _anchors.playerTransform;
        transform.position = _camera.position;
        _chngWpn = _player.GetComponent<ChangeWeapon>();
    }

    private void Update()
    {
        CalculateRotation();

        if (_camera)
            RotateCamera();

        if (_player)
            RotatePlayer();

        if (_weapon)
            RotateWeapon();
    }

    private void LateUpdate()
    {
        FollowPlayer();
    }

    private void FollowPlayer() => transform.position = Vector3.Lerp(transform.position, _camera.position, _gameData.playerData.cameraFollowSpeed * Time.deltaTime);

    private void CalculateRotation()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        transform.Rotate(Vector3.up, mouseX * _gameData.playerData.rotationSpeedHor * Time.deltaTime);

        _yaw += mouseX * _gameData.playerData.rotationSpeedHor * Time.deltaTime;
        _pitch -= mouseY * _gameData.playerData.rotationSpeedVer * Time.deltaTime;

        _pitch = Mathf.Clamp(_pitch, _gameData.playerData.rotationMinVer, _gameData.playerData.rotationMaxVer);
    }

    private void RotateCamera() => transform.localRotation = Quaternion.Euler(_pitch, _yaw, 0f);

    private void RotatePlayer() => _player.rotation = Quaternion.Euler(0f, _yaw, 0f);

    private void RotateWeapon()
    {
        if (_weapon != _chngWpn.ActiveWeaponPivot.transform)
            _weapon = _chngWpn.ActiveWeaponPivot.transform;

        _weapon.localRotation = Quaternion.Euler(_pitch, 0f, 0f);
    }
}