using UnityEngine;

public class BulletPooler : BasePooler<Bullet>
{
	protected override void Awake()
	{
		base.Awake();
	}

	public override Bullet GetItem(Transform endPosition, Transform target)
	{
		Bullet bull = base.GetItem(endPosition, target);
		bull.transform.position = endPosition.position;
		bull.TargetEnemy = target;
		bull.bulletPooler = this;
		bull.gameObject.SetActive(true);
		return bull;
	}

}
