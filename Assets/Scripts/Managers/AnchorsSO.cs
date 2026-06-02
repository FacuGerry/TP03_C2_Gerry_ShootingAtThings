using UnityEngine;

[CreateAssetMenu(fileName = "Anchors", menuName = "Settings/Anchor")]
public class AnchorsSO : ScriptableObject
{
    public Transform playerTransform;
    public Transform cameraTransform;
}