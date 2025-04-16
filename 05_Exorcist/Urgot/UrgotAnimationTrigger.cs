using Hellmade.Sound;
using UnityEngine;

public class UrgotAnimationTrigger : EnemyAnimationTrigger
{
    // 내 파일 Prefabs에 Bullet이랑 Chain 있음
    [SerializeField] private GameObject _bulletParent;
    [SerializeField] private GameObject _bulletPrefab; // bullet
    [SerializeField] private GameObject _chainPrefab; // chain
    [SerializeField] private GameObject _downAttackCol;
    [SerializeField] private AudioClip _walkAudioClip;
    [SerializeField] private AudioClip _energyAudioClip;
    [SerializeField] private int _downAttackDamage = 25;

    private void PlayWalkSound()
    {
        EazySoundManager.PlaySound(_walkAudioClip, 0.3f);
    }

    private void PlayEnergySound()
    {
        EazySoundManager.PlaySound(_energyAudioClip);
    }

    private void ShootBullet()
    {
        Instantiate(_bulletPrefab, _bulletParent.transform.position, Quaternion.identity);
    }

    private void ShootChain()
    {
        Instantiate(_chainPrefab, _bulletParent.transform.position, Quaternion.identity);
    }

    private void CheckDownAttack()
    {
        _downAttackCol.GetComponent<Collider2D>().enabled = true;
        _downAttackCol.GetComponent<AttackRangeCheck>().CheckAttack(_downAttackDamage);
    }

    private void EndDownAttack()
    {
        _downAttackCol.GetComponent<Collider2D>().enabled = false;
    }
}
