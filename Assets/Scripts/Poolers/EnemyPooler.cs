using UnityEngine;

public class EnemyPooler : BasePooler<Enemy>
{
	public Transform baseTransform;

	protected override void Awake()
	{
		base.Awake();
	}

	public Enemy GetItem(Transform endPosition)
	{
		Enemy enemy = base.GetItem(endPosition, transform);
		enemy.transform.position = endPosition.position;
		enemy.HomeBaseTran = baseTransform;
		enemy.EnemyPooler = this;
		enemy.gameObject.SetActive(true);
		return enemy;
	}

	// not used: 
	public override Enemy GetItem(Transform endPosition, Transform target)
	{
		return null;
	}
}