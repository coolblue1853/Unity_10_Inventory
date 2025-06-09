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

            // 애니메이션 속도 = 기본쿨타임 / 현재쿨타임
            float speedMultiplier = _baseCooldown / currentCooldown;

            _animator.SetFloat("attackSpeedMultiplier", speedMultiplier);
            _animator.Play("Attack");

            yield return new WaitForSeconds(speedMultiplier); // 현재 쿨타임 만큼만 대기
        }
    }
}
