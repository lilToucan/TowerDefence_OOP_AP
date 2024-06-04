using System;
using UnityEngine;

public class BaseTower : MonoBehaviour, IAttachable
{
	[SerializeField] protected float FireRate;
	[SerializeField] protected float Damage;
	[SerializeField] protected float Range;
	[SerializeField] protected float Hight;
	[SerializeField] protected LayerMask EnemyMask;
	[SerializeField] protected LayerMask ignoreRayLayer;
	[SerializeField] protected BulletPooler bulletPooler;
	protected Transform targetEnemy;
	protected float timer = 0;
	protected Bullet currentBullet;
	public Action<IHp> PowerUpsEffects;
	public Skyscraper Skyscraper { get; set; }

	public virtual void Attached(Transform target, Skyscraper skyscraper)
	{
		this.timer = 0;
		this.Skyscraper = skyscraper;
		Skyscraper.TowersUpdate += Shoot;
		Skyscraper.onPowerUpInteraction += UpdatePowerUps;
		UpdatePowerUps();
		this.transform.position = target.position + Vector3.up * Hight;
		if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit))
		{
			if (hit.transform.TryGetComponent<IAttachable>(out _))
			{
				hit.transform.gameObject.layer = ignoreRayLayer;
			}
		}

	}

	public virtual void Detached(Vector3 targetPos)
	{
		this.Skyscraper.TowersUpdate -= Shoot;
		this.Skyscraper.onPowerUpInteraction -= UpdatePowerUps;
		UpdatePowerUps();
		if (Physics.Raycast(this.transform.position, Vector3.down, out RaycastHit hit))
		{
			if (hit.transform.TryGetComponent<IAttachable>(out _))
			{
				hit.transform.gameObject.layer = 0;
			}
		}

		this.transform.position = targetPos + Vector3.up * Hight;
		this.Skyscraper = null;
	}
	public virtual void Shoot()
	{
		if (this.targetEnemy == null || Vector3.Distance(this.targetEnemy.position, this.transform.position) > this.Range)
		{
			this.GetTargetInRange();
		}

		else
		{
			Vector3 dir = targetEnemy.position - this.transform.position;
			dir.y = transform.position.y;
			dir = dir.normalized;
			float angle = Vector3.Angle(this.transform.forward, dir);
			transform.rotation = Quaternion.LookRotation(dir);
			if (this.timer < this.FireRate)
			{
				this.timer += Time.deltaTime;
				return;
			}

			this.currentBullet = this.bulletPooler.GetItem(this.transform, this.targetEnemy);
			this.currentBullet.onHit = this.OnHitEffect;
			this.timer = 0;
		}

	}

	public virtual void OnHitEffect(IHp enemyHp, Bullet bullet)
	{
		enemyHp.TakeDamage(Damage);
		PowerUpsEffects?.Invoke(enemyHp);
	}

	public void GetTargetInRange()
	{
		Collider[] coll = Physics.OverlapSphere(this.transform.position, this.Range, EnemyMask);
		if (coll.Length > 0)
		{
			this.targetEnemy = coll[0].transform;
		}
	}

	public void UpdatePowerUps()
	{
		// i just realized i could have just used skyscraper.PowerUpEffect
		PowerUpsEffects = Skyscraper.PowerUpEffect;
	}



#if UNITY_EDITOR
	protected virtual void OnDrawGizmos()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position, Range);
	}
#endif
}
