using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Character _character;
    private Weapon _weapon;
    [SerializeField] private Animator _animator;
    private float _baseCooldown;

    public void InitPlayer(Character character)
    {
        _character = character;
        _weapon = _animator.GetComponent<Weapon>();
        _weapon.Init(character);

        _baseCooldown = _character.Stats.AttackSpeed;

        StartCoroutine(AttackCor());
    }

    IEnumerator AttackCor()
    {
        while (true)
        {
            float currentCooldown = _character.Stats.AttackSpeed;

            // �ִϸ��̼� �ӵ� = �⺻��Ÿ�� / ������Ÿ��
            float speedMultiplier = _baseCooldown / currentCooldown;

            _animator.SetFloat("attackSpeedMultiplier", speedMultiplier);
            _animator.Play("Attack");

            yield return new WaitForSeconds(speedMultiplier); // ���� ��Ÿ�� ��ŭ�� ���
        }
    }
}
