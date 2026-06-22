using UnityEngine;
using UnityEngine.UI;

public class EnemyLifeBar : MonoBehaviour
{
    [SerializeField] private Image _img;
    [SerializeField] private EnemyHealthSystem _hs;

    private int _maxLife;

    private void OnEnable()
    {
        _hs.OnEnemyFirstDamage += InitBar;
        _hs.OnEnemyUpdateHealth += ReduceLife;
    }

    private void OnDisable()
    {
        _hs.OnEnemyFirstDamage -= InitBar;
        _hs.OnEnemyUpdateHealth -= ReduceLife;
    }

    private void InitBar(int maxLife) => _maxLife = maxLife;
    private void ReduceLife(int life) => _img.fillAmount = (float)life / (float)_maxLife;
}