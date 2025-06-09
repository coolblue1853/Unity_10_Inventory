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
            float currentAttackSpeed = _character.Stats.AttackSpeed;
            float cooldown = _baseCooldown / currentAttackSpeed;

            // 애니메이터 전체 속도 조절
            _animator.speed = currentAttackSpeed; 
            _animator.Play("Attack");

            yield return new WaitForSeconds(cooldown);
        }
    }

}
