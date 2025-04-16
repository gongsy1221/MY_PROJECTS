using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRangeCheck : MonoBehaviour
{
    private bool _isAttack = false;
    private Health _healthCompo;

    public void CheckAttack(int damage)
    {
        if(_isAttack && _healthCompo != null)
        {
            _healthCompo.ApplyDamage(damage, Vector2.zero, Vector2.zero);
            _isAttack = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            _healthCompo = other.GetComponent<Health>();
            _isAttack = true;
        }
    }
}
