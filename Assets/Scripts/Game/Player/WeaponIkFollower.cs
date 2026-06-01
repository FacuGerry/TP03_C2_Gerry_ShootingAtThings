using UnityEngine;

[RequireComponent(typeof(Animator))]

public class WeaponIkFollower : MonoBehaviour
{
    [SerializeField] private WeaponDataSO[] _data;
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
        foreach (CustomIkController ik in _data[_index].controller)
        {
            _anim.SetIKPositionWeight(ik.goal, ik.weight);
            _anim.SetIKRotationWeight(ik.goal, ik.weight);

            _anim.SetIKPosition(ik.goal, ik.target.position);
            _anim.SetIKRotation(ik.goal, ik.target.rotation);
        }
    }

    public void ChangeIndex(bool isAdding) => _index += isAdding ? 1 : -1;
    public void ChangeIndexByNumber(int num) => _index = num;
}