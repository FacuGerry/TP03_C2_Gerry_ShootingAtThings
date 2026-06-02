using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChangeWeapon : MonoBehaviour
{
    private readonly string _scroll = "Mouse ScrollWheel";

    [SerializeField] private WeaponIkFollower _ik;
    [SerializeField] private List<GameObject> _weaponsList = new();

    public int Index { get; private set; } = 0;

    private void Awake()
    {
        ToggleWeapon();
    }

    private void Update()
    {
        if (Input.GetAxisRaw(_scroll) != 0f)
        {
            ChangeIndexByScroll(Input.GetAxisRaw(_scroll) > 0f);
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
    }

    private void ChangeIndexByScroll(bool isAdding)
    {
        Index += isAdding ? 1 : -1;
        if (Index < 0)
            Index = _weaponsList.Count - 1;

        if (Index > _weaponsList.Count - 1)
            Index = 0;
    }

    private void ChangeIndexByNumber(int num) => Index = num;

    private void ToggleWeapon()
    {
        _ik.ChangeIndex(Index);

        int index = Index - 1;
        if (index < 0)
            index = _weaponsList.Count - 1;

        _weaponsList[index].SetActive(false);
        _weaponsList[Index].SetActive(true);
    }
}