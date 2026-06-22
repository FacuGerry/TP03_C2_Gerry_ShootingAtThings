using System.Collections;
using TMPro;
using UnityEngine;

public class UiScoreAdded : MonoBehaviour, IPooleable
{
    [SerializeField] private float _timeToShow = 1.5f;
    [SerializeField] private float _height = 1.5f;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private AnchorsSO _anchors;

    public bool IsActive { get; set; }

    private IEnumerator _coroutine;

    private Color _white = Color.white;
    private Color _black = Color.black;

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    private IEnumerator ShowingOnScreen()
    {
        bool hasChanged = false;
        bool isWhite = false;
        float clock = 0f;

        _text.color = _black;

        while (clock < _timeToShow)
        {
            clock += Time.deltaTime;

            if (!hasChanged)
                _text.color = isWhite ? _black : _white;

            hasChanged = !hasChanged;

            transform.LookAt(_anchors.playerTransform);
            transform.Rotate(0f, 180f, 0f);

            yield return null;
        }

        DeActivate();
    }

    public void StartShowOnScreen(Vector3 position)
    {
        position.y = _height;
        transform.position = position;

        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = ShowingOnScreen();
        StartCoroutine(_coroutine);
    }

    public void Activate()
    {
        IsActive = true;
        gameObject.SetActive(IsActive);
    }

    public void DeActivate()
    {
        IsActive = false;
        gameObject.SetActive(IsActive);
    }
}