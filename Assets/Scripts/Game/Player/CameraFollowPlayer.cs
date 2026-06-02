using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    [SerializeField] private GameDataSO _gameData;
    [SerializeField] private AnchorsSO _anchors;
    private Transform _player;
    private Transform _cameraPos;

    private float _yaw;
    private float _pitch;

    private void Start()
    {
        if (_anchors.playerTransform == null) return;

        _player = _anchors.playerTransform;
        _cameraPos = _anchors.cameraTransform;
        transform.position = _cameraPos.position;
    }

    private void Update()
    {
        LookAround();
    }

    private void LateUpdate()
    {
        FollowPlayer();
    }

    private void FollowPlayer() => transform.position = Vector3.Lerp(transform.position, _cameraPos.position, _gameData.playerData.cameraFollowSpeed * Time.deltaTime);

    private void LookAround()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        transform.Rotate(Vector3.up, mouseX * _gameData.playerData.rotationSpeedHor * Time.deltaTime);

        _yaw += mouseX * _gameData.playerData.rotationSpeedHor * Time.deltaTime;
        _pitch -= mouseY * _gameData.playerData.rotationSpeedVer * Time.deltaTime;

        _pitch = Mathf.Clamp(_pitch, _gameData.playerData.rotationMinVer, _gameData.playerData.rotationMaxVer);

        transform.localRotation = Quaternion.Euler(_pitch, _yaw, 0f);
        _player.rotation = Quaternion.Euler(0f, _yaw, 0f);

        // Quaternion weaponRot = _weapon.transform.rotation;
        // weaponRot.x = _pitch;
        // _weapon.transform.localEulerAngles = new(weaponRot.x, weaponRot.y, weaponRot.z);
    }
}