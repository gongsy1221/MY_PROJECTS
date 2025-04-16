using UnityEngine;

public class UrgotDamageCaster : DamageCaster
{
    public override void GunShoot(Vector2 dir)
    {
        if (Physics2D.Raycast(transform.position, dir, 10, whatIsEnemy))
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, 10, whatIsEnemy);

            Vector2 direction = (hit.transform.position - transform.position).normalized;
            if (hit.collider.TryGetComponent<IDamageable>(out IDamageable target))
            {
                int damage = 1;
                SetDamageMultiplier(3f);
                target.ApplyDamage(damage, direction, Vector2.zero);
            }
        }
    }
}
