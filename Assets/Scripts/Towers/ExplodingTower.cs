using UnityEngine;

public class ExplodingTower : BaseTower
{
	public float ExplosionRange;
	Transform explodingBullet;
	public override void Attached(Transform target, Skyscraper skyscraper)
	{
		base.Attached(target, skyscraper);
	}
	public override void Detached(Vector3 targetPos)
	{
		base.Detached(targetPos);
	}

	public override void Shoot()
	{
		base.Shoot();
	}

	public override void OnHitEffect(IHp enemyHp, Bullet bullet)
	{
		explodingBullet = bullet.transform;
		Collider[] colliders = Physics.OverlapSphere(bullet.transform.position, ExplosionRange, EnemyMask);

		if (colliders.Length < 0)
		{
			return;
		}
		foreach (Collider collider in colliders)
		{
			if (collider.transform.TryGetComponent(out IHp cHP))
			{
				PowerUpsEffects?.Invoke(cHP);
				cHP.TakeDamage(Damage);
			}
		}
	}

#if UNITY_EDITOR

	protected override void OnDrawGizmos()
	{
		base.OnDrawGizmos();

		Gizmos.color = Color.blue;
		if (explodingBullet == null)
			Gizmos.DrawWireSphere(transform.position, ExplosionRange);
		else
			Gizmos.DrawSphere(explodingBullet.position, ExplosionRange);
	}
#endif
}
