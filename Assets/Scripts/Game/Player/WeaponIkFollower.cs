using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class WeaponIkFollower : MonoBehaviour
{
    [SerializeField] private List<Weapon> _weaponsList = new();
    private Animator _anim;
    private int _index = 0;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    private void OnAnimatorIK(int layerIndex)
    {
        AnimateIK();
    }

    private void AnimateIK()
    {
        foreach (CustomIkController ik in _weaponsList[_index].controller)
        {
            _anim.SetIKPositionWeight(ik.goal, ik.weight);
            _anim.SetIKRotationWeight(ik.goal, ik.weight);

            _anim.SetIKPosition(ik.goal, ik.target.position);
            _anim.SetIKRotation(ik.goal, ik.target.rotation);
        }
    }

    public void ChangeIndex(int index) => _index = index;
}