using System.Collections;
using UnityEngine;

public class KnockBackTower : BaseTower
{
	public float EffectDuration;
	Transform lastHp;
	public override void Attached(Transform target, Skyscraper skyscraper)
	{
		base.Attached(target, skyscraper);
		lastHp = transform;
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
		base.OnHitEffect(enemyHp, bullet);
		if (enemyHp.HpTransform.TryGetComponent(out Enemy enemy))
		{
			if (enemy.transform == lastHp)
				return;
			lastHp = enemy.transform;
			StartCoroutine(KnockBack(enemy));
		}
	}

	IEnumerator KnockBack(Enemy enemy)
	{
		//i mean it's more like back instead of knockback
		enemy.SetDestination(enemy.StartPos);
		yield return new WaitForSeconds(EffectDuration);
		enemy.SetDestination(enemy.HomeBaseTran.position);
	}
}
