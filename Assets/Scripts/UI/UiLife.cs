using TMPro;
using UnityEngine;

public class UiLife : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private AnchorsSO _anchors;
    private PlayerHealthSystem _hs;

    private void Awake()
    {
        if (!_anchors.playerTransform) return;
        _hs = _anchors.playerTransform.gameObject.GetComponent<PlayerHealthSystem>();
    }

    private void OnEnable()
    {
        if (!_hs) return;
        _hs.OnLifeUpdated += OnLifeUpdated_UpdateText;
    }

    private void OnDestroy()
    {
        if (!_hs) return;
        _hs.OnLifeUpdated -= OnLifeUpdated_UpdateText;
    }

    private void OnLifeUpdated_UpdateText(int life) => _text.text = life.ToString();
}