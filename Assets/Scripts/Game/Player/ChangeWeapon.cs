using UnityEngine;

public class ChangeWeapon : MonoBehaviour
{
    private readonly string _scroll = "Mouse ScrollWheel";

    [SerializeField] private WeaponIkFollower _ik;
    [SerializeField] private Weapon[] _weapon;
    private int _index = 0;

    private void Update()
    {
        if (Input.GetAxisRaw(_scroll) != 0f)
        {
            ChangeIndex(Input.GetAxisRaw(_scroll) > 0f);
            ToggleWeapon();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeIndexByNumber(0);
            ToggleWeapon();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeIndexByNumber(1);
            ToggleWeapon();
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ChangeIndexByNumber(2);
            ToggleWeapon();
        }
    }

    private void ChangeIndex(bool isAdding)
    {
        _index += isAdding ? 1 : -1;
        if (_index < 0)
            _index = _weapon.Length - 1;

        if (_index > _weapon.Length - 1)
            _index = 0;

        _ik.ChangeIndex(isAdding);
    }

    private void ChangeIndexByNumber(int num)
    {
        _index = num;
        _ik.ChangeIndexByNumber(num);
    }

    private void ToggleWeapon()
    {
        int index = _index - 1;
        if (index < 0)
            index = _weapon.Length - 1;

        _weapon[index].gameObject.SetActive(false);
        _weapon[_index].gameObject.SetActive(true);
    }
}